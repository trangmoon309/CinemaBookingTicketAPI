using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Request
{
    public class UserLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
