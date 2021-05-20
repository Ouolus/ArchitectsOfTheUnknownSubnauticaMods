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

        Collider[] colliderBuffer;

        readonly List<EcoTargetType> aggressiveTargetTypes = new List<EcoTargetType>()
        {
            EcoTargetType.Biter,
            EcoTargetType.Shark,
            EcoTargetType.Leviathan,
            EcoTargetType.LavaLarva,
            EcoTargetType.Whale //not aggressive but whatever
        };

        readonly List<EcoTargetType> dontDeterTargetTypes = new List<EcoTargetType>()
        {
            EcoTargetType.SubDecoy,
            EcoTargetType.CuteFish,
            EcoTargetType.Cure,
            EcoTargetType.CuredWarp
        };

        void Start()
        {
            constructable = gameObject.GetComponent<Constructable>();
        }

        void Update()
        {
            if(constructable == null)
            {
                return;
            }
            if (!constructable.constructed)
            {
                return;
            }
            if(Time.time > timeDeterAgain)
            {
                timeDeterAgain = Time.time + deterDelay;
                Deter();
            }
        }

        public void Deter()
        {
            colliderBuffer = Physics.OverlapSphere(transform.position, maxDeterRadius);
            foreach(var collider in colliderBuffer)
            {
                Creature creature = collider.GetComponent<Creature>();
                if (creature)
                {
                    TryDeterCreature(creature);
                }
            }
        }

        void TryDeterCreature(Creature creature)
        {
            EcoTargetType targetType = EcoTargetType.SmallFish;
            var ecoTarget = creature.GetComponent<EcoTarget>();
            if (ecoTarget)
            {
                targetType = ecoTarget.type;
            }
            if(TryGetDeterDistance(targetType, out float deterDistance))
            {
                if(Vector3.Distance(creature.transform.position, this.transform.position) > deterDistance)
                {
                    return;
                }
                Vector3 directionToSwim = (creature.transform.position - this.transform.position).normalized;
                Vector3 targetPosition = transform.position + (directionToSwim * deterDistance);
                creature.Scared.Add(1f);
                var swimBehaviour = creature.GetComponent<SwimBehaviour>();
                if (swimBehaviour)
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
