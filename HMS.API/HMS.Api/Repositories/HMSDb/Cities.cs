using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Cities
    {
        public Cities()
        {
            Areas = new HashSet<Areas>();
        }

        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModefiedDate { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Areas> Areas { get; set; }
    }
}
