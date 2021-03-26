using System.Linq;
using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Utility;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace ArchitectsLibrary.Handlers
{
    public class AUHandler : IAuHandler
    {
        internal static IDictionary<TechType, TechType> customCreatureEggDictionary = 
            new HashDictionary<TechType, TechType>();

        internal static IDictionary<TechType, WaterParkCreatureParameters> customWaterParkCreatureParameters =
            new HashDictionary<TechType, WaterParkCreatureParameters>();
        
        public static IAuHandler Main { get; } = new AUHandler();

        private AUHandler()
        {
            // hide constructor
        }
        
        /// <summary>
        /// makes the object given Scannable from the Scanner Room.
        /// </summary>
        /// <param name="gameObject">Game Object to make Scannable</param>
        /// <param name="categoryTechType">Category in the Scanner Room</param>
        void IAuHandler.SetObjectScannable(GameObject gameObject, TechType categoryTechType)
        {
            var tt = CraftData.GetTechType(gameObject);

            if (tt != TechType.None)
            {
                ResourceTracker resourceTracker = gameObject.EnsureComponent<ResourceTracker>();
                resourceTracker.techType = tt;
                resourceTracker.overrideTechType = categoryTechType;
                resourceTracker.rb = gameObject.GetComponent<Rigidbody>();
                resourceTracker.prefabIdentifier = gameObject.GetComponent<PrefabIdentifier>();
                resourceTracker.pickupable = gameObject.GetComponent<Pickupable>();
            }
            else
                Logger.Log(Logger.Level.Warn, "TechType to get from SetObjectScannable() is null");
        }

        void IAuHandler.MakeItemAcidImmune(TechType techType)
        {
            var acidToList = DamageSystem.acidImmune.ToList();
            
            if (!acidToList.Contains(techType))
                acidToList.Add(techType);

            DamageSystem.acidImmune = acidToList.ToArray();
        }

        void IAuHandler.SetCreatureEgg(TechType creatureType, TechType eggType)
        {
            customCreatureEggDictionary[creatureType] = eggType;
        }

        void IAuHandler.SetCreatureParameters(TechType creatureType,
            WaterParkCreatureParameters waterParkCreatureParameters)
        {
            customWaterParkCreatureParameters[creatureType] = waterParkCreatureParameters;
        }

        /// <summary>
        /// makes the object given Scannable from the Scanner Room.
        /// </summary>
        /// <param name="gameObject">Game Object to make Scannable</param>
        /// <param name="categoryTechType">Category in the Scanner Room</param>
        public static void SetObjectScannable(GameObject gameObject, TechType categoryTechType = TechType.GenericEgg) =>
            Main.SetObjectScannable(gameObject, categoryTechType);

        public static void MakeItemAcidImmune(TechType techType) => Main.MakeItemAcidImmune(techType);

        public static void SetCreatureEgg(TechType creatureType, TechType eggType) =>
            Main.SetCreatureEgg(creatureType, eggType);

        public static void SetCreatureParameters(TechType creatureType,
            WaterParkCreatureParameters waterParkCreatureParameters) =>
            Main.SetCreatureParameters(creatureType, waterParkCreatureParameters);
    }
}