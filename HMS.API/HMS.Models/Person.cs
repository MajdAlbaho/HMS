using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Person : ModelBase<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public int NationalityId { get; set; }
        public string IdNumber { get; set; }
        public byte[] CopyOfIdentity { get; set; }


        public Guid RoomId { get; set; }
    }
}
