﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Reservation : ModelBase<Guid>
    {
        public string Code { get; set; }
        public Guid HotelId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public int TotalCost { get; set; }

        public int Nights => (int)(EndDate - StartDate).TotalDays;
        public string[] Rooms => ReservationRooms.Select(e => e.Room.RoomNumber.ToString())
            .Distinct().ToArray();
        public int Guests => ReservationRooms.Select(e => e.PersonId).Distinct().Count();
        public int Adults { get; set; }
        public int RoomType { get; set; }
        public Guid? RoomId { get; set; }

        public Status Status { get; set; }
        public List<ReservationRoom> ReservationRooms { get; set; }
        public List<ReservationGroup> ReservationGroups { get; set; }

    }
}
