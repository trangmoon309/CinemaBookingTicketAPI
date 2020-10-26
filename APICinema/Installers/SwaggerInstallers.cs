using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Installers
{
    //https://www.c-sharpcorner.com/article/how-to-use-jwt-authentication-with-web-api/
    public class SwaggerInstallers : IInstallers
    {
        public void InstallService(IServiceCollection service, IConfiguration configuration)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
                    Description = "Document about APIs of Cinema Booking",
                    Version = "v1",
                    Title = "Ciname Booking Ticket API by HPT"
                });

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Authen using bearer scheme.",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authentication"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                                 Name = "Name of Security Scheme",
                                Type = SecuritySchemeType.ApiKey,
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {  
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                        },
                        new List<string>()
                    }
                });
            });


        }
    }
}
