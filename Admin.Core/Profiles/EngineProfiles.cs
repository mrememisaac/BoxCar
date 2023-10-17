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
            CreateMap<AddEngineCommand, Engine>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddEngineResponse, Engine>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<GetEngineByIdResponse, Engine>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<EngineDto, Engine>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Features.Vehicles.AddVehicle.EngineDto, Engine>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Engine, EngineAddedEvent>()
                .ForMember(d => d.EngineId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Engine, Features.Engines.ListEngines.EngineQueryItem>();
            CreateMap<Engine, Features.Vehicles.ListVehicles.EngineQueryItem>();
        }
    }
}
