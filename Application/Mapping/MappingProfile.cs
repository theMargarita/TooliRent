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
            //CreateMap<Customer, CustomerReadDTO>()
            //    .ForMember(dto => dto.FullName, option => option.MapFrom(c => $"{c.FirstName} {c.LastName}"));
        }

    }
}
