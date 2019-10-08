using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class ReservationGroups
    {
        public Guid ReservationId { get; set; }
        public Guid GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Reservations Reservation { get; set; }
    }
}
