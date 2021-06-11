using RotA.Mono.Singletons;
using UnityEngine;

namespace RotA.Prefabs.Creatures
{
    public class GargantuanVoid : AdultGargantuan
    {
        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Global;

        public GargantuanVoid(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            prefab.AddComponent<VoidGargSingleton>();
        }
    }
}
