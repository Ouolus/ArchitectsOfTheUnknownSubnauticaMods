using UnityEngine;

namespace ProjectAncients.Mono
{
    public class RunAwayWhenScared : CreatureAction
    {
        public float priority = 1f;
        Vector3 scaredPosition;

        public override float Evaluate(Creature creature)
        {
            if(creature.Scared.Value > 0.9f)
            {
                return priority;
            }
            else
            {
                return 0f;
            }
        }

        public override void StartPerform(Creature creature)
        {
            scaredPosition = transform.position;
            swimBehaviour.SwimTo(scaredPosition + (Random.onUnitSphere * 75f), 20f);
        }
        public override void Perform(Creature creature, float deltaTime)
        {
            creature.Scared.Value -= 0.18f * deltaTime;
        }
    }
}
 