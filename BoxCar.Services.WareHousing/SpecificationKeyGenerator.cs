namespace BoxCar.Services.WareHousing
{
    public static class SpecificationKeyGenerator
    {
        /// <summary>
        /// Standard way to generate vehicle specification key for quickly confirming if we have an exact match
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="chassisId"></param>
        /// <param name="engineId"></param>
        /// <param name="optionPackId"></param>
        /// <returns>string in the format $"VehicleId-{vehicleId}-ChassisId-{chassisId}-EngineId-{engineId}-OptionPack-{optionPackId}";</returns>
        public static string GenerateSpecificationKey(Guid vehicleId, Guid chassisId, Guid engineId, Guid optionPackId)
        {
            return $"VehicleId-{vehicleId}-ChassisId-{chassisId}-EngineId-{engineId}-OptionPack-{optionPackId}";
        }
    }
}
