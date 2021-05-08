using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Handlers
{
    /// <summary>
    /// a Handler for common Modifications to <see cref="Vehicle"/>s.
    /// </summary>
    public static class VehicleHandler
    {
        internal static IDictionary<TechType, float> CustomEnergyCosts = new HashDictionary<TechType, float>();
        internal static IDictionary<TechType, float> CustomMaxCharges = new HashDictionary<TechType, float>();
        
        /// <summary>
        /// Amount of Energy this <see cref="TechType"/> Module will cost on use
        /// </summary>
        /// <param name="techType">the TechType Module</param>
        /// <param name="energyCost">the amount of energy it costs.</param>
        public static void EnergyCost(TechType techType, float energyCost) => CustomEnergyCosts[techType] = energyCost;

        
        /// <summary>
        /// the total max charge that the passed <see cref="TechType"/> Module cant bypass when its charging up its shot
        ///  for Modules that are <see cref="QuickSlotType.Chargeable"/> or <see cref="QuickSlotType.SelectableChargeable"/>.
        /// </summary>
        /// <param name="techType"></param>
        /// <param name="maxCharge"></param>
        public static void MaxCharge(TechType techType, float maxCharge) => CustomMaxCharges[techType] = maxCharge;
        
    }
}