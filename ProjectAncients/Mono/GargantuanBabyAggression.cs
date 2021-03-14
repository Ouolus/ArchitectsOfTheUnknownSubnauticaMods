using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectAncients.Mono
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
            if (obj == Player.main.gameObject)
            {
                return false;
            }
			if (Vector3.Distance(transform.position, obj.transform.position) > 35f)
			{
				return false;
			}
            return true;
        }

		private GameObject GetAggressionTarget()
		{
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
			if (EcoRegionManager.main != null)
			{
				GameObject aggressionTarget = GetAggressionTarget();
				if (aggressionTarget != null)
				{
					lastTarget.SetTarget(aggressionTarget);
					if(roar != null)
					{
						roar.PlayOnce();
					}
				}
			}
		}
	}
}
