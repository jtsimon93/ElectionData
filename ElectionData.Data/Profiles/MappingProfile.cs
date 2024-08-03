using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data.Profiles
{
    using AutoMapper;
    using ElectionData.Data.Dtos;
    using ElectionData.Data.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CleanPoll, PollDto>();
        }
    }

}
