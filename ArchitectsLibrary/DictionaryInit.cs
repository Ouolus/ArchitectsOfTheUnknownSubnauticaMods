using System.Collections.Generic;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary
{
    class DictionaryInit
    {
        internal static void PatchAllDictionaries()
        {
            PatchDictionary(WaterParkCreature.creatureEggs, AUHandler.customCreatureEggDictionary);
            PatchDictionary(WaterParkCreature.waterParkCreatureParameters, AUHandler.customWaterParkCreatureParameters);
            PatchDictionary(CraftData.maxCharges, VehicleHandler.CustomMaxCharges);
            PatchDictionary(CraftData.energyCost, VehicleHandler.CustomEnergyCosts);
        }
        
        static void PatchDictionary<V>(IDictionary<TechType, V> origDictionary, IDictionary<TechType, V> cusDictionary)
        {
            foreach (KeyValuePair<TechType,V> kvp in cusDictionary)
            {
                origDictionary[kvp.Key] = kvp.Value;
            }
        }
    }
}