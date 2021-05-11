using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class PrecursorFabricator : Fabricator
    {
        public override void Start()
        {
            base.Start();
			spawnAnimationDelay = 4f;
        }

        public override void Craft(TechType techType, float duration)
        {
			float powerToConsume = 100f;
			bool useMassiveEnergy = techType == TechType.PrecursorIonCrystal;
			if (useMassiveEnergy)
            {
				powerToConsume = 1000f;
            }
			if (GameModeUtils.RequiresPower() && powerRelay.GetPower() < powerToConsume)
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
				duration = Mathf.Max(spawnAnimationDelay + spawnAnimationDuration, duration);
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

        public override void OnCraftingBegin(TechType techType, float duration)
        {
            base.OnCraftingBegin(techType, duration);
			_progressDelayScalar = 4f / duration;
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
						if(mat != null)
                        {
							mat.SetColor("_BorderColor", new Color(0.2f, 1f, 0f));
							mat.SetFloat("_NoiseThickness", 0.41f);
							mat.SetFloat("_NoiseStr", 0.91f);
						}
					}
                }
            }
			base.LateUpdate();
        }
    }
}