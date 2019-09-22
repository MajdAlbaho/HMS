using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Companies
    {
        public Companies()
        {
            CompanyPhones = new HashSet<CompanyPhones>();
            Groups = new HashSet<Groups>();
        }

        public Guid Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public int AreaId { get; set; }
        public string ArAddress { get; set; }
        public string EnAddress { get; set; }
        public string FriAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModefiedDate { get; set; }

        public virtual Areas Area { get; set; }
        public virtual ICollection<CompanyPhones> CompanyPhones { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
    }
}
