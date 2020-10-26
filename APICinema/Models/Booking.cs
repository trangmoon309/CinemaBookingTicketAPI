using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Models
{
    public class Booking
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string UserID { get; set; }
        public int MovieID { get; set; }
        public string TimeID { get; set; }
        public int Amount { get; set; }
        public float TotalBill { get; set; }
    }
}
