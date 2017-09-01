using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Stash.DataSource.Entities;
using Stash.Services.RequestModels.Users;

namespace Stash.Services.Mappers
{
    public class ServiceMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Users, UserResponseItem>()
                .ForMember(dest => dest.account_key, opt => opt.MapFrom(src => src.AccountKey))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.metadata, opt => opt.MapFrom(src => src.MetaData))
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.full_name, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.phone_number, opt => opt.MapFrom(src => src.PhoneNumber));

        }
    }

}
