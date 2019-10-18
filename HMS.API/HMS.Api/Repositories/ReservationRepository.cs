using HMS.Api.Models.parameters;
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

        public async Task<IEnumerable<Rooms>> CheckReservation(CheckReservation checkReservation) {
            var reservedRooms = await Context.ReservationRooms
                .Include(e => e.Reservation)
                .Select(e => e.Room)
                .ToListAsync();

            return await Context.Rooms
                .Where(e => !reservedRooms.Contains(e))
                .ToListAsync();
        }
    }
}
