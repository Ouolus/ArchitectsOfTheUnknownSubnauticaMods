using System.Collections;
using UnityEngine;
using ArchitectsLibrary.API;

namespace RotA.Mono.AlienTech
{
    public class BlackHole : HandTarget, IHandTarget
    {
        int _attempts = 3;
        float _clickCooldown;

        public static bool solarSystemDestroyed;
        
        public void OnHandHover(GUIHand hand)
        {
            HandReticle.main.SetIcon(HandReticle.IconType.HandDeny);
            HandReticle.main.SetInteractText("Do not touch");
        }

        public void OnHandClick(GUIHand hand)
        {
            if (Time.time < _clickCooldown)
                return;
            
            if (_attempts >= 0)
            {
                _attempts--;
                Player.main.PlayGrab();
                TryPlayVoiceLine(_attempts, out float audioClipLength);
                _clickCooldown = Time.time + audioClipLength;
                return;
            }

            solarSystemDestroyed = true;
            StartCoroutine(Crash());
        }

        IEnumerator Crash()
        {
            IngameMenu.main.mainPanel.SetActive(false);
            AchievementServices.CompleteAchievement("TouchBlackHole");
            yield return IngameMenu.main.SaveGameAsync();
            ErrorMessage.AddMessage("Save file corrupted.");
            GameObject whiteout = GameObject.Instantiate(Mod.assetBundle.LoadAsset<GameObject>("BlackHoleScreenEffect"));
            GameObject.DontDestroyOnLoad(whiteout);
            whiteout.AddComponent<SceneCleanerPreserve>();
            yield return IngameMenu.main.QuitGameAsync(false);
        }

        void TryPlayVoiceLine(int attemptsNow, out float audioClipLength)
        {
            audioClipLength = 1f;
            if (attemptsNow == 2)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("BlackHole1"), "BlackHoleInteract1", "I strongly advise against interacting with this singularity. I calculate a 99.9% chance of immediate termination.");
                audioClipLength = 8f;
            }
            else if (attemptsNow == 1)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("BlackHole2"), "BlackHoleInteract2", "The safety of yourself and this PDA, please refrain.");
                audioClipLength = 3f;
            }
            else if (attemptsNow == 0)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("BlackHole4"), "BlackHoleInteract4", "Do not attempt. You still have an unsettled debt with Alterra Corporation requiring your attention.");
                audioClipLength = 5f;
            }
        }
    }
}
