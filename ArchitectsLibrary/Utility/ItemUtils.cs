using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Handlers;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// A Utility class for several generic methods related to editing data related to TechTypes.
    /// </summary>
    public static class ItemUtils
    {
        /// <summary>
        /// Make an object scannable and have a PDA databank entry. The language line for <paramref name="encyKey"/> must be set elsewhere.
        /// </summary>
        /// <param name="techType">The TechType that will be made scannable. ANY object with a TechTag matching this <see cref="TechType"/> will be scannable.</param>
        /// <param name="encyKey">The UNIQUE language line key for this encyclopedia entry.</param>
        /// <param name="scanTime">How long it takes to scan the object.</param>
        public static void MakeObjectScannable(TechType techType, string encyKey, float scanTime)
        {
            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = techType,
                encyclopedia = encyKey,
                scanTime = scanTime,
                isFragment = false
            });
        }
    }
}
