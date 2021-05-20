using UnityEngine;
using System.Collections.Generic;

namespace ArchitectsLibrary.MonoBehaviours
{
    class SonicDeterrentDeterCreatures : MonoBehaviour
    {
        float timeDeterAgain;
        float deterDelay = 1f;
        float smallFishDeterRadius = 15f;
        float aggressiveFishDeterRadius = 60f;
        float maxDeterRadius = 60f;
        Constructable constructable;
        
        readonly List<EcoTargetType> aggressiveTargetTypes = new()
        {
            EcoTargetType.Biter,
            EcoTargetType.Shark,
            EcoTargetType.Leviathan,
            EcoTargetType.LavaLarva,
            EcoTargetType.Whale // not aggressive but whatever
        };

        readonly List<EcoTargetType> dontDeterTargetTypes = new()
        {
            EcoTargetType.SubDecoy,
            EcoTargetType.CuteFish,
            EcoTargetType.Cure,
            EcoTargetType.CuredWarp
        };

        void Start() => constructable = gameObject.GetComponent<Constructable>();

        void Update()
        {
            if (constructable is null)
            {
                return;
            }
            
            if (!constructable.constructed)
            {
                return;
            }
            
            if (Time.time > timeDeterAgain)
            {
                timeDeterAgain = Time.time + deterDelay;
                Deter();
            }
        }

        void Deter()
        {
            // non-allocated OverlapSphere generates less garbage than Physics.OverlapSphere
            var hitColliders = UWE.Utils.OverlapSphereIntoSharedBuffer(transform.position, maxDeterRadius);
            for (var i = 0; i < hitColliders; i++)
            {
                var collider = UWE.Utils.sharedColliderBuffer[i];
                var obj = UWE.Utils.GetEntityRoot(collider.gameObject);
                obj ??= collider.gameObject;
                
                var creature = obj.GetComponent<Creature>();
                
                if (creature is not null)
                    DeterCreature(creature);
            }
        }

        void DeterCreature(Creature creature)
        {
            var targetType = EcoTargetType.SmallFish;
            var ecoTarget = creature.GetComponent<EcoTarget>();
            if (ecoTarget is not null)
                targetType = ecoTarget.type;
            
            if (TryGetDeterDistance(targetType, out var deterDistance))
            {
                if (Vector3.Distance(creature.transform.position, transform.position) > deterDistance)
                {
                    return;
                }

                var position = transform.position; // apparently this looks way more efficient according to Rider.
                var directionToSwim = (creature.transform.position - position).normalized;
                var targetPosition = position + directionToSwim * deterDistance;
                creature.Scared.Add(1f);
                var swimBehaviour = creature.GetComponent<SwimBehaviour>();
                if (swimBehaviour != null)
                {
                    swimBehaviour.SwimTo(targetPosition, 10f);
                }
            }

        }

        bool TryGetDeterDistance(EcoTargetType creature, out float deterDistance)
        {
            if (dontDeterTargetTypes.Contains(creature))
            {
                deterDistance = 0f;
                return false;
            }
            if (aggressiveTargetTypes.Contains(creature))
            {
                deterDistance = aggressiveFishDeterRadius;
                return true;
            }
            
            deterDistance = smallFishDeterRadius;
            return true;
        }
    }
}
