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
using Status = HMS.Api.Repositories.HMSDb.Status;

namespace HMS.Api.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservations, Guid>, IReservationRepository
    {
        private readonly IMapper _mapper;

        public ReservationRepository(HMSDbContext context, IMapper mapper)
            : base(context) {
            _mapper = mapper;
        }

        public override async Task<ICollection<Reservations>> GetAllAsync() {
            return await Context.Reservations
                .Include(e => e.Status)
                .Include(e => e.ReservationRooms)
                .ThenInclude(e => e.Room)
                .Include(e => e.ReservationGroups)
                .ThenInclude(e => e.Group)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rooms>> CheckReservation(Reservation reservation) {
            var reservedRooms = await Context.ReservationRooms
                .Include(e => e.Reservation)
                .Where(e => (reservation.StartDate >= e.Reservation.StartDate &&
                             reservation.StartDate <= e.Reservation.EndDate) ||
                            (reservation.EndDate >= e.Reservation.StartDate &&
                             reservation.EndDate <= e.Reservation.EndDate))
                .Select(e => e.Room)
                .ToListAsync();

            return await Context.Rooms
                .Where(e => !reservedRooms.Contains(e))
                .ToListAsync();
        }

        public async Task<Reservations> SaveReservation(Reservation reservation, List<Person> persons, Group group = null) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    persons.ForEach(e => e.Id = Guid.NewGuid());

                    var dbReservation = _mapper.Map<Reservations>(reservation);
                    var dbPersons = _mapper.Map<List<Persons>>(persons);

                    await Context.Reservations.AddAsync(dbReservation);
                    await Context.Persons.AddRangeAsync(dbPersons);
                    dbReservation.UserId = "47009186-d2a8-426d-8ad7-af784ee8bb5d";

                    await Context.SaveChangesAsync();

                    if (group != null && !string.IsNullOrEmpty(group.Name)) {
                        if (group.Id == Guid.Empty) {
                            group.CompanyId = Guid.Parse("D78AEBF1-AA9A-472D-B0CC-3BD52917CB05");
                            var dbGroup = _mapper.Map<Groups>(group);
                            await Context.Groups.AddAsync(dbGroup);
                            await Context.SaveChangesAsync();

                            group.Id = dbGroup.Id;
                        }

                        await Context.ReservationGroups.AddAsync(new ReservationGroups() {
                            GroupId = group.Id,
                            ReservationId = dbReservation.Id
                        });
                        await Context.SaveChangesAsync();
                    }

                    await Context.ReservationRooms.AddRangeAsync(persons.Select(e => new ReservationRooms() {
                        ReservationId = dbReservation.Id,
                        PersonId = e.Id,
                        RoomId = e.RoomId
                    }));
                    await Context.SaveChangesAsync();

                    transaction.Commit();

                    return await Context.Reservations
                        .Include(e => e.Status)
                        .Include(e => e.ReservationRooms)
                        .ThenInclude(e => e.Room)
                        .Include(e => e.ReservationGroups)
                        .ThenInclude(e => e.Group)
                        .FirstOrDefaultAsync(e => e.Id == dbReservation.Id);

                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task CheckIn(Guid id) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    var reservation =
                        await Context.Reservations.FirstOrDefaultAsync(e => e.Id == id);
                    reservation.StatusId = 1;
                    await Context.SaveChangesAsync();

                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
