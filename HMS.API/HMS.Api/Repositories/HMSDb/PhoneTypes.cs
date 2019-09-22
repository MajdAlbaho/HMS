using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class PhoneTypes
    {
        public PhoneTypes()
        {
            CompanyPhones = new HashSet<CompanyPhones>();
        }

        public byte Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public DateTime? LastModefiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CompanyPhones> CompanyPhones { get; set; }
    }
}
