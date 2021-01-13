using System;
using Microsoft.Extensions.DependencyInjection;
using PokeApiNet;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Core.Services;
using PokemonTranslator.Infrastructure.PokeApi;

namespace PokemonTranslator.Infrastructure
{
    public static class  RegisterServices
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
    }
}