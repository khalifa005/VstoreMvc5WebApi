using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vstore.Dtos;
using Vstore.Models;

namespace Vstore.App_Start
{
    public class MappingProfile :Profile
    {


        public MappingProfile()
        {

            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MembershipType,MembershipTypeDto>();

            Mapper.CreateMap<Genre, GenreDto>();


            Mapper.CreateMap<Movie, MoviesDto>();
            Mapper.CreateMap<MoviesDto, Movie>().ForMember(c => c.Id, opt => opt.Ignore());

        }

    }
}