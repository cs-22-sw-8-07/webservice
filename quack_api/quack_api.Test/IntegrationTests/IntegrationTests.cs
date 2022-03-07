using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quack_api;

namespace quack_api.Test.IntegrationTests
{
    public class IntegrationTests
    {
        /// <summary>
        /// Sets up a local test server where the configuration is giving through the appsettings file.
        /// </summary>
        /// <returns>a test server</returns>
        private TestServer GetTestServer()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var webHostBuilder =
                  new WebHostBuilder()
                  .UseConfiguration(configurationBuilder)
                        .UseEnvironment("production") // You can set the environment you want (development, staging, production)
                        .UseStartup<Startup>(); // Startup class of your web app project
            // Return test server
            return new TestServer(webHostBuilder);
        }
    }
}
