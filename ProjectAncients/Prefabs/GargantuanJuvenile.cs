using UnityEngine;

namespace ProjectAncients.Prefabs
{
    public class GargantuanJuvenile : GargantuanBase
    {
        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(80f, 30f, 80f), 10f, 2.5f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 100f);

        public GargantuanJuvenile(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }
    }
}
