using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
        public int AreaId { get; set; }
        public string ArAddress { get; set; }
        public string EnAddress { get; set; }
        public string FriAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModefiedDate { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<string> CompanyPhones { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
