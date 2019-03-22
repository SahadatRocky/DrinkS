using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Dermo.db_context;
using Dermo.db_Initializer;

namespace Dermo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // REPLACE THIS:
            // BuildWebHost(args).Run();

            // WITH THIS:
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                // Retrieve your DbContext isntance here
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                // place your DB seeding code here
               DbInitializer.Seed(dbContext);
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
