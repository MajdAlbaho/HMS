using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Api.Repositories.HMSDb;

namespace HMS.Api.Repositories.Interfaces
{
    public interface IPersonRepository : IRepositoryBase<Persons, Guid>
    {

    }
}
