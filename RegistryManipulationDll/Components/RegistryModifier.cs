namespace RegistryManipulationDll.Components
{
    using Contracts;
    using HirokuScript.RegistryInteraction.Models;

    public class RegistryModifier : IRegistryModifier
    {
        public void Set(object value, RegistryModel registry)
        {
            try
            {
                if (registry.SubKey == null)
                {
                    RegistryFinder finder = new RegistryFinder();
                    registry.SubKey = finder.GetRegistryKeyFor(registry);
                }

                registry.SubKey.SetValue(registry.RegistryName, value, Microsoft.Win32.RegistryValueKind.String);
            }
            catch
            {
                throw;
            }
        }
    }
}
