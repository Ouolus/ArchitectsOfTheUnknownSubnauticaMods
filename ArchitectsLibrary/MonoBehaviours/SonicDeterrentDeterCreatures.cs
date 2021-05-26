using UnityEngine;
using System.Collections.Generic;

namespace ArchitectsLibrary.MonoBehaviours
{
    /// <summary>
    /// Deters away creatures. Customizable. May have an impact on performance.
    /// </summary>
    public class SonicDeterrentDeterCreatures : MonoBehaviour
    {
        float timeDeterAgain;
        float deterDelay = 1f;
        /// <summary>
        /// Distance for most fish to swim away.
        /// </summary>
        public float smallFishDeterRadius = 45f;
        /// <summary>
        /// Distance for whales (gasopod, thalassacean, etc), sharks (stalker, lava lizard, etc), biters, leviathans, and lava larva to swim away.
        /// </summary>
        public float aggressiveFishDeterRadius = 75f;
        /// <summary>
        /// If a fish is further than this distance it won't get "evaluated" for swimming away.
        /// </summary>
        public float maxDeterRadius = 75f;
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
            if (constructable == null)
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
                
                if (creature != null)
                    DeterCreature(creature);
            }
        }

        void DeterCreature(Creature creature)
        {
            var targetType = EcoTargetType.SmallFish;
            var ecoTarget = creature.GetComponent<EcoTarget>();
            if (ecoTarget != null)
                targetType = ecoTarget.type;
            
            if (TryGetDeterDistance(targetType, out var deterDistance))
            {
                if (Vector3.Distance(creature.transform.position, transform.position) > deterDistance)
                {
                    return;
                }

                var position = transform.position; // apparently this looks way more efficient according to Rider.
                var directionToSwim = (creature.transform.position - position).normalized;
                var targetPosition = position + (directionToSwim * deterDistance);
                var targetLeashPosition = position + (directionToSwim * (deterDistance + 10f));
                creature.Scared.Add(1f);
                creature.leashPosition = targetLeashPosition;
                var swimBehaviour = creature.GetComponent<SwimBehaviour>();
                if (swimBehaviour != null)
                {
                    float swimVelocity = 6f;
                    var fleeOnDamage = creature.GetComponent<FleeOnDamage>();
                    if (fleeOnDamage != null)
                    {
                        swimVelocity = fleeOnDamage.swimVelocity;
                    }
                    swimBehaviour.SwimTo(targetPosition, swimVelocity);
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
