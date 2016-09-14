namespace HirokuScript.RegistryInteraction.Models
{
    using Microsoft.Win32;
    using RegistryManipulationDll.Components;
    using System.Linq;

    /// <summary>
    /// Represents a Key in the Registry.
    /// </summary>
    public class RegistryModel
    {
        public RegistryModel()
        {

        }

        public RegistryModel(string stringPath)
        {
            var pathSplitted = stringPath.Split('\\');

            this.RegistryName = pathSplitted.Last();
            this.SubKeySeparatedByBackSlashes = stringPath;

            //if (pathSplitted.Count() == 1)
            //    this.SubKeySeparatedByBackSlashes = pathSplitted.First();
            //else
            //{
            //    if (Helper.RegistryHives.Contains(pathSplitted.First()))
            //        this.SubKeySeparatedByBackSlashes = stringPath.Substring(stringPath.IndexOf('\\') + 1, stringPath.LastIndexOf('\\') - stringPath.IndexOf('\\') - 1);
            //    else
            //        this.SubKeySeparatedByBackSlashes = stringPath.Substring(0, stringPath.LastIndexOf('\\'));
            //}
        }

        /// <summary>
        /// Represents the path of the desired registry keyValue.
        /// </summary>
        public RegistryKey SubKey
        {
            get
            {
                if (string.IsNullOrEmpty(SubKeySeparatedByBackSlashes))
                    return null;

                var finder = new RegistryFinder();
                return finder.GetRegistryKeyFor(this);
            }
        }

        /// <summary>
        /// Represents the Last Real Navigational SubKey.
        /// It is posible when constructing Registries with this API, that the SubKey is not real yet.
        /// </summary>
        public RegistryKey LastRealSubKey { get; set; }

        /// <summary>
        /// Represents the path of the desired registry key.
        /// Example: For the "HKEY_CURRENT_USER/Environment" is the path for the key "TEMP"
        /// </summary>
        private string _subKeySeparatedBySlashes;
        public string SubKeySeparatedByBackSlashes
        {
            get { return _subKeySeparatedBySlashes; }
            set
            {
                if (value.Contains("/") && !value.Contains("\\"))
                    throw new InvalidRegistryModelException();

                _subKeySeparatedBySlashes = value;

                //triggers initialization.
                var a = this.SubKey;
            }
        }

        /// <summary>
        /// Represents a RegistryKey, using the example of the SubKeysSeparatedBySlashes documentation, RegistryName would be "TEMP"
        /// </summary>
        public string RegistryName { get; set; }

        public bool IsSubKeyReal
        {
            get
            {
                return SubKey != null;
            }
        }

        public bool IsRegistryReal
        {
            get
            {
                if (string.IsNullOrEmpty(RegistryName))
                    return false;

                var finder = new RegistryFinder();
                return finder.GetValueFrom(this) != null;
            }
        }
    }
}
