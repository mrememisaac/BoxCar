﻿namespace BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack
{
    public class AddOptionPackDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public IEnumerable<AddOptionDto> Options = new List<AddOptionDto>();
    }
}
