using AutoMapper;
using HMS.Api.Repositories.HMSDb;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateTwoWayMap<Reservations, Reservation>();
            CreateTwoWayMap<Countries, Country>();
        }

        private void CreateTwoWayMap<T1, T2>() {
            CreateMap<T1, T2>();
            CreateMap<T2, T1>();
        }
    }
}
