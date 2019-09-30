using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Repositories
{
    public class CountryRepository : RepositoryBase<Countries, int>, ICountryRepository
    {
        public CountryRepository(HMSDbContext context)
            : base(context) {
        }
    }
}
