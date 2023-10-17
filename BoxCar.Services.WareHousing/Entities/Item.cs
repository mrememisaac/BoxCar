namespace BoxCar.Services.WareHousing.Entities
{
    /// <summary>
    /// Represents a Vehicle or an  Engine or a Chassis or an OptionPack or an Option
    /// </summary>
    public class Item
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Can contain Vehicle or an  Engine or a Chassis or an OptionPack or an Option
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Can be a Vehicle,  Engine, Chassis, OptionPack, Option
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Corresponds to the VehicleId, EngineId, ChassisId, OptionPackId, OptionId
        /// </summary>
        public Guid ItemTypeId { get; set; }

        /// <summary>
        /// String representation of the vehicles exact specs in terms of Chassis Id, EngineId, OptionPackId
        /// var specificationKey = $"VehicleId-{vehicleId}-ChassisId-{chassisId}-EngineId-{engineId}-OptionPack-{optionPackId}"
        /// </summary>
        public string SpecificationKey { get; set; } = null!;

        public int Quantity { get; set; }
        
        //private readonly List<ItemInstance> _instances = new();

        //public IEnumerable<ItemInstance> Instances => _instances.ToList();

        //public void AddInstance(ItemInstance instance)
        //{
        //    _instances.Add(instance);
        //}

        //public void RemoveInstance(ItemInstance instance)
        //{
        //    _instances.Remove(instance);
        //}
    }
}
