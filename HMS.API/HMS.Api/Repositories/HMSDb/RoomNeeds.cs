using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class RoomNeeds
    {
        public Guid RoomId { get; set; }
        public byte NeedId { get; set; }
        public DateTime? LastModefiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Needs Need { get; set; }
        public virtual Rooms Room { get; set; }
    }
}
