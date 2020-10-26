using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contracts.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username's length is 20 characters maximum!")]
        //[EmailValidator]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

    }
}
