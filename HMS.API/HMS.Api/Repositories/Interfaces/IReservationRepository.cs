using HMS.Api.Models.parameters;
using HMS.Api.Repositories.HMSDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Repositories.Interfaces
{
    public interface IReservationRepository : IRepositoryBase<Reservations, Guid>
    {
        Task<IEnumerable<Rooms>> CheckReservation(Reservation reservation);
    }
}
