namespace RegistryManipulationDll.Components
{
    using Contracts;
    using System;
    using System.Diagnostics;

    public class RegistryFileHandler : IRegistryFileHandler
    {
        public void BackupCurrentUserRegistry(string savePath)
        {
            Process proc = new Process();

            try
            {
                proc.StartInfo.FileName = "regedit.exe";
                proc.StartInfo.UseShellExecute = false;

                proc = Process.Start("regedit.exe", "/e " + savePath + " HKEY_CURRENT_USER");
                proc.WaitForExit();
            }
            catch (Exception)
            {
                proc.Dispose();
            }
        }

        public void ExecuteRegistryFile(string regFilePath)
        {
            Process regeditProcess = Process.Start("regedit.exe", string.Format("/s {0}", regFilePath));
            regeditProcess.WaitForExit();
        }
    }
}
