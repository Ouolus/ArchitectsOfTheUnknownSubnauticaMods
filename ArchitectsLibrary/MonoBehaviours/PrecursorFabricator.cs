using System.Collections;
using UnityEngine;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.API;
using mset;

namespace ArchitectsLibrary.MonoBehaviours
{
    class PrecursorFabricator : Fabricator
    {
	    private const float DefaultPowerUsage = 100f;
	    private Sky baseInteriorSky;
	    private float originalBrightness;
	    private float timeEndFlicker;
	    private const float FlickerIntervalMin = 0.04f;
	    private const float FlickerIntervalMax = 0.1f;

	    public override void Start()
        {
            base.Start();
			spawnAnimationDelay = 4f;
			AchievementServices.CompleteAchievement("BuildPrecursorFabricator");
			var currentBase = gameObject.GetComponentInParent<BaseRoot>();
			if (currentBase != null) baseInteriorSky = currentBase.interiorSky;
        }

        public override void Craft(TechType techType, float duration)
        {
			float powerToConsume = PrecursorFabricatorService.ItemEnergyUsage.GetOrDefault(techType, DefaultPowerUsage);
			if (GameModeUtils.RequiresPower() && powerRelay != null && powerRelay.GetPower() < powerToConsume)
			{
				ErrorMessage.AddMessage($"Crafting of this item requires {powerToConsume} energy.");
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

        private IEnumerator FlickerCoroutine(float duration)
        {
	        if (baseInteriorSky == null)
	        {
		        yield break;
	        }
	        timeEndFlicker = Time.time + duration;
	        originalBrightness = baseInteriorSky.masterIntensity;
	        while (Time.time < timeEndFlicker)
	        {
		        baseInteriorSky.masterIntensity = Random.Range(originalBrightness / 4f, originalBrightness);
		        yield return new WaitForSeconds(Random.Range(FlickerIntervalMin, FlickerIntervalMax));
	        }
	        baseInteriorSky.masterIntensity = originalBrightness;
        }

        private bool IsIonCube(TechType techType)
        {
	        return techType == TechType.PrecursorIonCrystal || techType == AUHandler.ElectricubeTechType ||
	               techType == AUHandler.RedIonCubeTechType;
        }

        public override void OnCraftingBegin(TechType techType, float duration)
        {
            base.OnCraftingBegin(techType, duration);
			_progressDelayScalar = 4f / duration;
			if (PrecursorFabricatorService.FlickerItems.Contains(techType))
			{
				StartCoroutine(FlickerCoroutine(4f));
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