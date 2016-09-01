namespace HirokuScript.RegistryInteraction.Contracts
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;

    /// <summary>
    /// Service to perform search within the Windows Registry.
    /// </summary>
    public interface IRegistryFinder
    {
        /// <summary>
        /// Retrieves the value of a RegistryModel within the specified RegistryKey.
        /// Ex: RegistryKey is used as a startpoint, for example Registry.CurrentUser
        /// </summary>
        /// <param name="startPoint">RegistryKey or Folder to start the search, Example: Registry.LocalMachine</param>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <returns>The value of the registry, if there is no registry, it retrieves null.</returns>
        object GetValueFrom(RegistryModel registry, RegistryKey startPoint = null);
    }
}
