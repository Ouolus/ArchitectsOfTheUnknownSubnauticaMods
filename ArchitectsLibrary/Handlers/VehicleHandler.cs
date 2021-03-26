using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Handlers
{
    public class VehicleHandler : IVehicleHandler
    {
        internal static IDictionary<TechType, float> CustomEnergyCosts = new HashDictionary<TechType, float>();
        internal static IDictionary<TechType, float> CustomMaxCharges = new HashDictionary<TechType, float>();
        
        
        public static IVehicleHandler Main { get; } = new VehicleHandler();

        private VehicleHandler()
        {
            // hide constructor
        }

        #region Interface Implementation
        
        void IVehicleHandler.EnergyCost(TechType techType, float energyCost) => CustomEnergyCosts[techType] = energyCost;

        void IVehicleHandler.MaxCharge(TechType techType, float maxCharge) => CustomMaxCharges[techType] = maxCharge;

        #endregion

        #region Statics

        public static void EnergyCost(TechType techType, float energyCost) => Main.EnergyCost(techType, energyCost);

        public static void MaxCharge(TechType techType, float maxCharge) => Main.MaxCharge(techType, maxCharge);

        #endregion
    }
}