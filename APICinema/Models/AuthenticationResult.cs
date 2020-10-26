using System;
using System.Collections.Generic;
using System.Text;

namespace APICinema.Models
{
    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string Token { get; set; }
    }
}
