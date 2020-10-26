using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Response
{
    public class MovieResponse
    {
        public bool IsNull { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string CateId { get; set; }
        public string PhotoPath { get; set; }
        public int Hour { get; set; }
        public string Director { get; set; }
        public string Star { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
