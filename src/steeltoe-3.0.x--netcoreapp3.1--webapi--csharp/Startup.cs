using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
{{#data-mongodb}}
using Steeltoe.Connector.MongoDb;
{{/data-mongodb}}
{{#mysql-or-mysql-efcore}}
using Steeltoe.Connector.MySql;
{{/mysql-or-mysql-efcore}}
{{#mysql-efcore}}
using Steeltoe.Connector.MySql.EFCore;
{{/mysql-efcore}}
{{#oauth}}
using Steeltoe.Connector.OAuth;
{{/oauth}}
{{#postgresql}}
using Steeltoe.Connector.PostgreSql;
{{/postgresql}}
{{#postgresql-efcore}}
using Steeltoe.Connector.PostgreSql.EFCore;
{{/postgresql-efcore}}
{{#amqp}}
using Steeltoe.Connector.RabbitMQ;
{{/amqp}}
{{#data-redis}}
using Steeltoe.Connector.Redis;
{{/data-redis}}
{{#sqlserver}}
using Steeltoe.Connector.SqlServer;
{{/sqlserver}}
{{#eureka-client}}
using Steeltoe.Discovery.Client;
{{/eureka-client}}
{{#actuator}}
{{#cloud-foundry}}
using Steeltoe.Management.CloudFoundry;
{{/cloud-foundry}}
{{^cloud-foundry}}
using Steeltoe.Management.Endpoint;
{{/cloud-foundry}}
{{/actuator}}
{{#RequiresHttps}}
using Microsoft.AspNetCore.HttpsPolicy;
{{/RequiresHttps}}
{{#Auth}}
using Microsoft.AspNetCore.Authentication;
{{/Auth}}
{{#OrganizationalAuth}}
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
{{/OrganizationalAuth}}
{{#IndividualB2CAuth}}
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
{{/IndividualB2CAuth}}
{{#circuit-breaker}}
using Steeltoe.CircuitBreaker.Hystrix;
{{/circuit-breaker}}

namespace {{Namespace}}
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
{{#OrganizationalAuth}}
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
{{/OrganizationalAuth}}
{{#IndividualB2CAuth}}
            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));
{{/IndividualB2CAuth}}
{{#mysql}}
            services.AddMySqlConnection(Configuration);
{{/mysql}}
{{#actuator}}
{{#cloud-foundry}}
            services.AddCloudFoundryActuators(Configuration);
{{/cloud-foundry}}
{{^cloud-foundry}}
            services.AddAllActuators(Configuration);
{{/cloud-foundry}}
{{/actuator}}
{{#eureka-client}}
            services.AddDiscoveryClient(Configuration);
{{/eureka-client}}
{{#postgresql}}
            services.AddPostgresConnection(Configuration);
{{/postgresql}}
{{#amqp}}
            services.AddRabbitMQConnection(Configuration);
{{/amqp}}
{{#data-redis}}
            // Add the Redis distributed cache.
            // We are using the Steeltoe Redis Connector to pickup the CloudFoundry
            // Redis Service binding and use it to configure the underlying RedisCache
            // This adds a IDistributedCache to the container
            services.AddDistributedRedisCache(Configuration);
            // This works like the above, but adds a IConnectionMultiplexer to the container
            // services.AddRedisConnectionMultiplexer(Configuration);
{{/data-redis}}
{{#data-mongodb}}
            services.AddMongoClient(Configuration);
{{/data-mongodb}}
{{#oauth}}
            services.AddOAuthServiceOptions(Configuration);
{{/oauth}}
{{#mysql-efcore}}
            services.AddDbContext<TestContext>(options => options.UseMySql(Configuration));
{{/mysql-efcore}}
{{#postgresql-efcore}}
            services.AddDbContext<TestContext>(options => options.UseNpgsql(Configuration));
{{/postgresql-efcore}}
{{#sqlserver}}
            services.AddSqlServerConnection(Configuration);
{{/sqlserver}}
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
{{#RequiresHttps}}
            else
            {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
{{/RequiresHttps}}
{{#Auth}}
            app.UseAuthentication();
{{/Auth}}

{{#eureka-client}}
            app.UseDiscoveryClient();
{{/eureka-client}}
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
