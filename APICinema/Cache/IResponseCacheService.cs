using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Cache
{
    public interface IResponseCacheService
    {
        Task CreateCacheResponse(string key, object response, TimeSpan timeLive);
        Task<string> GetCacheResponse(string key);
    }
}
