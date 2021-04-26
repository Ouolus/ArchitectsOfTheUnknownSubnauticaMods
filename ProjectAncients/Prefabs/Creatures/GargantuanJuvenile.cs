using ECCLibrary;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
    public class GargantuanJuvenile : GargantuanBase
    {
        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(80f, 30f, 80f), 10f, 2.5f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 100f);

        public override bool OneShotsPlayer => false;

        public override bool CanBeScaredByElectricity => true;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 10f, "Lifeforms/Fauna/Leviathans", Mod.assetBundle.LoadAsset<Sprite>("Juvenile_Popup"), Mod.assetBundle.LoadAsset<Texture2D>("Juvenile_Ency"));

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 20f, 15f, 20f, 17f, 30f);

        public override float EyeFov => 0.8f;

        public override string GetEncyTitle => "Gargantuan Leviathan Juvenile";

        public GargantuanJuvenile(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override float VehicleDamagePerSecond => 30f;
    }
}
