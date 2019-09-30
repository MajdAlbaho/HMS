using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class CompanyPhones
    {
        public Guid CompanyId { get; set; }
        public string PhoneNumber { get; set; }
        public byte PhoneTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Companies Company { get; set; }
        public virtual PhoneTypes PhoneType { get; set; }
    }
}
