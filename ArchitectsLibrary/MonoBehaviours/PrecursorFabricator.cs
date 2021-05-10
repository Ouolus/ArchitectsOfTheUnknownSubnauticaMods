using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class PrecursorFabricator : Fabricator
    {
        public override void Craft(TechType techType, float duration)
        {
			float powerToConsume = 50f;
			bool useMassiveEnergy = techType == TechType.PrecursorIonCrystal;
			if (useMassiveEnergy)
            {
				powerToConsume = 1000f;
            }
			if (!CrafterLogic.ConsumeEnergy(powerRelay, powerToConsume - 5f)) //it will consume 5 more power later
			{
                if (useMassiveEnergy)
                {
					ErrorMessage.AddMessage(string.Format("Crafting of this item requires {0} energy.", powerToConsume));
				}
                else
                {
					ErrorMessage.AddMessage("Not enough energy.");
                }
				return;
			}
			base.Craft(techType, duration);
		}

        public override void LateUpdate()
		{
			if(ghost != null)
            {
				var materials = ghost.ghostMaterials;
				if(materials != null)
                {
					foreach (Material mat in materials)
                    {
						mat.SetColor("_BorderColor", new Color(0.2f, 1f, 0f));
						mat.SetFloat("_NoiseThickness", 0.41f);
						mat.SetFloat("_NoiseStr", 0.91f);
                    }
                }
            }
			base.LateUpdate();
        }
    }
}