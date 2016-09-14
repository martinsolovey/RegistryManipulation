namespace RegistryManipulationDll.Components
{
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;
    using System.Linq;

    public static class Extensions
    {
        public static void DeleteEntireTree(this RegistryKey value)
        {
            value.DeleteSubKeyTree("");
        }

        public static RegistryKey GoBack(this RegistryKey value)
        {
            string lastKeyString = value.Name.Substring(0, value.Name.LastIndexOf('\\'));
            return new RegistryFinder().GetRegistryKeyFor(new RegistryModel(lastKeyString));
        }

        public static bool ContainsKey(this RegistryKey value, string key)
        {
            return value.GetValueNames().Contains(key);
        }

        public static bool IsSameAs(this RegistryKey value, RegistryKey anotherKey)
        {
            return value.Name == anotherKey.Name;
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
