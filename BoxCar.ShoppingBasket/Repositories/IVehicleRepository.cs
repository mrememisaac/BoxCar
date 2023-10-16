﻿using System;
using System.Threading.Tasks;
using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Repositories
{
    public interface IVehicleRepository
    {
        void AddVehicle(Vehicle vehicle);
        Task<bool> VehicleExists(Guid id);
        Task<bool> SaveChanges();
    }
}