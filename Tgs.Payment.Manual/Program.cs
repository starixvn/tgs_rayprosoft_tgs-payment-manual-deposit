using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tgs.Payment.Manual.MessageQueue;
using TGS.Payment.ManualDeposit;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("/config/appsettings.json", true);

builder.Logging
    .AddOpenTelemetry(options =>
    {
        options.IncludeFormattedMessage = true;
        options.ParseStateValues = true;
        _ = options.AddFluentdForwardExporter(fluentOptions =>
        {
            var nodeIp = builder.Configuration.GetValue<string>("NODE_IP");
            fluentOptions.Host = nodeIp;
            fluentOptions.Tag = builder.Configuration.GetValue<string>("OTEL_SERVICE_NAME");
            fluentOptions.UseMessagePack();
        });
    });

builder.Services
    .AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.All;
        logging.MediaTypeOptions.AddText("application/x-www-form-urlencoded");
        logging.MediaTypeOptions.AddText("text/html");
    });

builder.Services.AddHealthChecks();

builder.Services
    .AddOptions<OtlpExporterOptions>()
    .Configure<IConfiguration>((options, conifg) =>
    {
        var nodeIp = conifg.GetValue<string>("NODE_IP");

        if (!string.IsNullOrWhiteSpace(nodeIp))
            options.Endpoint = new Uri($"http://{nodeIp}:4317");

        conifg.GetSection("Opentelemetry:Tracing").Bind(options);
    });

builder.Services.AddOpenTelemetryTracing(builder =>
{
    builder
        .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddEnvironmentVariableDetector())
        .AddSource("Tgs.*")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddProfileViewExporter()
        .AddOtlpExporter();
});

builder.Services.Configure<AspNetCoreInstrumentationOptions>(options =>
{
    options.Filter = httpContext =>
    {
        return !httpContext.Request.Path.HasValue
            || !httpContext.Request.Path.StartsWithSegments("/swagger")
            && !httpContext.Request.Path.StartsWithSegments("/healthz")
            && !httpContext.Request.Path.StartsWithSegments("/profiler/")
            && !new[] { "html", "ico", "png", "jpg" }.Any(
                ext => httpContext.Request.Path.Value.EndsWith($".{ext}"));
    };
});

builder.Services.AddProfileViewer();

builder.Services
    .AddSingleton<ManualDepositSerializer>()
    .AddMessageQueue()
    .AddNatsMessageQueue(config =>
    {
        _ = config
            .ConfigQueueOptions((options, sp) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                options.Url = config.GetValue<string>("Manual:NatsUrl");
            })
            .AddProcessor<DepositProcessor>("tgs.payment.manual.deposit", "tgs.payment.manual.deposit")
            .AddProcessor<WithdrawProcessor>("tgs.payment.manual.withdraw", "tgs.payment.manual.withdraw");
    });

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
