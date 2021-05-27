using System.Collections;
using UnityEngine;

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
                _clickCooldown = Time.time + 3f;
                TryPlayVoiceLine(_attempts);
                return;
            }

            solarSystemDestroyed = true;
            StartCoroutine(Crash());
        }

        IEnumerator Crash()
        {
            IngameMenu.main.mainPanel.SetActive(false);
            yield return IngameMenu.main.SaveGameAsync();
            ErrorMessage.AddMessage("Save file corrupted.");
            GameObject whiteout = GameObject.Instantiate(Mod.assetBundle.LoadAsset<GameObject>("BlackHoleScreenEffect"));
            GameObject.DontDestroyOnLoad(whiteout);
            whiteout.AddComponent<SceneCleanerPreserve>();

            yield return IngameMenu.main.QuitGameAsync(false);
        }

        void TryPlayVoiceLine(int attemptsNow)
        {
            if (attemptsNow == 2)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("BlackHole1"), "BlackHoleInteract1", "No.");
            }
            else if (attemptsNow == 1)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("BlackHole2"), "BlackHoleInteract2", "Do not attempt. You will be destroyed.");
            }
            else if (attemptsNow == 0)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("DeathImminent"), "DeathImminent", "Warning: Death imminent.");
            }
        }
    }
}
