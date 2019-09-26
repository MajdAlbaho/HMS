using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public int CityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModefiedDate { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
