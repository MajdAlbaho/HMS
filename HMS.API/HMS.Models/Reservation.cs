using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public int TotalCost { get; set; }
        public DateTime? LastModefiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public virtual Hotel Hotel { get; set; }
    }
}
