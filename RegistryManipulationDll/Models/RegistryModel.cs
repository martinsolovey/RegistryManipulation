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

        /// <summary>
        /// Constructor for a RegistryModel
        /// </summary>
        /// <param name="keyString">It represents the entire string for the desired Registry (existing or not)
        /// Example: "HKEY_CURRENT_USER\Control Panel\Accessibility\MessageDuration"</param>
        public RegistryModel(string keyString)
        {
            var pathSplitted = keyString.Split('\\');

            this.RegistryName = pathSplitted.Last();
            this.SubKeySeparatedByBackSlashes = keyString;
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
