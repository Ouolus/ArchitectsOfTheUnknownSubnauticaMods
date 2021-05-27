using ArchitectsLibrary.MonoBehaviours;
using UnityEngine;

namespace RotA.Mono
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
            if (_waterParkCreature is not null && !_waterParkCreature.IsInsideWaterPark())
            {
                var stagedGrowing = gameObject.EnsureComponent<StagedGrowing>();
                stagedGrowing.daysToNextStage = 25f;
                stagedGrowing.maxGrowSize = 4f;
                
                Destroy(this, 0.5f);
            }
        }
    }
}