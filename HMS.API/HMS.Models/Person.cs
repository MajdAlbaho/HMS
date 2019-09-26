﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstArName { get; set; }
        public string LastArName { get; set; }
        public string FatherArName { get; set; }
        public string MotherArName { get; set; }
        public string FirstEnName { get; set; }
        public string LastEnName { get; set; }
        public string FatherEnName { get; set; }
        public string MotherEnName { get; set; }
        public string FirstFriName { get; set; }
        public string LastFriName { get; set; }
        public string FatherFriName { get; set; }
        public string MotherFriName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastModefiedDate { get; set; }

        public virtual ICollection<Group> GroupPersons { get; set; }
    }
}