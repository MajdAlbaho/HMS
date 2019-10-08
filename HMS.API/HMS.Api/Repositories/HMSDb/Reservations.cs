using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Reservations
    {
        public Reservations()
        {
            ReservationRooms = new HashSet<ReservationRooms>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid HotelId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public int TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Hotels Hotel { get; set; }
        public virtual Status Status { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<ReservationRooms> ReservationRooms { get; set; }
    }
}
