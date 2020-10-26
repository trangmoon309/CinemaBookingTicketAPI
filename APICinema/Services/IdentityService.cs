using APICinema.Models;
using APICinema.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APICinema.Services
{
    public class IdentityService : IIdentityServices
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JwtSettings jwtSettings;
        private readonly TokenValidationParameters tokenValidationParameters;

        public IdentityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings;
            this.tokenValidationParameters = tokenValidationParameters;
        }
        public async Task<AuthenticationResult> LoginAsync(string username, string password)
        {

            if (await userManager.FindByEmailAsync(username) == null) return new AuthenticationResult
            {
                IsSuccess = false,
                Errors = new string[] { "User doesn't exist!" }
            };

            var user = await userManager.FindByEmailAsync(username);
            //await userManager.AddClaimAsync(user, new Claim("Admin", "true"));
            //await userManager.AddToRoleAsync(user, "Admin");

            if (await userManager.CheckPasswordAsync(user, password) == false) return new AuthenticationResult
            {
                IsSuccess = false,
                Errors = new string[] { "Password doesn't match!" }
            };

            return await GenerationTokenAsync(user);            
        }

        public async Task<AuthenticationResult> RegisterAsync(string username, string password)
        {
            if (await userManager.FindByEmailAsync(username) != null) return new AuthenticationResult
            {
                IsSuccess = false,
                Errors = new string[] { "Email has already existed!" }
            };

            IdentityUser newUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = username,
                UserName = username,
            };

            var result = await userManager.CreateAsync(newUser, password);
            if(!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    IsSuccess = false,
                    Errors = new string[] { "Failed to create new user!" }
                };
            }

            return await GenerationTokenAsync(newUser);

        }

        public async Task<AuthenticationResult> GenerationTokenAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }    
            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var accessKey = jwtSettings.AccessKey;

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(accessKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return new AuthenticationResult
            {
                IsSuccess = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();  
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityAlgorithm(validatedToken)) return null;

                return principal;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool IsJwtWithValidSecurityAlgorithm(SecurityToken securityToken)
        {
            return (securityToken is JwtSecurityToken jwtSecurityToken ) && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
