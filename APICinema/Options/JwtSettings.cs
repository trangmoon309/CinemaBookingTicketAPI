using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Options
{
    public class JwtSettings
    {
        public TimeSpan TokenLifeTime { get; set; }
        public string AccessKey { get; set; }
    }
}
