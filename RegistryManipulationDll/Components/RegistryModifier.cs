namespace RegistryManipulationDll.Components
{
    using Contracts;
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;

    public class RegistryModifier : IRegistryModifier
    {
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

        }
    }
}
