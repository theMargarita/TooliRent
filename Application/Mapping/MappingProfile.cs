using AutoMapper;
using Domain.Core.Models;
using Infrastructure.Models;
using Services.DTOs;
using Services.DTOs.CategoryDto;
using Services.DTOs.CustomerDtos;
using Services.DTOs.RentalDtos;
using Services.DTOs.ToolDtos;

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

        // CUSTOMERS
        CreateMap<Customer, CustomerReadDto>()
            .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"));
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerCreateDto, Customer>()
            .ForMember(dto => dto.BirthDate, opt => opt.MapFrom(c => c.BirthDate));
        CreateMap<CustomerUpdateDto, Customer>();

        // RENTALS
        CreateMap<RentalCreateDto, Rental>()
            //.ForMember(dest => dest.RentalId, opt => opt.Ignore())
            .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.TotalQuantity, opt => opt.Ignore()) // calculated in service
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // calculated in service
            .ForMember(dest => dest.RentalDetails, opt => opt.MapFrom(src => src.RentalDetails));

        CreateMap<Rental, RentalReadDto>()
            .ForMember(dest => dest.CustomerName,
                      opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : ""))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.RentalDetails, opt => opt.MapFrom(src => src.RentalDetails))
            .ForMember(dto => dto.StartDate, opt => opt.MapFrom(r => r.RentalDate))
            .ForMember(dto => dto.EndDate, opt => opt.MapFrom(r => r.ReturnDate));

        CreateMap<Rental, RentalUpdateDto>().ForMember(dest => dest.NewReturnDate,
                                                  opt => opt.MapFrom(src => src.RentalDate));
        CreateMap<Rental, ReturnRentalDto>()
            .ForMember(r => r.RentedAt, opt => opt.MapFrom(src => src.RentalDate))
            .ForMember(r => r.ReturnDate, o => o.MapFrom(s => s.ReturnDate));
            //.ForMember(r => r.IsReturned == true, o => o.MapFrom(s => s.TotalQuantity));

        // RENTAL DETAILS
        CreateMap<RentalDetailCreateDto, RentalDetail>()
            .ForMember(dest => dest.PricePerDay, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

        CreateMap<RentalDetail, RentalDetailReadDto>()
            .ForMember(dest => dest.ToolName,
                      opt => opt.MapFrom(src => src.Tool != null ? src.Tool.Name : ""));

        CreateMap<RentalDetail, RentalDetailDTO>();
    }
}