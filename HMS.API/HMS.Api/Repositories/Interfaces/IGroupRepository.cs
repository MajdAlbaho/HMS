using HMS.Api.Repositories.HMSDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Repositories.Interfaces
{
    public interface IGroupRepository : IRepositoryBase<Groups, Guid>
    {
        Task<ICollection<Reservations>> GetGroupsByReservationStartDate(DateTime reservationStartDate);
    }
}
