using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
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
    }

    public class MockReservationRepository : RepositoryBase<Reservations, Guid>, IReservationRepository
    {
        public MockReservationRepository(HMSDbContext context)
            : base(context) {
        }

        public override async Task<ICollection<Reservations>> GetAllAsync() {
            await base.GetAllAsync();

            return new List<Reservations>()
            {
                new Reservations() { Id = Guid.NewGuid(), Code = "Sample", HotelId = Guid.NewGuid(),TotalCost = 98400},
                new Reservations() { Id = Guid.NewGuid(), Code = "Data", HotelId = Guid.NewGuid(),TotalCost = 98400},
                new Reservations() { Id = Guid.NewGuid(), Code = "Mock", HotelId = Guid.NewGuid(),TotalCost = 98400}
            };
        }
    }
}
