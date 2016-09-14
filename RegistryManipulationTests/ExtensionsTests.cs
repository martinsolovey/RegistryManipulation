namespace RegistryManipulationTests
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Win32;
    using RegistryManipulationDll.Components;

    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void GoBackTest()
        {
            RegistryModel model = new RegistryModel("HKEY_CURRENT_USER\\SOFTWARE\\Google\\Chrome", false);

            var subKey = model.SubKey.GoBack();

            Assert.IsTrue(subKey.Name == "HKEY_CURRENT_USER\\SOFTWARE\\Google");
        }

        [TestMethod]
        public void TryGetSubKey()
        {
            RegistryModel model = new RegistryModel("HKEY_CURRENT_USER\\SOFTWARE\\Google", false);

            RegistryKey chromeSubKey;
            bool gotSubKey = model.SubKey.TryGetSubKey("Chrome", out chromeSubKey);

            Assert.IsNotNull(chromeSubKey);
            Assert.IsTrue(gotSubKey);
        }

        [TestMethod]
        public void TryGetValue()
        {
            RegistryModel model = new RegistryModel("Control Panel\\Desktop", true);

            object logPixelsValue;
            bool gotValue = model.SubKey.TryGetValue("LogPixels", out logPixelsValue);

            Assert.IsNotNull(logPixelsValue);
            Assert.IsTrue(gotValue);
        }
    }
}
