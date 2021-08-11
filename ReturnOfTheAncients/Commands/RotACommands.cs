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
        
        [ConsoleCommand("gargdebug")]
        public static void GargDebug()
        {
            GargantuanBehaviour[] gargs = Object.FindObjectsOfType<GargantuanBehaviour>();
            foreach (var garg in gargs)
            {
                Rigidbody rb = garg.GetComponent<Rigidbody>();
                rb.isKinematic = !rb.isKinematic;
            }

        }
        [ConsoleCommand("rotacommands")]
        public static void RotACommandsList()
        {
            if (_commandList is null)
            {
                _commandList = new StringBuilder();

                foreach (var method in typeof(RotACommands).GetMethods())
                {
                    var consoleCommand = method.GetCustomAttribute<ConsoleCommandAttribute>(false);
                    
                    if (consoleCommand != null)
                        _commandList.AppendLine(consoleCommand.Command);
                }
            }
            ErrorMessage.AddMessage($"RotA commands list: \n{_commandList}");
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
            int children = 2;
            foreach (var garg in gargs)
            {
                for (int i = 0; i < children; i++)
                {
                    var obj = Object.Instantiate(garg.gameObject, garg.transform.position, garg.transform.rotation);
                    float newScale = garg.transform.localScale.x / children;
                    obj.GetComponent<Creature>().sizeDistribution = new AnimationCurve(new Keyframe[] { new Keyframe(0f, newScale), new Keyframe(1f, newScale) });
                    obj.transform.localScale = Vector3.one * newScale;
                }
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
