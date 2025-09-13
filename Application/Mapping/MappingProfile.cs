using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Models;
using Services.DTOs;


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

            CreateMap<CreateToolDto, Tool>();
            CreateMap<ToolUpdateDto, Tool>();

            //CreateMap<Customer, CustomerReadDTO>()
            //    .ForMember(dto => dto.FullName, option => option.MapFrom(c => $"{c.FirstName} {c.LastName}"));
        }

    }
}
