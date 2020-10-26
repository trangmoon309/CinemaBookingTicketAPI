using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Installers
{
    public static class InstallServerExtension
    {
        public static void ConfigureService(this IServiceCollection service, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>

                typeof(IInstallers).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract
            ).Select(Activator.CreateInstance).Cast<IInstallers>().ToList();

            installers.ForEach(x => x.InstallService(service, configuration));
        }
    }
}
