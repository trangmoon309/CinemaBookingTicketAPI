using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Cache
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache distributed;
        public ResponseCacheService(IDistributedCache distributed)
        {
            this.distributed = distributed;
        }
        public async Task CreateCacheResponse(string key, object response, TimeSpan timeLive)
        {
            if (response == null) return;
            var serializeReponse = JsonConvert.SerializeObject(response);
            await distributed.SetStringAsync(key, serializeReponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeLive
            });
        }

        public async Task<string> GetCacheResponse(string key)
        {
            var cacheReponse = await distributed.GetStringAsync(key);
            if (string.IsNullOrEmpty(cacheReponse)) return null;
            return cacheReponse;
        }
    }
}
