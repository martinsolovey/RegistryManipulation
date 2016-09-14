namespace RegistryManipulationDll.Contracts
{
    using HirokuScript.RegistryInteraction.Models;

    public interface IRegistryModifier
    {
        /// <summary>
        /// Sets the specified value into the specified registry key
        /// </summary>
        /// <param name="value">the new value for the registry</param>
        /// <param name="registry">registry to modify.</param>
        void Set(object value, RegistryModel registry);

        /// <summary>
        /// Create the specified Registry Key & Subkeys (if not existing) and sets the specified value.
        /// </summary>
        /// <param name="value">The value for the Key to be created</param>
        /// <param name="registry">The Registry Model Containing RegistryName and SubKey</param>
        void Create(object value, RegistryModel registry);
    }
}
