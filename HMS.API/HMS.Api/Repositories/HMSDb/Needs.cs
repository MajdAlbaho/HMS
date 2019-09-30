using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Needs
    {
        public Needs()
        {
            RoomNeeds = new HashSet<RoomNeeds>();
        }

        public byte Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual ICollection<RoomNeeds> RoomNeeds { get; set; }
    }
}
