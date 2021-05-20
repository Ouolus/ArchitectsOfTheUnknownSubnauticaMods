using System.Collections.Generic;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary
{
    internal static class DictionaryInit
    {
        internal static void PatchAllDictionaries()
        {
            PatchDictionary(WaterParkCreature.creatureEggs, AUHandler.CustomCreatureEggDictionary);
            PatchDictionary(WaterParkCreature.waterParkCreatureParameters, AUHandler.CustomWaterParkCreatureParameters);
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