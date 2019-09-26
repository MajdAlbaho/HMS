using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public short RoomNumber { get; set; }
        public byte FloorNumber { get; set; }
        public byte TotalBeds { get; set; }
        public int Cost { get; set; }
        public DateTime? LastModefiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public Hotel Hotel { get; set; }
        public ICollection<Need> RoomNeeds { get; set; }
    }
}
