using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VittighedsMaskinen.Authentication
{
    public class InMemoryGetApiKeyQuery : IGetAllApiKeysQuery
    {
        private readonly IDictionary<string, ApiKey> _apikeys;

        public InMemoryGetApiKeyQuery()
        {
            var existingApiKeys = new List<ApiKey>
            {
                new ApiKey(1, "Admin", "1234", new List<string> { "Everything"}),
                new ApiKey(2, "Dads", "4321", new List<string> { "DadJokes"})
            };

            _apikeys = existingApiKeys.ToDictionary(k => k.Key, k => k);

        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apikeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
}
