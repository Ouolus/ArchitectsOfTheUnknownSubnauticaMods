using RotA.Mono.Cinematics;
using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class DataTerminalSecretCutscene : MonoBehaviour
    {
        private bool interactedThisSession;

        public void OnStoryHandTarget()
        {
            if (interactedThisSession == true)
            {
                return;
            }
            interactedThisSession = true;
            Invoke(nameof(StartCinematic), 3f);
        }

        private void StartCinematic()
        {
            SecretBaseGargController.PlayCinematic();
        }
    }
}
