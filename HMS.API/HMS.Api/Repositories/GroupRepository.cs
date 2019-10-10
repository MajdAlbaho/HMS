using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Repositories
{
    public class GroupRepository : RepositoryBase<Groups, Guid>, IGroupRepository
    {
        public GroupRepository(HMSDbContext context)
            : base(context) {
        }

        public override async Task<ICollection<Groups>> GetAllAsync() {
            return await Context.Groups.Include(e => e.Company)
                            .ToListAsync();
        }

        public async Task<ICollection<Reservations>> GetGroupsByReservationStartDate(DateTime reservationStartDate) {
            return await Context.Reservations
                .Include(e => e.ReservationGroups)
                .ThenInclude(e => e.Group)
                .Where(e => e.StartDate == reservationStartDate)
                .ToListAsync();
        }
    }
}
