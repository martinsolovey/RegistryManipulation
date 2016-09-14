namespace RegistryManipulationTests
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegistryModelTests
    {
        [TestMethod]
        public void ConstructorTest1()
        {
            string path = "HKEY_CURRENT_USER\\Control Panel\\Desktop\\BlockSendInputResets";

            var model = new RegistryModel(path);

            Assert.IsTrue(model.IsRegistryReal);
            Assert.IsTrue(model.IsSubKeyReal);
        }

        [TestMethod]
        public void ConstructorTest2()
        {
            string path = "Control Panel\\Desktop\\BlockSendInputResets";

            var model = new RegistryModel(path);

            Assert.IsTrue(model.IsRegistryReal);
            Assert.IsTrue(model.IsSubKeyReal);
        }
    }
}
