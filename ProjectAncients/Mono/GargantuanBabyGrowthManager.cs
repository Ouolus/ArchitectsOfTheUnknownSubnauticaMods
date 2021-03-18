using ArchitectsLibrary.MonoBehaviours;
using UnityEngine;

namespace ProjectAncients.Mono
{
    public class GargantuanBabyGrowthManager : MonoBehaviour
    {
        WaterParkCreature _waterParkCreature;

        void Awake()
        {
            _waterParkCreature = gameObject.GetComponent<WaterParkCreature>();
        }
        
        public void OnDrop()
        {
            if (_waterParkCreature != null && !_waterParkCreature.isInside)
            {
                var stagedGrowing = gameObject.EnsureComponent<StagedGrowing>();
                stagedGrowing.daysToNextStage = 25f;
            }
        }
    }
}