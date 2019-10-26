using AutoMapper;
using HMS.Api.Repositories.HMSDb;
using HMS.Models;
using Status = HMS.Api.Repositories.HMSDb.Status;

namespace HMS.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Reservation, Reservations>()
                .ReverseMap()
                .ForMember(e => e.Nights, e => e.Ignore())
                .ForMember(e => e.Rooms, e => e.Ignore())
                .ForMember(e => e.Guests, e => e.Ignore());

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
