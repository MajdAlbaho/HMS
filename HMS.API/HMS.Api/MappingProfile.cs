using AutoMapper;
using HMS.Api.Repositories.HMSDb;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Status = HMS.Api.Repositories.HMSDb.Status;

namespace HMS.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateTwoWayMap<Reservations, Reservation>();
            CreateTwoWayMap<ReservationRooms, ReservationRoom>();
            CreateTwoWayMap<Countries, Country>();
            CreateTwoWayMap<Status, HMS.Models.Status>();
            CreateTwoWayMap<Persons, Person>();
            CreateTwoWayMap<Groups, Group>();
            CreateTwoWayMap<ReservationGroups, ReservationGroup>();
            CreateTwoWayMap<Companies, Company>();
            CreateTwoWayMap<Rooms, Room>();
        }

        private void CreateTwoWayMap<T1, T2>() {
            CreateMap<T1, T2>();
            CreateMap<T2, T1>();
        }
    }
}
