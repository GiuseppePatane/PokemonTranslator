using Microsoft.Extensions.DependencyInjection;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Core.Services;

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
}