using APICinema.Models;
using APICinema.Options;
using APICinema.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICinema.Installers
{
    public class MvcInstaller : IInstallers
    {
        public void InstallService(IServiceCollection service, IConfiguration configuration)
        {
            service.AddAutoMapper(typeof(Startup));

            service.AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            JwtSettings jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

            service.AddSingleton(jwtSettings);


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false, 
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.AccessKey)),
                ValidateAudience = false,
                RequireExpirationTime = false, 
                ValidateLifetime = true 
            };

            service.AddSingleton(tokenValidationParameters);


            service.AddScoped<IIdentityServices, IdentityService>();

            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Bearer
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(xx =>
                {
                    xx.SaveToken = true;
                    xx.TokenValidationParameters = tokenValidationParameters;
                });

            service.AddScoped<IMovieService, MovieService>();

            service.AddAutoMapper(typeof(Startup));

            //Paged 
            service.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var path = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new UriService(path);
            });

            service.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            });
        }
    }
}
