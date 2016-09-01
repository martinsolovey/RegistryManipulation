namespace RegistryManipulationDll.Contracts
{
    public interface IRegistryFileHandler
    {
        /// <summary>
        /// Executes a reg file with regedit, changing all the values of the specified keys.
        /// </summary>
        /// <param name="regFilePath">Path to the .reg file.</param>
        void ExecuteRegistryFile(string regFilePath);

        /// <summary>
        /// Backups the current values of all the keys and subkeys on HKEY_CURRENT_USER on a .reg file.
        /// </summary>
        /// <param name="savePath">Path to where you want to save the .reg exported file.</param>
        void BackupCurrentUserRegistry(string savePath);
    }
}
