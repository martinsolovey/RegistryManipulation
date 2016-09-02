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
    }
}
