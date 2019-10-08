using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Room : ModelBase<Guid>
    {
        public Guid HotelId { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int TotalBeds { get; set; }
        public int Cost { get; set; }
    }
}
