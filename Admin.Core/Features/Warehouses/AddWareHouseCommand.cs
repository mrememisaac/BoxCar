﻿using MediatR;

namespace Admin.Core.Features.Factories.AddWareHouse
{
    public class AddWareHouseCommand : IRequest<Result<AddWareHouseCommand>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public AddressDto Address { get; set; }
    }
}
