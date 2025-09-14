using AutoMapper;
using Domain.Core.Models;
using Infrastructure.Models;
using Services.DTOs.CustomerDtos;
using Services.DTOs.RentalDtos;
using Services.DTOs.ToolDtos;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        //domain model
        //entity to DTO
        public MappingProfile() 
        {
            //mapping for tools
            CreateMap<Tool, ToolReadDto>()
                .ForMember(dto => dto.CategoryName, option => option.MapFrom(t => t.Category.Name))
                .ForMember(dto => dto.CategoryId, option => option.MapFrom(t => t.Category.CategoryId))
                .ForMember(dto => dto.IsAvailable, option => option.MapFrom(a => a.QuantityInStock > 0)); 

            CreateMap<ToolCreateDto, Tool>();
            CreateMap<ToolUpdateDto, Tool>();

            //mapping for customers
            CreateMap<Customer, CustomerReadDto>()
                .ForMember(dto => dto.FullName, option => option.MapFrom(c => $"{c.FirstName} {c.LastName}"));

            CreateMap<CustomerCreateDto, CustomerReadDto>();
            CreateMap<CustomerUpdateDto, CustomerUpdateDto>();

            //mapping for rentals
            CreateMap<Rental, RentalReadDto>()
                .ForMember(dto => dto.CustomerName, option => option.MapFrom(r => $"{r.Customer.FirstName} {r.Customer.LastName}"))
                .ForMember(dto => dto.CustomerId, option => option.MapFrom(r => r.Customer.CustomerId))
                .ForMember(dto => dto.RentalDetails.Select(r => r.ToolName), option => option.MapFrom(r => r.Tools))
                .ForMember(dto => dto.RentalDetails, option => option.MapFrom(r => r.OrderDetails));
        }

    }
}
