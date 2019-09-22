using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class GroupPersons
    {
        public Guid GroupId { get; set; }
        public Guid PersonId { get; set; }
        public DateTime? LastModefiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Persons Person { get; set; }
    }
}
