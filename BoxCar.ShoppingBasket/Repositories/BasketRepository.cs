﻿using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxCar.ShoppingBasket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShoppingBasketDbContext _shoppingBasketDbContext;

        public BasketRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            _shoppingBasketDbContext = shoppingBasketDbContext;
        }         

        public async Task<Basket> GetBasketById(Guid basketId)
        {
            return await _shoppingBasketDbContext.Baskets.Include(sb => sb.BasketLines)
                .Where(b => b.BasketId == basketId).FirstOrDefaultAsync();
        }

        public async Task<bool> BasketExists(Guid basketId)
        {
            var exists = await _shoppingBasketDbContext.Baskets
                .AnyAsync(b => b.BasketId == basketId);
            return exists;
        }

        public void AddBasket(Basket basket)
        {
            _shoppingBasketDbContext.Baskets.Add(basket);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingBasketDbContext.SaveChangesAsync() > 0);
        }

        public async Task ClearBasket(Guid basketId)
        {
            var basketLinesToClear = _shoppingBasketDbContext.BasketLines.Where(b => b.BasketId == basketId);
            _shoppingBasketDbContext.BasketLines.RemoveRange(basketLinesToClear);

            await SaveChanges();
        }
    }
}
