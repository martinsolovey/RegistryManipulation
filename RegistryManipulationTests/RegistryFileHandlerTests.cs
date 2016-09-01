namespace RegistryManipulationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RegistryManipulationDll.Components;
    using System;
    using System.IO;

    [TestClass]
    public class RegistryFileHandlerTests
    {
        [TestMethod]
        public void BackupTest()
        {
            RegistryFileHandler backuper = new RegistryFileHandler();

            string savePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = Path.Combine(savePath, "backup.reg");

            backuper.BackupCurrentUserRegistry(filePath);
            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
