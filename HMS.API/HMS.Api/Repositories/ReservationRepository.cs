using AutoMapper;
using HMS.Api.Models.parameters;
using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
using HMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Rooms>> CheckReservation(Reservation reservation) {
            var reservedRooms = await Context.ReservationRooms
                .Include(e => e.Reservation)
                .Where(e => (reservation.StartDate >= e.Reservation.StartDate &&
                             reservation.StartDate < e.Reservation.EndDate) ||
                            (reservation.EndDate >= e.Reservation.StartDate &&
                             reservation.EndDate < e.Reservation.EndDate))
                .Select(e => e.Room)
                .ToListAsync();

            return await Context.Rooms
                .Where(e => !reservedRooms.Contains(e))
                .ToListAsync();
        }

        public async Task SaveReservation(Reservations reservation, List<Persons> persons, Guid roomId) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    await Context.Reservations.AddAsync(reservation);
                    foreach (var person in persons) {
                        await Context.Persons.AddAsync(person);
                        await Context.SaveChangesAsync();

                        await Context.ReservationRooms.AddAsync(new ReservationRooms() {
                            ReservationId = reservation.Id,
                            RoomId = roomId,
                            PersonId = person.Id
                        });

                        await Context.SaveChangesAsync();
                    }

                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
