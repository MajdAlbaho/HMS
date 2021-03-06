﻿using AutoMapper;
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
using HMS.Models.Enum;
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
                .Distinct()
                .ToListAsync();

            return await Context.Rooms
                .Where(e => !reservedRooms.Contains(e))
                .ToListAsync();
        }

        public async Task<Reservations> SaveReservation(Reservations reservation, List<Persons> persons) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    Context.Entry(reservation).State = EntityState.Added;

                    foreach (var reservationGroup in reservation.ReservationGroups) {
                        Context.Entry(reservationGroup).State = EntityState.Added;
                    }
                    Context.ReservationRooms.AddRange(reservation.ReservationRooms);

                    reservation.UserId = "47009186-d2a8-426d-8ad7-af784ee8bb5d";

                    await Context.SaveChangesAsync();

                    transaction.Commit();

                    return await Context.Reservations
                        .Include(e => e.Status)
                        .Include(e => e.ReservationRooms)
                        .ThenInclude(e => e.Room)
                        .Include(e => e.ReservationGroups)
                        .ThenInclude(e => e.Group)
                        .FirstOrDefaultAsync(e => e.Id == reservation.Id);

                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task ChangeStatus(Guid id, StatusEnum status) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    var reservation =
                        await Context.Reservations.FirstOrDefaultAsync(e => e.Id == id);
                    reservation.StatusId = (int)status;
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
