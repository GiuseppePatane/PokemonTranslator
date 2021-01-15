using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeApiNet;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Core.Services;
using PokemonTranslator.Infrastructure.PokemonCient;
using PokemonTranslator.Infrastructure.ShakespeareClient;

namespace PokemonTranslator.Infrastructure
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITranslatorService, TranslatorService>();
            return services;
        }
    }

    public static class RegisterClients
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddTransient<PokeApiClient>();
            services.AddTransient<IPokemonClient, PokeClient>();
            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ITranslatorClient, ShakespeareHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClients:ShakespeareApiBaseUrl"]);
            });
            return services;
        }
    }
}