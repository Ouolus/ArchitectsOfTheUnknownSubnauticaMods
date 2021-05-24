using UnityEngine;
using ArchitectsLibrary.Handlers;

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
			bool useMassiveEnergy = techType == TechType.PrecursorIonCrystal || techType == AUHandler.ElectricubeTechType;
			if (useMassiveEnergy)
            {
				powerToConsume = 1000f;
            }
			if (GameModeUtils.RequiresPower() && powerRelay.GetPower() < powerToConsume)
			{
                if (useMassiveEnergy)
                {
					ErrorMessage.AddMessage($"Energy cubes require {powerToConsume} energy to craft.");
				}
                else
                {
					ErrorMessage.AddMessage($"Crafting of this item requires {powerToConsume} energy.");
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
                            if (mat.name.Contains("precursor_crystal_cube"))
                            {
								mat.SetColor("_BorderColor", new Color(1f, 1f, 1f));
							}
							else
                            {
								mat.SetColor("_BorderColor", new Color(0.2f, 1f, 0f));
							}
							mat.SetFloat("_NoiseThickness", 0.2f);
							mat.SetFloat("_NoiseStr", 0.91f);
						}
					}
                }
            }
			base.LateUpdate();
        }
    }
}