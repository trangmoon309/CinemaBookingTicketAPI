using APICinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Services
{
    public interface IIdentityServices
    {
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task<AuthenticationResult> RegisterAsync(string username, string password);
    }
}
