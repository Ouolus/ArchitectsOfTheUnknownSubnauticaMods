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
				powerToConsume = 2750f;
            }
			if (powerRelay.GetPower() < powerToConsume)
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
			if (!CrafterLogic.ConsumeResources(techType))
			{
				return;
			}
			powerRelay.ConsumeEnergy(powerToConsume, out _);
			if (CraftData.GetCraftTime(techType, out duration))
			{
				duration = Mathf.Max(this.spawnAnimationDelay + this.spawnAnimationDuration, duration);
			}
			else
			{
				duration = spawnAnimationDelay + spawnAnimationDuration;
			}
			if (_logic != null && _logic.Craft(techType, duration))
			{
				state = true;
				OnCraftingBegin(techType, duration);
			}
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