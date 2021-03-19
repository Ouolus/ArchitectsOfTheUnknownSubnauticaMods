using ArchitectsLibrary.MonoBehaviours;
using UnityEngine;

namespace ProjectAncients.Mono
{
    public class GargantuanBabyGrowthManager : MonoBehaviour
    {
        WaterParkCreature _waterParkCreature;

        bool _initialized = false;
        void Awake()
        {
            _waterParkCreature = gameObject.GetComponent<WaterParkCreature>();
        }

        void Update()
        {
            if (_initialized)
                Destroy(this);
            
            if (_waterParkCreature != null && !_waterParkCreature.IsInsideWaterPark())
            {
                var stagedGrowing = gameObject.EnsureComponent<StagedGrowing>();
                stagedGrowing.daysToNextStage = 25f;
                stagedGrowing.maxGrowSize = 4f;
                _initialized = true;
            }
        }
    }
}