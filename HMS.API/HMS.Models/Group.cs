﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Group : ModelBaseMultiLang<Guid>
    {
        public Guid CompanyId { get; set; }
    }
}
