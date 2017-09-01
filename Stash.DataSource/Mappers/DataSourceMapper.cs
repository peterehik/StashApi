using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Stash.DataSource.Entities;

namespace Stash.DataSource.Mappers
{
    public class DataSourceMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Users, Users>();
        }
    }
}
