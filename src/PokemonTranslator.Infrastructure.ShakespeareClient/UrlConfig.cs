using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonTranslator.Infrastructure.ShakespeareClient
{
    public static class UrlConfig
    {
        public static string GetShakespeareTranslation(string text)
        {
            var queryString = BuildQueryString(new Dictionary<string, string>
            {
                {"text", text}
            });
            var url = $"translate/shakespeare.json?{queryString}";
            return url;
        }


        private static string BuildQueryString(Dictionary<string, string> keyValueQueryString)
        {
            var validParameters =
                keyValueQueryString.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value));
            return string.Join("&",
                validParameters.Select(x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"));
        }
    }
}