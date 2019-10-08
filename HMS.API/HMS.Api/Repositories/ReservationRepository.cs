using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservations = HMS.Api.Repositories.HMSDb.Reservations;

namespace HMS.Api.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservations, Guid>, IReservationRepository
    {
        public ReservationRepository(HMSDbContext context)
            : base(context) {
        }

        public override async Task<ICollection<Reservations>> GetAllAsync() {
            return await Context.Reservations
                .Include(e => e.Status)
                .Include(e => e.ReservationRooms)
                .Include(e => e.ReservationGroups)
                .ThenInclude(e => e.Group)
                .ToListAsync();
        }
    }
}
