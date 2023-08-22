using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using System;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricDto, CpuMetricDal>()
            .ForMember(dal => dal.Time, item => item.MapFrom(dto => dto.Time.ToUnixTimeMilliseconds()));

            CreateMap<CpuMetricDal, CpuMetricDto>()
            .ForMember(dto => dto.Time, item => item.MapFrom(dal => DateTimeOffset.FromUnixTimeMilliseconds(dal.Time)));

            CreateMap<CpuMetricCreateRequest, CpuMetricDto>();                    
        }
    }
}
