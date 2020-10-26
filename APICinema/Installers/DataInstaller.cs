using APICinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Installers
{
    public class DataInstaller : IInstallers
    {
        public void InstallService(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AppDbContext>(e => e.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnectionString")));

            service.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {

            })
                .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>();

            
        }
    }
}
