using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class ModelBase<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

    public class ModelBaseMultiLang<T> : ModelBase<T>
    {
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string FriName { get; set; }
    }

}
