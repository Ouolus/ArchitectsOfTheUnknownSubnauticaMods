using Story;
using UnityEngine;

namespace RotA.Mono.Creatures.Baby
{
	public class GargantuanBabyTeleport : MonoBehaviour
	{
		public void Start()
		{
			creature = GetComponent<Creature>();
			creature.friend = Player.main.gameObject;
			InvokeRepeating("WarpToPlayer", UnityEngine.Random.value * warpInterval, warpInterval);
			cuteFishGoal.Trigger();
			UpdateCinematicTargetActive();
		}

		public void OnDrop()
		{
			creature.leashPosition = Player.main.transform.position;
			UpdateCinematicTargetActive();
		}

		void UpdateCinematicTargetActive()
        {
			WaterParkCreature component = GetComponent<WaterParkCreature>();
			bool flag = component != null && component.IsInsideWaterPark();
			cinematicTarget.SetActive(!flag);
		}

		private void WarpToPlayer()
		{
			if (Player.main.GetBiomeString().StartsWith("precursor", System.StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			Vector3 vector = Player.main.transform.position - base.transform.position;
			if (vector.magnitude > warpDistance)
			{
				Vector3 vector2 = Player.main.transform.position - vector.normalized * this.warpDistance;
				vector2.y = Mathf.Min(vector2.y, -1f);
				int num = UWE.Utils.OverlapSphereIntoSharedBuffer(base.transform.position, 5f, -1, QueryTriggerInteraction.UseGlobal);
				for (int i = 0; i < num; i++)
				{
					if (UWE.Utils.sharedColliderBuffer[i].GetComponentInParent<SubRoot>())
					{
						return;
					}
				}
				transform.position = vector2;
				var swim = GetComponent<SwimBehaviour>();
                if (swim)
                {
					swim.overridingTarget = false;
                }
			}
		}

		public float warpInterval = 5f;
		public float warpDistance = 30f;
		public Creature creature;
		public GameObject cinematicTarget;

		static StoryGoal cuteFishGoal = new StoryGoal("Goal_CuteFish", Story.GoalType.Story, 0);
	}
}
