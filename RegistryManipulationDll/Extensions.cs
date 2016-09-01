namespace RegistryManipulationDll.Components
{
    using Microsoft.Win32;
    using System.Linq;

    public static class Extensions
    {
        public static bool ContainsKey(this RegistryKey value, string key)
        {
            return value.GetValueNames().Contains(key);
        }
    }
}
