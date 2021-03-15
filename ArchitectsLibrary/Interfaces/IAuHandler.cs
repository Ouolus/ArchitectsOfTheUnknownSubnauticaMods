using UnityEngine;

namespace ArchitectsLibrary.Interfaces
{
    public interface IAuHandler
    {
        /// <summary>
        /// makes the object given Scannable from the Scanner Room.
        /// </summary>
        /// <param name="gameObject">Game Object to make Scannable</param>
        /// <param name="categoryTechType">Category in the Scanner Room</param>
        void SetObjectScannable(GameObject gameObject, TechType categoryTechType = TechType.GenericEgg);
        
        
        void MakeItemAcidImmune(TechType techType);
    }
}