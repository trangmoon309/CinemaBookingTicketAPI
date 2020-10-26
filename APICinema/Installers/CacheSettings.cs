using APICinema.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Installers
{
    public class CacheSettings : IInstallers
    {
        public void InstallService(IServiceCollection service, IConfiguration configuration)
        {
            RedisCacheSetting redisCacheSetting = new RedisCacheSetting();
            configuration.GetSection(nameof(RedisCacheSetting)).Bind(redisCacheSetting);

            service.AddSingleton(redisCacheSetting);

            if (redisCacheSetting.Enable) return;

            service.AddStackExchangeRedisCache(opt => opt.Configuration = redisCacheSetting.ConnectionString);
            service.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
