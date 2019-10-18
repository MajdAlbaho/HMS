using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Repositories
{
    public class RoomRepository : RepositoryBase<Rooms, Guid>, IRoomRepository
    {
        public RoomRepository(HMSDbContext context)
            : base(context) {
        }

        public override async Task<ICollection<Rooms>> GetAllAsync() {
            var rooms = await Context.Rooms
                .ToListAsync();
            var reservations = await Context.ReservationRooms
                .Include(e => e.Reservation)
                .Include(e => e.Person)
                .Where(e => e.Reservation.EndDate >= DateTime.Now.Date && e.Reservation.StartDate <= DateTime.Now.Date)
                .ToListAsync();

            foreach (var room in rooms) {
                room.ReservationRooms
                    = reservations
                        .Where(e => e.RoomId == room.Id)
                        .ToList();
            }

            return rooms;
        }
    }
}
