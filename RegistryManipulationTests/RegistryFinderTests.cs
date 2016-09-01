namespace RegistryManipulationTests
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Win32;
    using RegistryManipulationDll.Components;

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
        public void GetStraightKey()
        {
            RegistryFinder finder = new RegistryFinder();
            RegistryModel registry = new RegistryModel
            {
                SubKeysSeparatedBySlashes = "Control Panel/Desktop",
                RegistryName = "LogPixels"
            };

            RegistryKey value = finder.GetRegistryKeyFor(registry, Registry.CurrentUser);

            Assert.IsTrue(value.ContainsKey(registry.RegistryName));
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
        public void GetRegistryKeyByRecursivity()
        {
            RegistryFinder finder = new RegistryFinder();
            RegistryModel registry = new RegistryModel
            {
                SubKeysSeparatedBySlashes = "Control Panel/Desktop",
                RegistryName = "LogPixels"
            };

            RegistryKey value = finder.GetRegistryKeyFor(registry);

            Assert.IsTrue(value.ContainsKey(registry.RegistryName));
        }

        [TestMethod]
        public void GetRegistryKeyByRecursivity2()
        {
            RegistryFinder finder = new RegistryFinder();
            RegistryModel registry = new RegistryModel
            {
                RegistryName = "LogPixels"
            };

            RegistryKey value = finder.GetRegistryKeyFor(registry);

            Assert.IsTrue(value.ContainsKey(registry.RegistryName));
        }
    }
}
