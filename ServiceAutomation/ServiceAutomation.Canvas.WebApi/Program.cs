using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureKestrel(a =>
                {
                    a.AddServerHeader = false;
                }).UseUrls("https://localhost:7001", "http://localhost:7000");
            var port = Environment.GetEnvironmentVariable("PORT");
            if (!String.IsNullOrWhiteSpace(port))
            {
                builder.UseUrls("http://*:" + port);
            }
            return builder;
        }
    }
}
