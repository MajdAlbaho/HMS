using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class UsersHotels
    {
        public string UserId { get; set; }
        public Guid HotelId { get; set; }
    }
}
