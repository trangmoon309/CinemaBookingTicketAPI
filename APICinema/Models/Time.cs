using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Models
{
    public class Time
    {
        [Key]
        public string Id { get; set; }
        public string fromTime { get; set; }
        public string Date { get; set; }
        public int IDMovie { get; set; }
        public Movie Movie { get; set; }

    }
}
