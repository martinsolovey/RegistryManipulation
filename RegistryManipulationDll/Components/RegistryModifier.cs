namespace RegistryManipulationDll.Components
{
    using Contracts;
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;
    using System.Linq;

    public class RegistryModifier : IRegistryModifier
    {
        /// <summary>
        /// Sets the specified value into the specified registry key
        /// </summary>
        /// <param name="value">the new value for the registry</param>
        /// <param name="registry">registry to modify.</param>
        public void Set(object value, RegistryModel registry)
        {
            try
            {
                //TODO: Pick the proper type set depending the object.
                var type = RegistryValueKind.String;

                registry.SubKey.SetValue(registry.RegistryName, value, type);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create the specified Registry Key & Subkeys (if not existing) and sets the specified value.
        /// </summary>
        /// <param name="value">The value for the Key to be created</param>
        /// <param name="registry">The Registry Model Containing RegistryName and SubKey</param>
        public void Create(object value, RegistryModel registry)
        {
            if (registry.IsRegistryReal)
                return;

            //If the subKey exists, let's create our entry.
            if (registry.IsSubKeyReal)
            {
                registry.SubKey.CreateSubKey(registry.RegistryName, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
                registry.SubKey.SetValue(registry.RegistryName, value);
            }
            else
            {
                //Let's create the Sub SubKey before.
                string lastRealSubKeyName = registry.LastRealSubKey.Name;
                var subKeyToCreate = registry.SubKeySeparatedByBackSlashes.Remove(0, lastRealSubKeyName.Count());

                while (subKeyToCreate[0] == '\\')
                    subKeyToCreate = subKeyToCreate.Remove(0, 1);

                registry.LastRealSubKey.CreateSubKey(subKeyToCreate, RegistryKeyPermissionCheck.ReadWriteSubTree);
                Create(value, registry);
            }
        }
    }
}
