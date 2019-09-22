using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Countries
    {
        public Countries()
        {
            Cities = new HashSet<Cities>();
        }

        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModefiedDate { get; set; }

        public virtual ICollection<Cities> Cities { get; set; }
    }
}
