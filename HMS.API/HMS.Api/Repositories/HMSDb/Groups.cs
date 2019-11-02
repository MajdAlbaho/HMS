﻿using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Groups
    {
        public Groups()
        {
            ReservationGroups = new HashSet<ReservationGroups>();
        }

        public Guid Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Companies Company { get; set; }
        public virtual ICollection<ReservationGroups> ReservationGroups { get; set; }
    }
}
