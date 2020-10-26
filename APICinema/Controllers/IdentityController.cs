using APICinema.Services;
using Contracts;
using Contracts.Request;
using Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityServices identityService;
        
        public IdentityController(IIdentityServices identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost(ApiRoute.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)).ToArray()
                });
            }
            var result = await identityService.RegisterAsync(request.UserName, request.Password);
            if (!result.IsSuccess) return BadRequest(new AuthFailedResponse { Errors = result.Errors });

            return Ok(new AuthSuccessResponse
            {
                Token = result.Token
            });
        }

        [HttpPost(ApiRoute.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await identityService.LoginAsync(request.UserName, request.Password);
            if (!result.IsSuccess) return BadRequest(new AuthFailedResponse { Errors = result.Errors });

            return Ok(new AuthSuccessResponse
            {
                Token = result.Token
            });
        }


    }
}
