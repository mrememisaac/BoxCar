﻿using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Repositories.Contracts
{
    public interface IChassisRepository
    {
        void AddChassis(Chassis chassis);
        Task<bool> ChassisExists(Guid id);
        Task<bool> SaveChanges();
    }
}