namespace RegistryManipulationTests
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RegistryManipulationDll.Components;

    [TestClass]
    public class RegistryModifierTests
    {
        [TestMethod]
        public void ChangeRegistryTest()
        {
            var finder = new RegistryFinder();
            var modifier = new RegistryModifier();

            RegistryModel registry = new RegistryModel
            {
                SubKeysSeparatedBySlashes = "Control Panel/Desktop",
                RegistryName = "LogPixels"
            };


            var beforeValue = new RegistryFinder().GetValueFrom(registry);
            modifier.Set("60", registry);
            var afterValue = new RegistryFinder().GetValueFrom(registry);

            Assert.IsTrue((string)afterValue == "60");
            modifier.Set("96", registry);

            afterValue = new RegistryFinder().GetValueFrom(registry);
            Assert.IsTrue((string)afterValue == "96");
        }

        [TestMethod]
        public void CreationRegistryTest()
        {
            RegistryModel registry = new RegistryModel();
            registry.SubKeysSeparatedBySlashes = "HKEY_CURRENT_USER/SOFTWARE/RegistryManipulationAPI";
            registry.RegistryName = "TestRegistry";

            var modifier = new RegistryModifier();
            modifier.Create(true, registry);
        }
    }
}
