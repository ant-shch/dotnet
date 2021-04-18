using EnumModelBinder.Web.Model;
using System;
using Xunit;

namespace EnumModelBinder.Test
{
    public class CarPartsTests
    {
        [Fact]
        public void CarPartsToCarCatalogContractTest()
        {
            Assert.Equal("ENGINE", CarPartName.Engine.ToCarContract());
        }
    }
}
