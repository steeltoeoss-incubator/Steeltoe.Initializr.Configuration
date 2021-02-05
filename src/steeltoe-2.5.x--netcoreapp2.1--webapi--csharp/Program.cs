// <autogenerated />
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
{{#actuator-or-dynamic-logger}}
using Steeltoe.Extensions.Logging;
{{/actuator-or-dynamic-logger}}
{{#cloud-foundry}}
using Steeltoe.Extensions.Configuration;
{{^config-server}}
using Steeltoe.Extensions.Configuration.CloudFoundry;
{{/config-server}}
{{/cloud-foundry}}
{{#config-server}}
using Steeltoe.Extensions.Configuration.ConfigServer;
{{/config-server}}
{{#placeholder}}
using Steeltoe.Extensions.Configuration.PlaceholderCore;
{{/placeholder}}
{{#random-value}}
using Steeltoe.Extensions.Configuration.RandomValue;
{{/ random-value}}
namespace {{Namespace}}
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
            .Build()
            .Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                {{#cloud-foundry}}
                .UseCloudFoundryHosting() //Enable listening on a Env provided port
                {{^config-server}}
                .AddCloudFoundry() //Add cloudfoundry environment variables as a configuration source
                {{/config-server}}
                {{/cloud-foundry}}
                {{#config-server}}
			    .AddConfigServer()
                {{/config-server}}
                {{#placeholder}}
                .AddPlaceholderResolver()
                {{/placeholder}}
                {{#random-value}}
                .ConfigureAppConfiguration((b) => b.AddRandomValueSource())
                {{/random-value}}
                .UseStartup<Startup>();
            {{#actuator-or-dynamic-logger}}
            builder.ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                loggingBuilder.AddDynamicConsole();
            });
            {{/actuator-or-dynamic-logger}}
            return builder;
        }
    }
}