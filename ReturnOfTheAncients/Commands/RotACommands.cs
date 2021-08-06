namespace RotA.Commands
{
    using Mono;
    using Mono.Creatures.GargEssentials;
    using Mono.Cinematics;
    using System.Reflection;
    using System.Text;
    using SMLHelper.V2.Commands;
    using UnityEngine;
    
    public static class RotACommands
    {
        private static StringBuilder _commandList;
        
        // commands must be public and static
        
        [ConsoleCommand("togglecinematic")]
        public static void ToggleCinematic()
        {
            GameObject[] gameObjects = Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in gameObjects)
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
                }
            }
        }
    }
}
