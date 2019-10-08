using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class ReservationRooms
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public Guid RoomId { get; set; }
        public Guid PersonId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Persons Person { get; set; }
        public virtual Reservations Reservation { get; set; }
        public virtual Rooms Room { get; set; }
    }
}
