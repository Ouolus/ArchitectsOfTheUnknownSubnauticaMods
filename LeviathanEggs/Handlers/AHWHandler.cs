using System.Linq;
using LeviathanEggs.Interfaces;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace LeviathanEggs.Handlers
{
    public class AHWHandler : IAhwHandler
    {
        public static IAhwHandler Main { get; } = new AHWHandler();

        private AHWHandler()
        {
            // hide constructor
        }
        
        /// <summary>
        /// makes the object given Scannable from the Scanner Room.
        /// </summary>
        /// <param name="gameObject">Game Object to make Scannable</param>
        /// <param name="categoryTechType">Category in the Scanner Room</param>
        void IAhwHandler.MakeObjectScannable(GameObject gameObject, TechType categoryTechType)
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
                Logger.Log(Logger.Level.Error, "TechType to get from MakeObjectScannable() is null");
        }

        void IAhwHandler.MakeItemAcidImmune(TechType techType)
        {
            var acidToList = DamageSystem.acidImmune.ToList();
            
            if (!acidToList.Contains(techType))
                acidToList.Add(techType);

            DamageSystem.acidImmune = acidToList.ToArray();
        }

        /// <summary>
        /// makes the object given Scannable from the Scanner Room.
        /// </summary>
        /// <param name="gameObject">Game Object to make Scannable</param>
        /// <param name="categoryTechType">Category in the Scanner Room</param>
        public static void MakeObjectScannable(GameObject gameObject, TechType categoryTechType = TechType.GenericEgg) =>
            Main.MakeObjectScannable(gameObject, categoryTechType);

        public static void MakeItemAcidImmune(TechType techType) => Main.MakeItemAcidImmune(techType);
    }
}