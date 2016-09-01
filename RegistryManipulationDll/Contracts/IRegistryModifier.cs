namespace RegistryManipulationDll.Contracts
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;

    public interface IRegistryModifier
    {
        void Set(object value, RegistryModel registry);
    }
}
