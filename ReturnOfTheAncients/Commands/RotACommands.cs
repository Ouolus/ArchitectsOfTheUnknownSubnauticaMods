namespace RotA.Commands
{
    using Mono;
    using Mono.Creatures.GargEssentials;
    using Mono.Cinematics;
    using SMLHelper.V2.Commands;
    using UnityEngine;
    
    public static class RotACommands
    {
        // commands must be public and static
        
        [ConsoleCommand("rotacommands")]
        public static void RotACommandsList()
        {
            ErrorMessage.AddMessage("RotA commands list:\nclonegarg\nrotacommands\nsecretbasecutscene\nsunbeamgarg\ntogglecinematic");
        }

        [ConsoleCommand("secretbasecutscene")]
        public static void SecretBaseCutscene()
        {
            SecretBaseGargController.PlayCinematic();
        }

        [ConsoleCommand("sunbeamgarg")]
        public static void SunbeamGarg()
        {
            SunbeamGargController.PlayCinematic();
        }

        //the commands below are just for fun

        [ConsoleCommand("clonegarg")]
        public static void CloneGarg()
        {
            GargantuanBehaviour[] gargs = Object.FindObjectsOfType<GargantuanBehaviour>();
            foreach (var garg in gargs)
            {
                var obj = Object.Instantiate(garg.gameObject, garg.transform.position, garg.transform.rotation);
                obj.transform.localScale = garg.transform.localScale * 0.5f;
                Object.Destroy(garg.gameObject);
            }
        }

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
