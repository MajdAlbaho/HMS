using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Hotels
    {
        public Hotels()
        {
            Reservations = new HashSet<Reservations>();
            Rooms = new HashSet<Rooms>();
        }

        public Guid Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public int AreaId { get; set; }
        public string Notes { get; set; }
        public byte Rate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Areas Area { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
