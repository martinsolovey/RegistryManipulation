namespace RegistryManipulationDll.Components
{
    using HirokuScript.RegistryInteraction.Contracts;
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;
    using System;

    public class RegistryFinder : IRegistryFinder
    {
        private bool _writable;
        private RegistryKey[] _startPoints;

        public RegistryFinder(bool forReadOnly = false)
        {
            _writable = !forReadOnly;
            _startPoints = new RegistryKey[]
            {
                Registry.CurrentUser,
                Registry.LocalMachine
            };
        }

        /// <summary>
        /// Retrieves the value of a RegistryModel within the specified RegistryHive.
        /// </summary>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <param name="startPoint">Starting point to look for the specified Registry</param>
        /// <returns>The value of the seeked registry</returns>
        public object GetValueFrom(RegistryModel registry, RegistryHive startPoint)
        {
            RegistryKey startKey = RegistryKey.OpenBaseKey(startPoint, RegistryView.Default);

            return GetValueFrom(registry, startKey);
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

            //Checking that SubKey does not include the RegistryHive.
            //var firstSplit = registry.SubKeySeparatedByBackSlashes.Substring(0, registry.SubKeySeparatedByBackSlashes.IndexOf('\\'));
            //if (_startPoints.Any(x => x.ToString() == firstSplit))
            //    registry.SubKeySeparatedByBackSlashes = registry.SubKeySeparatedByBackSlashes.Remove(0, registry.SubKeySeparatedByBackSlashes.IndexOf('\\') + 1);

            if (startPoint == null)
                foreach (var searchPoint in _startPoints)
                {
                    object value = GetValueFrom(registry, searchPoint);

                    if (value != null)
                        return value;
                }

            RegistryKey pathToRegistry = string.IsNullOrEmpty(registry.SubKeySeparatedByBackSlashes)
                ? GetRegistryKeyWithRecursiveSearch(startPoint, registry)
                : GetStraightRegistryKey(startPoint, registry);

            return GetValueFromRegistryKey(pathToRegistry, registry);
        }

        /// <summary>
        /// Retrieves the RegistryKey through the one to get or set a value of the specified registry.
        /// Ex: RegistryKey is used as a startpoint, for example Registry.CurrentUser
        /// </summary>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <param name="startPoint">RegistryKey or Folder to start the search, Example: Registry.LocalMachine</param>
        /// <returns>The RegistryKey where the seeked registry lies, if there is no registry, it retrieves null.</returns>
        public RegistryKey GetRegistryKeyFor(RegistryModel registry, RegistryHive startPoint)
        {
            RegistryKey startKey = RegistryKey.OpenBaseKey(startPoint, RegistryView.Default);

            return GetRegistryKeyFor(registry, startKey);
        }

        /// <summary>
        /// Retrieves the RegistryKey through the one to get or set a value of the specified registry.
        /// Ex: RegistryKey is used as a startpoint, for example Registry.CurrentUser
        /// Side Note: In case your RegistryModel does not contain the SubKeys, the finder will recursively search through the registry.
        /// </summary>
        /// <param name="startPoint">RegistryKey or Folder to start the search, Example: Registry.LocalMachine</param>
        /// <param name="registry">The Registry you want to get the value for.</param>
        /// <returns>The value of the RegistryKey, if there is no registry, it retrieves null.</returns>
        public RegistryKey GetRegistryKeyFor(RegistryModel registry, RegistryKey startPoint = null)
        {
            if (string.IsNullOrEmpty(registry.RegistryName))
                return null;

            if (startPoint == null)
                foreach (var searchPoint in _startPoints)
                {
                    RegistryKey value = GetRegistryKeyFor(registry, searchPoint);

                    if (value != null || registry.SubKeySeparatedByBackSlashes.Contains(searchPoint.Name))
                        return value;
                }

            RegistryKey pathToRegistry = string.IsNullOrEmpty(registry.SubKeySeparatedByBackSlashes)
                ? GetRegistryKeyWithRecursiveSearch(startPoint, registry)
                : GetStraightRegistryKey(startPoint, registry);

            return pathToRegistry;
        }

        private RegistryKey GetStraightRegistryKey(RegistryKey startPoint, RegistryModel registry)
        {
            try
            {
                string[] subKeys = registry.SubKeySeparatedByBackSlashes.Split('\\');

                bool navigatedAllSubKeys = true;
                foreach (string subKey in subKeys)
                {
                    if (startPoint.Name == subKey)
                        continue;

                    var tempSubKey = startPoint.OpenSubKey(subKey, _writable);
                    bool isKeyNotFolder = tempSubKey == null && startPoint.ContainsKey(subKey);

                    if (tempSubKey == null)
                    {
                        registry.LastRealSubKey = startPoint;
                        navigatedAllSubKeys = false;
                    }

                    if (!isKeyNotFolder)
                        startPoint = tempSubKey;
                }

                if (navigatedAllSubKeys)
                    registry.LastRealSubKey = startPoint;

                return startPoint;
            }
            catch
            {
                return null;
            }
        }

        private RegistryKey GetRegistryKeyWithRecursiveSearch(RegistryKey startPoint, RegistryModel registry)
        {
            RegistryKey value = null;

            try
            {
                if (startPoint.ContainsKey(registry.RegistryName))
                    return startPoint;
                else
                {
                    foreach (string subKey in startPoint.GetSubKeyNames())
                    {
                        value = GetRegistryKeyWithRecursiveSearch(startPoint.OpenSubKey(subKey, _writable), registry);

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

        private object GetValueFromRegistryKey(RegistryKey key, RegistryModel registry)
        {
            if (key == null)
                return null;

            return key.GetValue(registry.RegistryName);
        }
    }
}
