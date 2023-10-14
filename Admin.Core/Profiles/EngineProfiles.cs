using Admin.Core.Features.Engines.AddEngine;
using Admin.Core.Features.Engines.GetEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Profiles
{
    public class EngineProfiles : Profile
    {
        public EngineProfiles()
        {
            CreateMap<AddEngineDto, AddEngineCommand>().ReverseMap();
            CreateMap<Engine, AddEngineCommand>().ReverseMap();
            CreateMap<AddEngineResponse, Engine>().ReverseMap();
            CreateMap<GetEngineByIdResponse, Engine>().ReverseMap();
        }
    }
}
