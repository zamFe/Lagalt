using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LagaltAPI.Context;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace LagaltAPI
{
    public static class Extensions
    {
        public static IHost MigrateDatabase(this IHost webHost)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if(env == "Production")
            {
                var serviceScopeFactory = (IServiceProvider)webHost.Services.GetService(typeof(IServiceScopeFactory));
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var dbContext = services.GetRequiredService<LagaltContext>();

                    dbContext.Database.Migrate();
                }
            }
            return webHost;
        }
    }
}
