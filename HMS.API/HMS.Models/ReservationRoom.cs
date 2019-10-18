using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class ReservationRoom : ModelBase<Guid>
    {
        public Guid ReservationId { get; set; }
        public Guid RoomId { get; set; }
        public Guid PersonId { get; set; }

        public Room Room { get; set; }
        public Person Person { get; set; }
    }
}
