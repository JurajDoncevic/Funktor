using AutoMapper;
using Funktor.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funktor.WpfApp.Utils
{
    public static class MapperSingleton
    {
        private static IMapper _mapper;
        
        public static IMapper GetMapperInstance()
        {
            if(_mapper == null)
            {
                _mapper = new MapperConfiguration(_ =>
                {
                    _.CreateMap<MappingItem, Funktor.Core.Configuration.MappingItem>()
                     .ReverseMap();
                    _.CreateMap<MappingConfiguration, Funktor.Core.Configuration.MappingConfiguration>()
                     .ForMember(_ => _.ItemMappings, opt => opt.MapFrom(src => src.MappingItems))
                     .ReverseMap();
                }).CreateMapper();
            }
            return _mapper;

        }
    }
}
