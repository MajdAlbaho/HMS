using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Guests
    {
        public Guests()
        {
            Reservations = new HashSet<Reservations>();
        }

        public Guid Id { get; set; }
        public byte GuestTypeId { get; set; }
        public DateTime? LastModefiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual GuestTypes GuestType { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
