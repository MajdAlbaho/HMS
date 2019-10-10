using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class ReservationGroup
    {
        public Guid ReservationId { get; set; }
        public Guid GroupId { get; set; }

        public Group Group { get; set; }
        public Reservation Reservation { get; set; }
    }
}
