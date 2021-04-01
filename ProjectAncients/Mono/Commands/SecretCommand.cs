using UnityEngine;

namespace ProjectAncients.Mono.Commands
{
    public class SecretCommand : MonoBehaviour
    {
        void Start()
        {
            DevConsole.RegisterConsoleCommand(this, "togglecinematic", false, true);
        }

        private void OnConsoleCommand_togglecinematic(NotificationCenter.Notification n)
        {
            GameObject[] gameObjects = Object.FindObjectsOfType<GameObject>();
            foreach(GameObject go in gameObjects)
            {
                if (ECCLibrary.ECCHelpers.CompareStrings(go.name, "GargantuanVoid", ECCLibrary.ECCStringComparison.Contains))
                {
                    go.FindChild("AdultGargModel").SetActive(false);
                    GameObject trollFace = go.FindChild("TrollFace");
                    trollFace.SetActive(true);
                    trollFace.EnsureComponent<TrollFaceTracker>();
                    GargantuanRoar roar = go.GetComponent<GargantuanRoar>();
                    if (roar != null)
                    {
                        Destroy(roar.audioSource);
                        Destroy(roar);
                    }
                    go.EnsureComponent<TrollVoice>();
                }
            }
        }
    }
}
