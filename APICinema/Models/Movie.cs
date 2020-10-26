using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string CateId { get; set; }
        public string PhotoPath { get; set; }
        public int Hour { get; set; }
        public string Director { get; set; }
        public string Star { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Rating { get; set; }
        public Category Type { get; set; }
        public List<Time> Times { get; set; }
    }
}
