using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Response
{
    public class ErrorReponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
