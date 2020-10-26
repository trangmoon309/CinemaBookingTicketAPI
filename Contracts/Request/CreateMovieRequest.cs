using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Request
{
    public class CreateMovieRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Language { get; set; }
        public int Hour { get; set; }
        public string Director { get; set; }
        public string Star { get; set; }
        public string Description { get; set; }
    }
}
