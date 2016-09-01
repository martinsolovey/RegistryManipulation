using Microsoft.Win32;

namespace HirokuScript.RegistryInteraction.Models
{
    /// <summary>
    /// Represents a Key in the Registry.
    /// </summary>
    public class RegistryModel
    {
        /// <summary>
        /// Represents the path of the desired registry keyValue.
        /// </summary>
        public RegistryKey SubKey { get; set; }

        /// <summary>
        /// Represents the path of the desired registry key.
        /// Example: For the "HKEY_CURRENT_USER/Environment" is the path for the key "TEMP"
        /// </summary>
        public string SubKeysSeparatedBySlashes { get; set; }

        /// <summary>
        /// Represents a RegistryKey, using the example of the SubKeysSeparatedBySlashes documentation, RegistryName would be "TEMP"
        /// </summary>
        public string RegistryName { get; set; }

        public string FullKeyName
        {
            get
            {
                if (string.IsNullOrEmpty(RegistryName) || string.IsNullOrEmpty(SubKeysSeparatedBySlashes))
                    return string.Empty;

                return string.Format("{0}/{1}", SubKeysSeparatedBySlashes, RegistryName);
            }
        }
    }
}
