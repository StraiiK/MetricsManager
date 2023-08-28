using AutoMapper;
using MetricsManager.DAL;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;
using MetricsManager.Requests;
using MetricsManager.Responses;
using System;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricDto, CpuMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<CpuMetricDal, CpuMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<AllCpuMetricsApiResponse, CpuMetricDto>();

            CreateMap<DotNetMetricDto, DotNetMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<DotNetMetricDal, DotNetMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<AllDotNetMetricsApiResponse, DotNetMetricDto>();

            CreateMap<NetworkMetricDto, NetworkMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<NetworkMetricDal, NetworkMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<AllNetworkMetricsApiResponse, NetworkMetricDto>();

            CreateMap<RamMetricDto, RamMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<RamMetricDal, RamMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<AllRamMetricsApiResponse, RamMetricDto>();

            CreateMap<RomMetricDto, RomMetricDal>()
                .ForMember(dal => dal.Time, item => item.MapFrom(dto => UnixTimeConverter.ToUnixTime(dto.Time)));
            CreateMap<RomMetricDal, RomMetricDto>()
                .ForMember(dto => dto.Time, item => item.MapFrom(dal => UnixTimeConverter.FromUnixTime(dal.Time)));
            CreateMap<AllRomMetricsApiResponse, RomMetricDto>();

            CreateMap<RegisterAgentRequest, AgentDto>();
            CreateMap<AgentDto, AgentDal>();
            CreateMap<AgentDal, AgentDto>();
        }
    }
}
