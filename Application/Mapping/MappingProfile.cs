using AutoMapper;
using Domain.Core.Models;
using Infrastructure.Models;
using Services.DTOs;
using Services.DTOs.CategoryDto;
using Services.DTOs.CustomerDtos;
using Services.DTOs.RentalDtos;
using Services.DTOs.ToolDtos;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TOOLS
            CreateMap<Tool, ToolReadDto>()
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                .ForMember(dto => dto.CategoryId, opt => opt.MapFrom(t => t.Category.CategoryId))
                .ForMember(dto => dto.IsAvailable, opt => opt.MapFrom(t => t.QuantityInStock > 0));

            CreateMap<Tool, ToolDto>()
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(t => t.Category.Name));

            CreateMap<ToolCreateDto, Tool>();
            CreateMap<ToolUpdateDto, Tool>();


            // CATEGORIES
            CreateMap<Category, CategoryReadDto>();
            CreateMap<Category, CategoryDTO>();

            //CreateMap<CategoryCreateDto, Category>();
            //CreateMap<CategoryUpdateDto, Category>();


            // CUSTOMERS
            CreateMap<Customer, CustomerReadDto>()
                .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"));
                

            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();


            // RENTALS
            CreateMap<Rental, RentalReadDto>()
                .ForMember(dto => dto.CustomerName, opt => opt.MapFrom(r => $"{r.Customer.FirstName} {r.Customer.LastName}"))
                .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(r => r.Customer.CustomerId))
                .ForMember(dto => dto.RentalDetails, opt => opt.MapFrom(r => r.OrderDetails));

            CreateMap<RentalDetail, RentalDetailDTO>()
                .ForMember(dto => dto.ToolId, opt => opt.MapFrom(od => od.ToolId));

            CreateMap<RentalCreateDto, Rental>();
            // CreateMap<RentalUpdateDto, Rental>(); 
        }
    }
}
