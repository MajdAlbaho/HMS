using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Areas
    {
        public Areas()
        {
            Companies = new HashSet<Companies>();
            Hotels = new HashSet<Hotels>();
        }

        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public int CityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModefiedDate { get; set; }

        public virtual Cities City { get; set; }
        public virtual ICollection<Companies> Companies { get; set; }
        public virtual ICollection<Hotels> Hotels { get; set; }
    }
}
