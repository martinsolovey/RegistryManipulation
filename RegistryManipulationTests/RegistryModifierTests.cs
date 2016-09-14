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
                SubKeySeparatedByBackSlashes = "Control Panel\\Desktop",
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
            registry.RegistryName = "TestRegistry";
            registry.SubKeySeparatedByBackSlashes = "HKEY_CURRENT_USER\\SOFTWARE\\RegistryManipulationAPI";

            var modifier = new RegistryModifier();

            Assert.IsFalse(registry.IsRegistryReal);

            modifier.Create(new
            {
                Installed = true
            }, registry);

            Assert.IsTrue(registry.IsRegistryReal);

            registry.SubKey.DeleteEntireTree();
        }
    }
}
