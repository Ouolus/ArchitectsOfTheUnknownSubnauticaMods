using UnityEngine;

namespace RotA.Mono.Commands
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
                if (ECCLibrary.ECCHelpers.CompareStrings(go.name, "GargantuanVoid(Clone)", ECCLibrary.ECCStringComparison.Equals))
                {
                    go.FindChild("AdultGargModel").SetActive(false);
                    GameObject trollFace = go.FindChild("TrollFace");
                    trollFace.SetActive(true);
                    trollFace.EnsureComponent<TrollFaceTracker>().enabled = true;
                    GargantuanRoar roar = go.GetComponent<GargantuanRoar>();
                    if (roar != null)
                    {
                        roar.audioSource.enabled = false;
                        roar.enabled = false;
                    }
                    go.EnsureComponent<TrollVoice>().enabled = true;
                    go.name = "GargantuanVoidTroll";
                    continue;
                }
                if (ECCLibrary.ECCHelpers.CompareStrings(go.name, "GargantuanVoidTroll", ECCLibrary.ECCStringComparison.Contains))
                {
                    go.FindChild("AdultGargModel").SetActive(true);
                    GameObject trollFace = go.FindChild("TrollFace");
                    trollFace.SetActive(false);
                    GargantuanRoar roar = go.GetComponent<GargantuanRoar>();
                    if (roar != null)
                    {
                        roar.audioSource.enabled = true;
                        roar.enabled = true;
                    }
                    go.name = "GargantuanVoid(Clone)";
                    go.GetComponent<TrollVoice>().enabled = false;
                    trollFace.GetComponent<TrollFaceTracker>().enabled = false;
                    continue;
                }
            }
        }
    }
}
