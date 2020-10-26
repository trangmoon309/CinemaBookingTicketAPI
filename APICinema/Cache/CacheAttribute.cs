using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICinema.Cache
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeLive;
        public CacheAttribute(int timeLive)
        {
            this.timeLive = timeLive;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSetting = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSetting>();

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            if (!cacheSetting.Enable) return;

            var cacheKey = GenerateKeyFromRequest(context.HttpContext.Request);
            var cacheReponse = await cacheService.GetCacheResponse(cacheKey);

            if(!string.IsNullOrEmpty(cacheReponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheReponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var executedContext = await next();
            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CreateCacheResponse(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(timeLive));
            }

        }

        private string GenerateKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach(var (key,value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
