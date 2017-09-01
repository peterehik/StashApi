using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Stash.DataSource.Entities;
using Stash.DataSource.Mappers;
using Stash.Services.Mappers;
using StashWebApiApp.Models;

namespace StashWebApiApp.Mappers
{
    public class WebMapperConfiguration
    {
        public void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<WebMapperProfile>();
                cfg.AddProfile<ServiceMapperProfile>();
                cfg.AddProfile<DataSourceMapperProfile>();
            });
        }
    }

    internal class WebMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<CreateUserBindingModel, Users>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.MetaData, opt => opt.MapFrom(src => src.MetaData))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.full_name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phone_number));

        }
    }
}