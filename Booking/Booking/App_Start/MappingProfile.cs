using AutoMapper;
using Booking.Models;
using Booking.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
        }
    }
}
