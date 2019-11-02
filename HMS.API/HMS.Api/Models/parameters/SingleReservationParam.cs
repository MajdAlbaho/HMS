﻿using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Models.parameters
{
    public class SingleReservationParam
    {
        public List<Person> Person { get; set; }
        public Reservation Reservation { get; set; }
        public Group Group { get; set; }
    }
}