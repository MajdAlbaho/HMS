using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;

namespace HMS.Api.Repositories
{
    public class PersonRepository : RepositoryBase<Persons, Guid>, IPersonRepository
    {
        public PersonRepository(HMSDbContext context)
            : base(context) {
        }
    }
}
