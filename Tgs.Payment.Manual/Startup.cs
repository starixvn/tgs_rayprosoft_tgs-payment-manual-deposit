using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Tgs.Payment.Manual.MessageQueue;

namespace Tgs.Payment.Manual
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddNatsMessageQueue(config =>
			{
				config
					.ConfigQueueOptions((options, sp) =>
					{
						var config = sp.GetRequiredService<IConfiguration>();

						options.Url = config.GetValue<string>("Manual:NatsUrl");
					})
					.AddProcessor<DepositProcessor>("tgs.payment.manual.deposit", "tgs.payment.manual.deposit");
				//.AddProcessor<WithdrawProcessor>("tgs.payment.manual.withdraw", "tgs.payment.manual.withdraw")
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
