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
        /// Retrieves the value of a RegistryModel within the specified RegistryHive.
        /// </summary>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <param name="startPoint">Starting point to look for the specified Registry</param>
        /// <returns>The value of the seeked registry</returns>
        object GetValueFrom(RegistryModel registry, RegistryHive startPoint);

        /// <summary>
        /// Retrieves the value of a RegistryModel within the specified RegistryKey.
        /// Ex: RegistryKey is used as a startpoint, for example Registry.CurrentUser
        /// </summary>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <param name="startPoint">RegistryKey or Folder to start the search, Example: Registry.LocalMachine</param>
        /// <returns>The value of the registry, if there is no registry, it retrieves null.</returns>
        object GetValueFrom(RegistryModel registry, RegistryKey startPoint = null);

        /// <summary>
        /// Retrieves the RegistryKey through the one to get or set a value of the specified registry.
        /// </summary>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <param name="startPoint">Starting point to look for the specified Registry</param>
        /// <returns>The RegistryKey where the seeked registry lies, if there is no registry, it retrieves null.</returns>
        RegistryKey GetRegistryKeyFor(RegistryModel registry, RegistryHive startPoint);

        /// <summary>
        /// Retrieves the RegistryKey through the one to get or set a value of the specified registry.
        /// Ex: RegistryKey is used as a startpoint, for example Registry.CurrentUser
        /// </summary>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <param name="startPoint">RegistryKey or Folder to start the search, Example: Registry.LocalMachine</param>
        /// <returns>The RegistryKey where the seeked registry lies, if there is no registry, it retrieves null.</returns>
        RegistryKey GetRegistryKeyFor(RegistryModel registry, RegistryKey startPoint = null);
    }
}
