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
            var pathSplitted = stringPath.Split('/');

            this.RegistryName = pathSplitted.Last();

            if (Helper.RegistryHives.Contains(pathSplitted.First()))
                this.SubKeysSeparatedBySlashes = stringPath.Substring(stringPath.IndexOf('/') + 1, stringPath.LastIndexOf('/') - stringPath.IndexOf('/') - 1);
            else
                this.SubKeysSeparatedBySlashes = stringPath.Substring(0, stringPath.LastIndexOf('/'));
        }

        /// <summary>
        /// Represents the path of the desired registry keyValue.
        /// </summary>
        public RegistryKey SubKey
        {
            get
            {
                if (string.IsNullOrEmpty(SubKeysSeparatedBySlashes))
                    return null;

                var finder = new RegistryFinder();
                return finder.GetRegistryKeyFor(this);
            }
        }

        /// <summary>
        /// Represents the path of the desired registry key.
        /// Example: For the "HKEY_CURRENT_USER/Environment" is the path for the key "TEMP"
        /// </summary>
        private string _subKeySeparatedBySlashes;
        public string SubKeysSeparatedBySlashes
        {
            get { return _subKeySeparatedBySlashes; }
            set
            {
                if (!value.Contains("/") && value.Contains("\\"))
                    throw new InvalidRegistryModelException();

                _subKeySeparatedBySlashes = value;
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
