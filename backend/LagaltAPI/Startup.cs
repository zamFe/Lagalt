using AspNetCoreRateLimit;
using LagaltAPI.Auth;
using LagaltAPI.Context;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;

namespace LagaltAPI
{
    public class Startup
    {
        private readonly string _clientOrigin = "Client Origin";
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Rate limiting
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            //automapper
            services.AddAutoMapper(typeof(Startup));

            //controllers and services
            services.AddControllers();
            services.AddScoped(typeof(ApplicationService));
            services.AddScoped(typeof(MessageService));
            services.AddScoped(typeof(ProjectService));
            services.AddScoped(typeof(ProfessionService));
            services.AddScoped(typeof(SkillService));
            services.AddScoped(typeof(UserService));
            services.AddScoped(typeof(UpdateService));

            //Policy
            services.AddCors(options =>
            {
                options.AddPolicy(name: _clientOrigin,
                                  builder => builder
                                  .WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_URL"))
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  );
            });

            // Entity Framework
            services.AddDbContext<LagaltContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                string connStr = "";
                if (env == "Development")
                    // Use Local Variable during development.
                    connStr = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                else
                {
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                    if (Environment.GetEnvironmentVariable("CLOUD_PLATFORM") == "HEROKU")
                    {
                        var pgUserPass = connUrl.Split("@")[0];
                        var pgHostPortDb = connUrl.Split("@")[1];
                        var pgHostPort = pgHostPortDb.Split("/")[0];

                        var pgsqlBuilder = new NpgsqlConnectionStringBuilder
                        {
                            Host = pgHostPort.Split(":")[0],
                            Port = Int32.Parse(pgHostPort.Split(":")[1]),
                            Username = pgUserPass.Split(":")[1].Split("//")[1],
                            Password = pgUserPass.Split(":")[2],
                            Database = pgHostPortDb.Split("/")[1],
                            SslMode = SslMode.Require,
                            TrustServerCertificate = true
                        };
                        connStr = pgsqlBuilder.ToString();
                        Environment.SetEnvironmentVariable("CONNECTION_STRING", connStr);
                    }
                    else if (Environment.GetEnvironmentVariable("CLOUD_PLATFORM") == "AZURE")
                    {
                        connStr = connUrl;
                        Environment.SetEnvironmentVariable("CONNECTION_STRING", connStr);
                    }
                }
                options.UseNpgsql(connStr);
            });

            // URI
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(UriService), options =>
            {
                var accessor = options.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var baseUri = request.Scheme + "://" + request.Host.ToUriComponent();
                return new UriService(baseUri);
            });

            // Authentication and Authorization
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{Environment.GetEnvironmentVariable("AUTH_DOMAIN")}/";
                    options.Audience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE");
                    //options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement("read:users", $"https://{Environment.GetEnvironmentVariable("AUTH_DOMAIN")}/")));
            });
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LagaltAPI",
                    Version = "1.0.0",
                    Description = "ASP.NET Core Web API for an online forum for creators."
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LagaltAPI v1");
                if (!env.IsDevelopment())
                    c.RoutePrefix = string.Empty;
            });

            app.UseIpRateLimiting();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(_clientOrigin);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
