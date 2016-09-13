namespace RegistryManipulationDll.Contracts
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;

    public interface IRegistryModifier
    {
        /// <summary>
        /// Sets the specified value into the specified registry key
        /// </summary>
        /// <param name="value">the new value for the registry</param>
        /// <param name="registry">registry to modify.</param>
        void Set(object value, RegistryModel registry);

        void Create(object value, RegistryModel registry);
    }
}
