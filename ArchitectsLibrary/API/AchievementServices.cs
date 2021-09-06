using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ArchitectsLibrary.Configuration;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A class that helps with the creation and use of custom achievements.
    /// </summary>
    public static class AchievementServices
    {
        internal static Dictionary<string, Achievement> registeredAchievements = new();

        private static FMODAsset UnlockSound => uGUI_PopupNotification.main.soundEncyUnlock;

        private static string AchievementNotificationFormat
        {
            get
            {
                return string.Format("<color=#{0:X}{1:X}{2:X}>{{0}}</color>\n<color=#{3:X}{4:X}{5:X}>{{1}}</color>", new object[]
                {
                    AchievementNameColor.r,
                    AchievementNameColor.g,
                    AchievementNameColor.b,
                    AchievementDescriptionColor.r,
                    AchievementDescriptionColor.g,
                    AchievementDescriptionColor.b
                });
            }
        }

        private static Color32 AchievementNameColor => new (137, byte.MaxValue, 137, byte.MaxValue);

        private static Color32 AchievementDescriptionColor => new (119, 205, byte.MaxValue, byte.MaxValue);

        /// <summary>
        /// Registers an achievement into the game.
        /// </summary>
        /// <param name="id">The ID to be used for unlocking this achievement or checking it is locked.</param>
        /// <param name="name">The LANGUAGE KEY of the name of the achievement shown in the popup and in the achievement list.</param>
        /// <param name="icon">The icon of the achievement shown in the popup and in the achievement list. Should be 256x128 and should respect the curved rectangular shape of the popup frame. If left null, uses a default sprite.</param>
        /// <param name="lockedDescription">The LANGUAGE KEY of the description shown in the achievements list before you unlock this achievement.</param>
        /// <param name="unlockedDescription">The LANGUAGE KEY of the description shown in the achievements list after you unlock this achievement.</param>
        /// <param name="hideIconWhenLocked">Determines if the icon in the list should be shown as a question mark when it has not been unlocked.</param>
        /// <param name="totalTasks">The amount of times a specific task must be completed in order for the achievement to be unlocked.</param>
        /// <param name="showAsPercent">For achievements where total tasks > 1. Whether to show the completion as 'X/Y' or as 'N%".</param>
        public static void RegisterAchievement(string id, string name, Sprite icon, string lockedDescription, string unlockedDescription, bool hideIconWhenLocked, int totalTasks = 1, bool showAsPercent = false)
        {
            registeredAchievements.Add(id, new Achievement(id, name, icon, lockedDescription, unlockedDescription, hideIconWhenLocked, totalTasks, showAsPercent));
        }

        /// <summary>
        /// Instantly completes this achievement across all saves. Should only be called while in a save.
        /// </summary>
        /// <param name="id">The ID of the achievement.</param>
        public static void CompleteAchievement(string id)
        {
            SetAchievementCompletion(id, GetAchievement(id).totalTasks);
        }

        /// <summary>
        /// Sets an achievement's completion to <paramref name="tasks"/>.
        /// </summary>
        /// <param name="id">The id of the achievement.</param>
        /// <param name="tasks">The tasks done for this achievement.</param>
        public static void SetAchievementCompletion(string id, int tasks)
        {
            bool wasIncomplete = !GetAchievementComplete(id);
            if (Main.achievementData.achievements.ContainsKey(id))
            {
                Main.achievementData.achievements[id] = tasks;
            }
            else
            {
                Main.achievementData.achievements.Add(id, tasks);
            }
            Main.achievementData.Save();
            if (wasIncomplete && tasks >= GetAchievement(id).totalTasks)
            {
                ShowAchievementCompletePopup(id);
            }
        }

        /// <summary>
        /// Changes the completion of an achievement by <paramref name="amount"/>.
        /// </summary>
        /// <param name="id">The id of the achievement.</param>
        /// <param name="amount">The amount to change this achievement's completion.</param>
        public static void ChangeAchievementCompletion(string id, int amount)
        {
            SetAchievementCompletion(id, Mathf.Clamp(GetTasksCompleted(id) + amount, 0, GetAchievement(id).totalTasks));
        }

        /// <summary>
        /// Checks whether an achievement has been complete or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetAchievementComplete(string id)
        {
            return GetTasksCompleted(id) >= GetAchievement(id).totalTasks;
        }

        private static Achievement GetAchievement(string id)
        {
            return registeredAchievements[id];
        }

        /// <summary>
        /// The amount of tasks that have been completed towards a specific achievement.
        /// </summary>
        /// <param name="achievementId"></param>
        /// <returns></returns>
        public static int GetTasksCompleted(string achievementId)
        {
            Main.achievementData.Load();
            return Main.achievementData.achievements.GetOrDefault(achievementId, 0);
        }

        private static void ShowAchievementCompletePopup(string id)
        {
            Achievement achievement = GetAchievement(id);
            uGUI_PopupNotification.main.Set(PDATab.None, string.Empty, Language.main.Get("AchievementComplete"), string.Format(AchievementNotificationFormat, achievement.name, achievement.unlockedDescription), null, GetAchievementSprite(id, false));
            uGUI_PopupNotification.main.TryPlaySound(UnlockSound);
        }

        private static Sprite GetAchievementSprite(string id, bool showSpecialIconIfHidden)
        {
            Achievement achievement = GetAchievement(id);
            if (showSpecialIconIfHidden)
            {
                if (GetAchievementComplete(id))
                {
                    if (achievement.icon != null)
                    {
                        return achievement.icon;
                    }
                }
            }
            else
            {
                if (achievement.icon != null)
                {
                    return achievement.icon;
                }
            }
            return Main.assetBundle.LoadAsset<Sprite>("DefaultAchievementIcon");
        }

        internal struct Achievement
        {
            public string id;
            public string name;
            public Sprite icon;
            public string lockedDescription;
            public string unlockedDescription;
            public bool hideWhenLocked;
            public int totalTasks;
            public bool showAsPercent;

            public Achievement(string id, string name, Sprite icon, string lockedDescription, string unlockedDescription, bool hideWhenLocked, int totalTasks, bool showAsPercent)
            {
                this.id = id;
                this.name = name;
                this.icon = icon;
                this.lockedDescription = lockedDescription;
                this.unlockedDescription = unlockedDescription;
                this.hideWhenLocked = hideWhenLocked;
                this.totalTasks = totalTasks;
                this.showAsPercent = showAsPercent;
            }
        }
    }
}
