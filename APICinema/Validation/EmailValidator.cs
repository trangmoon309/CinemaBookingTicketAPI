using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Validation
{
    //custom validation
    public class EmailValidator : ValidationAttribute
    {
        public  override bool IsValid(object value)
        {
            var domain = value.ToString().Split('.')[1];
            if (domain != "dut.com") return false;
            return true;
        }

    }
}
