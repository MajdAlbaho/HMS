using HMS.Api.Models.parameters;
using HMS.Api.Repositories.HMSDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Models;
using HMS.Models.Enum;

namespace HMS.Api.Repositories.Interfaces
{
    public interface IReservationRepository : IRepositoryBase<Reservations, Guid>
    {
        Task<IEnumerable<Rooms>> CheckReservation(Reservation reservation);
        Task<Reservations> SaveReservation(Reservations reservation, List<Persons> persons);
        Task ChangeStatus(Guid id, StatusEnum status);
    }
}
