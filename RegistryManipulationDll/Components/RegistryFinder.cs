﻿namespace RegistryManipulationDll.Components
{
    using HirokuScript.RegistryInteraction.Contracts;
    using HirokuScript.RegistryInteraction.Models;
    using Microsoft.Win32;
    using System;

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
                    object value = GetValueFrom(registry, searchPoint);

                    if (value != null)
                        return value;
                }

            RegistryKey pathToRegistry = string.IsNullOrEmpty(registry.SubKeysSeparatedBySlashes)
                ? GetRegistryKeyWithRecursiveSearch(startPoint, registry)
                : GetStraightRegistryKey(startPoint, registry);

            return GetValueFromRegistryKey(pathToRegistry, registry);
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
                throw new Exception();

            if (startPoint == null)
                foreach (var searchPoint in _startPoints)
                {
                    RegistryKey value = GetRegistryKeyFor(registry, searchPoint);

                    if (value != null)
                        return value;
                }

            RegistryKey pathToRegistry = string.IsNullOrEmpty(registry.SubKeysSeparatedBySlashes)
                ? GetRegistryKeyWithRecursiveSearch(startPoint, registry)
                : GetStraightRegistryKey(startPoint, registry);

            return pathToRegistry;
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
                        value = GetRegistryKeyWithRecursiveSearch(startPoint.OpenSubKey(subKey), registry);

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
            return key.GetValue(registry.RegistryName);
        }
    }
}
