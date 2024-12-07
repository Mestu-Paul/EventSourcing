using AutoMapper;
using EventSourcingApi.Entities;
using Orders.Api.Dtos;

namespace Orders.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderForCreateDto>().ReverseMap();
            CreateMap<Order, OrderForUpdateDto>().ReverseMap();
            CreateMap<Order, OrderForReturnDto>().ReverseMap();
        }
    }
}
