using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using System;

namespace MetricsAgent.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricDto, CpuMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<CpuMetricDal, CpuMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<CpuMetricCreateRequest, CpuMetricDto>();

            CreateMap<DotNetMetricDto, DotNetMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<DotNetMetricDal, DotNetMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<DotNetMetricCreateRequest, DotNetMetricDto>();

            CreateMap<NetworkMetricDto, NetworkMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<NetworkMetricDal, NetworkMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<NetworkMetricCreateRequest, NetworkMetricDto>();

            CreateMap<RamMetricDto, RamMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<RamMetricDal, RamMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<RamMetricCreateRequest, RamMetricDto>();

            CreateMap<RomMetricDto, RomMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<RomMetricDal, RomMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<RomMetricCreateRequest, RomMetricDto>();
        }
    }
}
