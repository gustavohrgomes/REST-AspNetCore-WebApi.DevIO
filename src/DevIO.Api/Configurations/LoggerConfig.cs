using DevIO.Api.Extensions;
using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.Configurations
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "002269b470184239941d5fc979b501a3";
                o.LogId = new Guid("a2f78d1d-e45b-42af-988d-cd4927c20d24");
            });

            //services.AddLogging(builder => 
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "002269b470184239941d5fc979b501a3";
            //        o.LogId = new Guid("a2f78d1d-e45b-42af-988d-cd4927c20d24");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "388dd3a277cb44c4aa128b5c899a3106";
                    options.LogId = new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096");
                    options.HeartbeatId = "API Fornecedores";

                })
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "SqlServer");

            //services.AddHealthChecksUI();

            return services;
        }

        public static IApplicationBuilder UseLoggingConfig(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
