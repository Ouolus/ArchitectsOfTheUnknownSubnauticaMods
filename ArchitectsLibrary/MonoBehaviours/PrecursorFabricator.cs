using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class PrecursorFabricator : Fabricator
    {
        public override void Craft(TechType techType, float duration)
        {
			float powerToConsume = 50f;
			if(techType == TechType.PrecursorIonCrystal)
            {
				powerToConsume = 1000f;
            }
			if (!CrafterLogic.ConsumeEnergy(powerRelay, powerToConsume))
			{
				ErrorMessage.AddMessage("Not enough power.");
				return;
			}
			if (!CrafterLogic.ConsumeResources(techType))
			{
				return;
			}
			if (CraftData.GetCraftTime(techType, out duration))
			{
				duration = Mathf.Max(spawnAnimationDelay + spawnAnimationDuration, duration);
			}
			else
			{
				duration = spawnAnimationDelay + spawnAnimationDuration;
			}
			base.Craft(techType, duration);
		}
    }
}