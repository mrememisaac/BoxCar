using AutoMapper;
using BoxCar.Admin.Tests.FactoryTests;

public class MappingProfileTests : FactoryTestsBase
{
    [Fact]
    public void ValidateMappingConfigurationTest()
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}