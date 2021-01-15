using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using PokemonTranslator.Api;

namespace PokemonTranslator.FunctionalTests
{
    public class ApiTestFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.FunctionalTest.json");
            builder
                .UseSetting("http_port", "5000")
                .UseConfiguration(configurationBuilder.Build());
        }
    }
}