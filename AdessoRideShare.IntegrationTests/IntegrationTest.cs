using AdessoRideShare.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AdessoRideShare.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected readonly TestServer server;

        protected IntegrationTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseConfiguration(config);

            server = new TestServer(builder);

            TestClient = server.CreateClient();
        }
    }
}
