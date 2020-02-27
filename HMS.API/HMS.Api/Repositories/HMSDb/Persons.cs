using System;
using System.Collections.Generic;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class Persons
    {
        public Persons()
        {
            ReservationRooms = new HashSet<ReservationRooms>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public int NationalityId { get; set; }
        public string IdNumber { get; set; }
        public byte[] CopyOfIdentity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual Nationalities Nationality { get; set; }
        public virtual ICollection<ReservationRooms> ReservationRooms { get; set; }
    }
}
