using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Group : ModelBase<Guid>
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }

        public Company Company { get; set; }

        public List<ReservationGroup> ReservationGroups { get; set; }

    }
}
