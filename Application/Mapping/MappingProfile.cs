using AutoMapper;
using Infrastructure.Models;
using Services.DTOs.CustomerDtos;
using Services.DTOs.ToolDtos;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        //domain model
        //entity to DTO
        public MappingProfile() 
        {
            CreateMap<Tool, ToolReadDto>()
                .ForMember(dto => dto.CategoryName, option => option.MapFrom(t => t.Category.Name))
                .ForMember(dto => dto.CategoryId, option => option.MapFrom(t => t.Category.CategoryId))
                .ForMember(dto => dto.IsAvailable, option => option.MapFrom(a => a.QuantityInStock > 0)); 
            CreateMap<ToolCreateDto, Tool>();
            CreateMap<ToolUpdateDto, Tool>();


            CreateMap<Customer, CustomerReadDto>()
                .ForMember(dto => dto.FullName, option => option.MapFrom(c => $"{c.FirstName} {c.LastName}"));
            CreateMap<CustomerCreateDto, CustomerReadDto>();
            CreateMap<CustomerUpdateDto, CustomerUpdateDto>();
        }

    }
}
