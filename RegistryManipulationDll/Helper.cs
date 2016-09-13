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

        public static RegistryKey GoBack(this RegistryKey value)
        {
            return null; 
        }
    }

    public static class Helper
    {
        private static string[] _hives = new string[]
        {
            Registry.CurrentUser.Name,
            Registry.LocalMachine.Name,
            Registry.CurrentConfig.Name
        };
        public static string[] RegistryHives
        {
            get
            {
                return _hives;
            }
        }
    }
}
