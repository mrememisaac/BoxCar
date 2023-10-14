using BoxCar.Admin.Core.Features.Engines.AddEngine;
using BoxCar.Admin.Core.Features.Engines.GetEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;

namespace BoxCar.Admin.Core.Profiles
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
