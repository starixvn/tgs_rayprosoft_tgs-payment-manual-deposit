using System.Text.Json;
using System.Text.Json.Serialization;
using FluentdForward.OpenTelemetry.MessagePack;
using Microsoft.AspNetCore.HttpLogging;
using NATS.Client.Core;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tgs.Payment.Manual.MessageQueue;
using TGS.Payment.ManualDeposit;

var builder = WebApplication.CreateBuilder(args);

builder.Logging
    .AddOpenTelemetry(options =>
    {
        options.IncludeFormattedMessage = true;
        options.ParseStateValues = true;
        _ = options.AddFluentdForwardExporter(fluentOptions =>
        {
            var nodeIp = builder.Configuration.GetValue<string>("Otel:NodeIp");
            fluentOptions.Host = nodeIp;
            fluentOptions.Tag = builder.Configuration.GetValue<string>("Otel:ServiceName");
            fluentOptions.UseMessagePack(LogRecordFormatterResolver.GetResolverInstanceWithExtendInfo(
                "MachineInfo",
                () => new
                {
                    Environment.MachineName
                })
);
        });
    });

builder.Services
    .AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.All;
        logging.MediaTypeOptions.AddText("application/x-www-form-urlencoded");
        logging.MediaTypeOptions.AddText("text/html");
    });

builder.Services
    .AddHealthChecks();

var excludeExtensions = new[] { "html", "ico", "png", "jpg" };
builder.Services
    .AddOpenTelemetry()
    .WithTracing(builder => builder
        .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddEnvironmentVariableDetector())
        .AddSource("Tgs.*")
        .AddSource("Valhalla.*")
        .AddAspNetCoreInstrumentation(options => options.Filter =
            httpContext => !httpContext.Request.Path.HasValue
                || !httpContext.Request.Path.StartsWithSegments("/swagger")
                && !httpContext.Request.Path.StartsWithSegments("/healthz")
                && !httpContext.Request.Path.StartsWithSegments("/profiler/")
                && !excludeExtensions.Any(
                    ext => httpContext.Request.Path.Value.EndsWith($".{ext}")))
        .AddHttpClientInstrumentation()
        .AddProfileViewExporter()
        .AddOtlpExporter())
    .Services
    .AddProfileViewer();

builder.Services
    .AddSingleton<ManualDepositSerializer>()
    .AddKeyedSingleton(
        "Nats",
        (sp, _) => new NatsConnection(NatsOpts.Default with
        {
            Url = builder.Configuration.GetValue<string>("Manual:NatsUrl")
        }))
    .AddMessageQueue()
    .AddNatsMessageQueue(config => config
        .ConfigureResolveConnection(sp => sp.GetRequiredKeyedService<NatsConnection>("Nats"))
        .AddProcessor<DepositProcessor>("tgs.payment.manual.deposit", "tgs.payment.manual.deposit")
        .AddProcessor<WithdrawProcessor>("tgs.payment.manual.withdraw", "tgs.payment.manual.withdraw"))
    .AddNatsGlobPatternExchange("_INBOX.*");

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.AddManualDepositConverters();
    });

var app = builder.Build();

app.UseHttpLogging();

app.UseRouting();

app.MapHealthChecks("/healthz");

app.MapProfileViewer();

app.Run();
