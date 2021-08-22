using ArchitectsLibrary.API;
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
            HandReticle.main.SetInteractText(LanguageSystem.Get("BlackHoleHandTarget"));
        }

        public void OnHandClick(GUIHand hand)
        {
            if (Time.time < _clickCooldown)
                return;

            if (_attempts > 0)
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
            CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDABlackHole5"), "BlackHoleInteract5");
            IngameMenu.main.mainPanel.SetActive(false);
            AchievementServices.CompleteAchievement("TouchBlackHole");
            yield return new WaitForSeconds(0.5f);
            yield return IngameMenu.main.SaveGameAsync();
            ErrorMessage.AddMessage(LanguageSystem.Get("BlackHoleCrashErrorMessage"));
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
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDABlackHole1"), "BlackHoleInteract1");
                audioClipLength = 8f;
            }
            else if (attemptsNow == 1)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDABlackHole2"), "BlackHoleInteract2");
                audioClipLength = 3f;
            }
            else if (attemptsNow == 0)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDABlackHole4"), "BlackHoleInteract4");
                audioClipLength = 5f;
            }
        }
    }
}
