using ProjectAncients.Mono;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
    public class GargantuanVoid : GargantuanJuvenile //eventually change it to inherit from GargantuanAdult
    {
        public GargantuanVoid(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            prefab.AddComponent<AdultGargSingleton>();
        }
    }
}
