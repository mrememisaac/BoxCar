﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxCar.ShoppingBasket.Profiles
{
    public class BasketLineProfile : Profile
    {
        public BasketLineProfile()
        {
            CreateMap<Models.BasketLineForCreation, Entities.BasketLine>()
                .ForMember(d => d.UnitPrice, opts => opts.MapFrom(src => src.Price))
                .ReverseMap();
            CreateMap<Models.BasketLineForUpdate, Entities.BasketLine>();
            CreateMap<Entities.BasketLine, Models.BasketLine>().ReverseMap();

        }
    }
}
