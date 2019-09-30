using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Rooms
    {
        public Rooms()
        {
            ReservationRooms = new HashSet<ReservationRooms>();
            RoomNeeds = new HashSet<RoomNeeds>();
        }

        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public short RoomNumber { get; set; }
        public byte FloorNumber { get; set; }
        public byte TotalBeds { get; set; }
        public int Cost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Hotels Hotel { get; set; }
        public virtual ICollection<ReservationRooms> ReservationRooms { get; set; }
        public virtual ICollection<RoomNeeds> RoomNeeds { get; set; }
    }
}
