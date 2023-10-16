﻿using BoxCar.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.DbContexts
{
    public class ShoppingBasketDbContext : DbContext
    {
        public ShoppingBasketDbContext(DbContextOptions<ShoppingBasketDbContext> options)
        : base(options)
        {
        }

        public DbSet<BasketChangeEvent> BasketChangeEvents { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketLine> BasketLines { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Chassis> Chassis { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<OptionPack> OptionPacks { get; set; }

    }
}
