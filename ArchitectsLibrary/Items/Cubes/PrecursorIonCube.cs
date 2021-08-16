using ArchitectsLibrary.API;
using ArchitectsLibrary.MonoBehaviours;
using UnityEngine;

namespace ArchitectsLibrary.Items.Cubes
{
    internal abstract class PrecursorIonCube : ReskinSpawnable
    {
        public PrecursorIonCube(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description) {}
        
        protected sealed override string ReferenceClassId => "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a";

        public sealed override float CraftingTime => 30f;
        
        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;

        protected abstract int Capacity { get; }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = Capacity;
            Main.IonCubeCraftModelFix(prefab);
            prefab.GetComponent<InspectOnFirstPickup>().animParam = "holding_precursorioncrystal";
        }
    }
}