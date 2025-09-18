using AutoMapper;
using Domain.Core.Models;
using Infrastructure.Models;
using Services.DTOs;
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
            CreateMap<Tool, ToolDto>();

            //mapping for customers
            CreateMap<Customer, CustomerReadDto>()
                .ForMember(dto => dto.FullName, option => option.MapFrom(c => $"{c.FirstName} {c.LastName}"));

            CreateMap<CustomerCreateDto, CustomerReadDto>();
            CreateMap<CustomerUpdateDto, CustomerUpdateDto>();

            // mapping for rentals
            CreateMap<Rental, RentalReadDto>()
                .ForMember(dto => dto.CustomerName, opt => opt.MapFrom(r => $"{r.Customer.FirstName} {r.Customer.LastName}"))
                .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(r => r.Customer.CustomerId))
                .ForMember(dto => dto.RentalDetails, opt => opt.MapFrom(r => r.OrderDetails));

            //mapping for rental detail
            CreateMap<RentalDetail, RentalDetailDTO>()
                .ForMember(dto => dto.ToolId, opt => opt.MapFrom(od => od.ToolId));
                //.ForMember(dto => dto., opt => opt.MapFrom(od => od.tool));


            CreateMap<RentalCreateDto, Rental>();
            //CreateMap<RentalUpdateDto, Rental>();

        }

    }
}
