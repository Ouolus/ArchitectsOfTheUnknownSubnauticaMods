using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ArchitectsLibrary.Patches
{
    [HarmonyPatch(typeof(uGUI_MainMenu))]
    internal static class uGUI_MainMenuPatches
    {
        private const string kAchievementsButtonName = "Achievements";
        
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(uGUI_MainMenu), nameof(uGUI_MainMenu.Awake));
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(uGUI_MainMenuPatches), nameof(AwakePostfix)));
            harmony.Patch(orig, postfix: postfix);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(uGUI_MainMenu.Awake))]
        private static void AwakePostfix(uGUI_MainMenu __instance)
        {
            var buttonsPanel = __instance.transform.Find("Panel/MainMenu/PrimaryOptions/MenuButtons");
            if (buttonsPanel == null) 
                return;
            
            var playButton = buttonsPanel.GetChild(0).gameObject;
            if (playButton == null) 
                return;
                
            var achievementsButton = Object.Instantiate(playButton, buttonsPanel);
            achievementsButton.GetComponentInChildren<Text>().text = kAchievementsButtonName;
            achievementsButton.GetComponentInChildren<TranslationLiveUpdate>().translationKey = kAchievementsButtonName;
            achievementsButton.name = kAchievementsButtonName;
            achievementsButton.transform.SetSiblingIndex(1);
            var button = achievementsButton.GetComponentInParent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(Application.Quit);
        }
    }
}