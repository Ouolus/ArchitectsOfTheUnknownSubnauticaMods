using ECCLibrary.Internal;
using UnityEngine;

namespace ProjectAncients.Mono
{
	// Ensures there is only ever one NATURALLY spawned adult.
	public class VoidGargSingleton : MonoBehaviour
	{
		private static VoidGargSingleton main;

		public static bool AdultGargExists
		{
			get
			{
				if (main == null)
				{
					return false;
				}
				if (main.gameObject.activeSelf == false)
				{
					return false;
				}
				return true;
			}
		}

		void Awake()
		{
			main = this;
			InvokeRepeating("CheckDistance", Random.value, 10f);
		}

		void CheckDistance()
		{
			string playerBiome = Player.main.GetBiomeString();
			if (!VoidGargSpawner.IsVoidBiome(playerBiome) && !playerBiome.StartsWith("precursor", System.StringComparison.OrdinalIgnoreCase) && !playerBiome.StartsWith("prison", System.StringComparison.OrdinalIgnoreCase) && !playerBiome.StartsWith("observatory", System.StringComparison.OrdinalIgnoreCase))
			{
				float distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
				if (distance > 250f)
				{
					Destroy(gameObject);
				}
			}
		}
	}
}
