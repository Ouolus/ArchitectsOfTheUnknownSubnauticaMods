namespace ArchitectsLibrary.Handlers
{
    using API;
    using System.Collections.Generic;
    using Interfaces;
    using UnityEngine;
    using UnityEngine.UI;
    using Utility;

    internal static class AchievementsMenu
    {
        private const string kPrefabName = "AchievementList_Prefab";
        private const string kButtonPrefabName = "AchievementDisplay_Prefab";
        private const string kVanillaButtonPrefabName = "VanillaAchievementDisplay_Prefab";
        private static GameObject _current;

        // start showing the achievements menu, has a lot of slow instantation to get it working right, this was not fun to write
        internal static void Show()
        {
            var mainMenu = uGUI_MainMenu.main;
            if (_current == null && mainMenu != null)
            {
                _current = Object.Instantiate(Main.assetBundle.LoadAsset<GameObject>(kPrefabName));
                _current.transform.SetParent(mainMenu.transform.GetChild(0), false);
                _current.transform.localScale = Vector3.one;
                _current.transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(Hide);

                var displayData = GetDisplayData();
                if (displayData != null && displayData.Count > 0)
                {
                    var layoutGroupParent = _current.transform.Find("Viewport/VerticalLayoutGroup");
                    for (var i = 0; i < displayData.Count; i++)
                    {
                        var a = displayData[i];
                        var go = Object.Instantiate(Main.assetBundle.LoadAsset<GameObject>(a.isVanillaAchievement ? kVanillaButtonPrefabName : kButtonPrefabName));
                        go.transform.SetParent(layoutGroupParent, false);
                        go.transform.localScale = Vector3.one * 20f;

                        // basic achievement stuff
                        go.SearchChild("Icon").GetComponent<Image>().sprite = a.icon;
                        go.SearchChild("Name").GetComponent<Text>().text = a.displayText;
                        go.SearchChild("Description").GetComponent<Text>().text = a.descriptionText;

                        // more conditional things related to showing completion, based on the type of achievement
                        go.SearchChild("CompletionIcon").SetActive(a.GetComplete);
                        GameObject completionBar = go.SearchChild("CompletionBar");
                        completionBar.SetActive(!a.GetComplete & a.HasMultipleTasks);
                        completionBar.transform.Find("Mask/Bar").localScale = new Vector3(a.GetCompletionPercent, 1f, 1f);
                        GameObject completionText = go.SearchChild("CompletionText");
                        completionText.SetActive(!a.GetComplete & a.HasMultipleTasks);
                        completionText.GetComponent<Text>().text = a.showAsPercent ? string.Format("{0}%", Mathf.Round(a.GetCompletionPercent * 100f)) : string.Format("{0}/{1}", a.tasksDone, a.totalTasks);
                    }
                }
            }
        }

        // stop showing the achievements menu
        internal static void Hide()
        {
            Object.Destroy(_current);
        }

        // defines how all of the achievements will be displayed in the menu
        private static List<AchievementDisplayData> GetDisplayData()
        {
            var list = new List<AchievementDisplayData>();
            foreach (var pair in AchievementServices.registeredAchievements)
            {
                var completion = Main.achievementData.achievements == null ? 0 : Main.achievementData.achievements.GetOrDefault(pair.Key, 0);
                var a = pair.Value;
                list.Add(new AchievementDisplayData(a.name, a.unlockedDescription, completion, a.totalTasks, false, a.icon));
            }
            return list;
        }

        // internal struct that defines how an achievement is displayed in the menu, so both vanilla and custom achievments can utilize the same system
        private struct AchievementDisplayData
        {
            public bool isVanillaAchievement;
            public string displayText;
            public string descriptionText;
            public int tasksDone;
            public int totalTasks;
            public bool showAsPercent;
            public Sprite icon;

            public bool GetComplete => totalTasks == tasksDone;
            public bool HasMultipleTasks => totalTasks > 1;
            public float GetCompletionPercent => tasksDone / totalTasks;

            public AchievementDisplayData(string displayText, string descriptionText, int tasksDone, int totalTasks, bool showAsPercent, Sprite icon) // for custom achievements which have more to them
            {
                isVanillaAchievement = false;

                this.displayText = displayText;
                this.descriptionText = descriptionText;
                this.tasksDone = tasksDone;
                this.totalTasks = totalTasks;
                this.showAsPercent = showAsPercent;
                this.icon = icon;
            }

            public AchievementDisplayData(string displayText, string descriptionText, bool complete, Sprite icon) // for vanilla achievements
            {
                isVanillaAchievement = true;

                this.displayText = displayText;
                this.descriptionText = descriptionText;
                this.icon = icon;
                tasksDone = complete ? 1 : 0;
                totalTasks = 1;
                showAsPercent = false;
            }
        }
    }
}