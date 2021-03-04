using ECCLibrary.Internal;
using UnityEngine;

namespace ProjectAncients.Mono
{
	// Assures there is only ever one NATURALLY spawned adult.
	public class AdultGargSingleton : MonoBehaviour
	{
		private static AdultGargSingleton main;

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
			if (main != null)
			{
				ECCLog.AddMessage("Multiple VOID adult gargantuans detected.");
			}
			main = this;
		}
	}
}
