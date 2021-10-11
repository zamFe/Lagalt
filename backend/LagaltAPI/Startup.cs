using LagaltAPI.Context;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;
using System.IO;
using System.Reflection;
=========
using Npgsql;
using System;
>>>>>>>>> Temporary merge branch 2

namespace LagaltAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder => builder.WithOrigins("http://localhost:4200"));
            });

            services.AddDbContext<LagaltContext>();

            services.AddScoped(typeof(MessageService));
            services.AddScoped(typeof(ProjectService));
            services.AddScoped(typeof(ProfessionService));
            services.AddScoped(typeof(SkillService));
            services.AddScoped(typeof(UserService));
            services.AddAutoMapper(typeof(Startup));
            services.AddEntityFrameworkNpgsql().AddDbContext<LagaltContext>(options =>
            {
                //options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                string connStr;
                if(env == "Development") //if dev, get local connection
                {
                    connStr = Configuration.GetConnectionString("DefaultConnection");
                }
                else //else, get url from Heroku
                {
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
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
                }
                options.UseNpgsql(connStr);
            });

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

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LagaltAPI v1");
                if (!env.IsDevelopment())
                {
                    c.RoutePrefix = string.Empty;
                }
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
