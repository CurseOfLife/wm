using Api.Models;
using Api.Models.Create;
using AutoMapper;
using Domain;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            //Measurement Profile
            CreateMap<Measurement, MeasurementDTO>().ReverseMap();
            CreateMap<Measurement, CreateMeasurementDTO>().ReverseMap();

            //MeasuringPoint Profile
            CreateMap<MeasuringPoint, MeasuringPointDTO>().ReverseMap();
            CreateMap<MeasuringPoint, CreateMeasurementDTO>().ReverseMap();

            //ReadingStatus Profile
            CreateMap<ReadingStatus, ReadingStatusDTO>().ReverseMap();
            CreateMap<ReadingStatus, CreateReadingStatusDTO>().ReverseMap();

            //Route Profile
            CreateMap<Route, RouteDTO>().ReverseMap();
            CreateMap<Route, RouteDTO>().ReverseMap();

            //WaterMeter Profile
            CreateMap<WaterMeter, WaterMeterDTO>().ReverseMap();
            CreateMap<WaterMeter, CreateWaterMeterDTO>().ReverseMap();

            //User Profile
            CreateMap<ApiUser, UserDTO>().ReverseMap();
            //no need to create a LoginUserDTO map because


        }
    }
}
