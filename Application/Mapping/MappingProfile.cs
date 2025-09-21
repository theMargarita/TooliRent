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
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
                 .ForMember(dto => dto.CategoryId, opt => opt.MapFrom(t => t.Category.CategoryId))
                .ForMember(dto => dto.IsAvailable, opt => opt.MapFrom(t => t.QuantityInStock > 0));

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
                .ForMember(dto => dto.RentalDetails, opt => opt.MapFrom(r => r.RentalDetails));

            CreateMap<RentalCreateDto, Rental>();
            // CreateMap<RentalUpdateDto, Rental>(); 


            CreateMap<RentalDetail, RentalDetailDTO>()
                .ForMember(dto => dto.ToolId, opt => opt.MapFrom(od => od.ToolId));

            CreateMap<RentalCreateDto, Rental>()
            .ForMember(dest => dest.RentalId, opt => opt.Ignore())
            .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.TotalQuantity, opt => opt.Ignore()) // will be calculated in service
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // will be calculated in service
            .ForMember(dest => dest.RentalDetails, opt => opt.MapFrom(src => src.RentalDetails));

            CreateMap<Rental, RentalReadDto>()
                .ForMember(dest => dest.CustomerName,
 opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : ""));

            CreateMap<RentalDetailCreateDto, RentalDetail>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RentalId, opt => opt.Ignore()) 
                .ForMember(dest => dest.PricePerDay, opt => opt.Ignore()) 
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());
                //.ForMember(dest => dest.CustomerId, opt => opt.Ignore()); 

            CreateMap<RentalDetail, RentalDetailReadDto>()
                .ForMember(dest => dest.ToolName,
                          opt => opt.MapFrom(src => src.Tool != null ? src.Tool.Name : ""));

        }
    }
}
