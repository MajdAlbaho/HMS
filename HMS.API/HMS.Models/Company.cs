using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Company : ModelBaseMultiLang<Guid>
    {
        public int AreaId { get; set; }
        public string ArAddress { get; set; }
        public string EnAddress { get; set; }
        public string FriAddress { get; set; }
        public string Notes { get; set; }
    }
}
