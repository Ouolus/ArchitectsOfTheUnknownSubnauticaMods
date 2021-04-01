using UnityEngine;

namespace ProjectAncients.Mono.Commands
{
    public class SecretCommand : MonoBehaviour
    {
        void Start()
        {
            DevConsole.RegisterConsoleCommand(this, "garg", false, true);
        }

        private void OnConsoleCommand_garg(NotificationCenter.Notification n)
        {
            GameObject garg = GameObject.Find("GargantuanVoid(Clone)");
            garg.FindChild("AdultGargModel").SetActive(false);
            GameObject trollFace = garg.FindChild("TrollFace");
            trollFace.SetActive(true);
            trollFace.AddComponent<TrollFaceTracker>();
        }
    }
}
