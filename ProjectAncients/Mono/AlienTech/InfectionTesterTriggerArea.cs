using UnityEngine;

namespace ProjectAncients.Mono
{
    public class InfectionTesterTriggerArea : MonoBehaviour
    {
		private void OnTriggerEnter(Collider other)
		{
			GameObject gameObject = UWE.Utils.GetEntityRoot(other.gameObject);
			if (!gameObject)
			{
				gameObject = other.gameObject;
			}
			if (gameObject.GetComponent<Player>() != null)
			{
				terminal.OnTerminalAreaEnter();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			GameObject gameObject = UWE.Utils.GetEntityRoot(other.gameObject);
			if (!gameObject)
			{
				gameObject = other.gameObject;
			}
			if (gameObject.GetComponent<Player>() != null)
			{
				terminal.OnTerminalAreaExit();
			}
		}

		public InfectionTesterOpenDoor terminal;
	}
}
