namespace HirokuScript.RegistryInteraction.Components
{
    using HirokuScript.RegistryInteraction.Contracts;
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;
    using System;
    using System.Linq;

    public class RegistryFinder : IRegistryFinder
    {
        private RegistryKey[] _startPoints;

        public RegistryFinder()
        {
            _startPoints = new RegistryKey[]
            {
                Registry.CurrentUser,
                Registry.LocalMachine
            };
        }

        /// <summary>
        /// Retrieves the value of a RegistryModel within the specified RegistryKey.
        /// Ex: RegistryKey is used as a startpoint, for example Registry.CurrentUser
        /// Side Note: In case your RegistryModel does not contain the SubKeys, the finder will recursively search through the registry.
        /// </summary>
        /// <param name="startPoint">RegistryKey or Folder to start the search, Example: Registry.LocalMachine</param>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <returns>The value of the registry, if there is no registry, it retrieves null.</returns>
        public object GetValueFrom(RegistryModel registry, RegistryKey startPoint = null)
        {
            if (string.IsNullOrEmpty(registry.RegistryName))
                throw new Exception();

            if (startPoint == null)
                foreach (var searchPoint in _startPoints)
                {
                    object value = string.IsNullOrEmpty(registry.SubKeysSeparatedBySlashes) ? GetValueWithRecursiveSearch(searchPoint, registry) : GetStraightValue(searchPoint, registry);

                    if (value != null)
                        return value;
                }

            return string.IsNullOrEmpty(registry.SubKeysSeparatedBySlashes) ? GetValueWithRecursiveSearch(startPoint, registry) : GetStraightValue(startPoint, registry);
        }

        public RegistryKey GetRegistryKeyFor(RegistryModel registry, RegistryKey startSearchPoint)
        {
            return null;
        }

        private RegistryKey GetStraightRegistryKey(RegistryKey startPoint, RegistryModel registry)
        {
            try
            {
                string[] subKeys = registry.SubKeysSeparatedBySlashes.Split('/');

                foreach (string subKey in subKeys)
                    startPoint = startPoint.OpenSubKey(subKey);

                return startPoint;
            }
            catch
            {
                return null;
            }
        }

        private object GetValueFromRegistryKey(RegistryKey key, RegistryModel registry)
        {
            return key.GetValue(registry.RegistryName);
        }

        private object GetStraightValue(RegistryKey startPoint, RegistryModel registry)
        {
            try
            {
                string[] subKeys = registry.SubKeysSeparatedBySlashes.Split('/');

                foreach (string subKey in subKeys)
                    startPoint = startPoint.OpenSubKey(subKey);

                return startPoint.GetValue(registry.RegistryName);
            }
            catch
            {
                return null;
            }
        }

        private object GetValueWithRecursiveSearch(RegistryKey startPoint, RegistryModel registry)
        {
            object value = null;

            try
            {
                string[] valueKeys = startPoint.GetValueNames();

                if (valueKeys.Contains(registry.RegistryName))
                    return startPoint.GetValue(registry.RegistryName);
                else
                {
                    foreach (string subKey in startPoint.GetSubKeyNames())
                    {
                        value = GetValueWithRecursiveSearch(startPoint.OpenSubKey(subKey), registry);

                        if (value != null)
                            break;
                    }
                }
            }
            catch
            {
                return null;
            }

            return value;
        }
    }
}
