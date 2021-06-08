using RotA.Mono.Creatures.GargEssentials;
using UnityEngine;

namespace RotA.Mono.Creatures.CreatureActions
{
    public class GargantuanBabyAggression : MonoBehaviour
    {
        private EcoRegion.TargetFilter isTargetValidFilter;
        private Creature creature;
		private GargantuanRoar roar;
		private LastTarget lastTarget;

        private void Start()
        {
            creature = GetComponent<Creature>();
			lastTarget = GetComponent<LastTarget>();
			roar = GetComponent<GargantuanRoar>();
            isTargetValidFilter = new EcoRegion.TargetFilter(IsTargetValid);
            InvokeRepeating("ScanForAggressionTarget", UnityEngine.Random.Range(0f, 1f), 1f);
        }

        private bool IsTargetValid(IEcoTarget target)
        {
            if(target == null)
            {
                return false;
            }
            GameObject obj = target.GetGameObject();
            if(obj == null)
            {
                return false;
            }
            if (obj.GetComponent<GargantuanRoar>() is not null) //Lazy way of checking if it's another Garg
            {
				return false;
            }
            if (obj == Player.main.gameObject)
            {
                return false;
            }
			if (Vector3.Distance(transform.position, obj.transform.position) > 35f)
			{
				return false;
			}
			if(obj == this.gameObject)
			{
				return false;
			}
            return true;
        }

		private GameObject GetAggressionTarget()
		{
			if(creature.Hunger.Value >= 0.95f) 
			{
				IEcoTarget smallFish = EcoRegionManager.main.FindNearestTarget(EcoTargetType.SmallFish, transform.position, isTargetValidFilter, 2);
				if (smallFish != null)
                {
					return smallFish.GetGameObject();
                }
			}
			IEcoTarget ecoTarget = EcoRegionManager.main.FindNearestTarget(EcoTargetType.Shark, transform.position, isTargetValidFilter, 2);
			if (ecoTarget == null)
			{
				ecoTarget = EcoRegionManager.main.FindNearestTarget(EcoTargetType.Leviathan, transform.position, isTargetValidFilter, 2);
				if (ecoTarget == null)
				{
					return null;
				}
			}
			return ecoTarget.GetGameObject();
		}

		private void ScanForAggressionTarget()
		{
			if (!gameObject.activeInHierarchy || !enabled)
			{
				return;
			}
			if(lastTarget.target != null)
			{
				LiveMixin lm = lastTarget.target.GetComponent<LiveMixin>();
				if(lm == null || !lm.IsAlive())
                {
					lastTarget.SetTarget(null);
                }
                else
                {
					creature.Aggression.Value = 1f;
					return;
				}
			}
			if (EcoRegionManager.main != null)
			{
				GameObject aggressionTarget = GetAggressionTarget();
				if (aggressionTarget != null)
				{
					lastTarget.SetTarget(aggressionTarget);
					creature.Aggression.Value = 1f;
					if (roar != null)
					{
                        roar.PlayOnce(out _, GargantuanRoar.RoarMode.CloseOnly);
					}
				}
			}
		}
	}
}
