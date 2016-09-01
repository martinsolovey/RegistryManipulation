namespace RegistryManipulationTests
{
    using HirokuScript.RegistryInteraction.Components;
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Win32;

    [TestClass]
    public class RegistryFinderTests
    {
        [TestMethod]
        public void GetStraightValue()
        {
            RegistryFinder finder = new RegistryFinder();
            RegistryModel registry = new RegistryModel
            {
                SubKeysSeparatedBySlashes = "Control Panel/Desktop",
                RegistryName = "LogPixels"
            };

            object value = finder.GetValueFrom(registry, Registry.CurrentUser);

            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void GetValueByRecursivity()
        {
            RegistryFinder finder = new RegistryFinder();
            RegistryModel registry = new RegistryModel
            {
                SubKeysSeparatedBySlashes = "Control Panel/Desktop",
                RegistryName = "LogPixels"
            };

            object value = finder.GetValueFrom(registry);

            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void GetValueByRecursivity2()
        {
            RegistryFinder finder = new RegistryFinder();
            RegistryModel registry = new RegistryModel
            {
                RegistryName = "LogPixels"
            };

            object value = finder.GetValueFrom(registry);

            Assert.IsNotNull(value);
        }
    }
}
