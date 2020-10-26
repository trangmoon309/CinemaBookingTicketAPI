using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Cache
{
    public class RedisCacheSetting 
    {
        public string ConnectionString { get; set; }
        public bool Enable { get; set; }
    }
}
