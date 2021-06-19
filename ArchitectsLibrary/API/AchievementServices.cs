using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A class that helps with the creation and use of custom achievements.
    /// </summary>
    public static class AchievementServices
    {
        internal static Dictionary<string, Achievement> registeredAchievements = new Dictionary<string, Achievement>();

        /// <summary>
        /// Registers an achievement into the game.
        /// </summary>
        /// <param name="id">The ID to be used for unlocking this achievement or checking it is locked.</param>
        /// <param name="name">The name of the achievement shown in the popup and in the achievement list.</param>
        /// <param name="icon">The icon of the achievement shown in the popup and in the achievement list. Should be 256x128 and should respect the curved rectangular shape of the popup frame. If left null, uses a default sprite.</param>
        /// <param name="lockedDescription">The description shown in the achievements list before you unlock this achievement.</param>
        /// <param name="unlockedDescription">The description shown in the achievements list after you unlock this achievement.</param>
        /// <param name="hideIconWhenLocked">Determines if the icon in the list should be shown as a question mark when it has not been unlocked.</param>
        public static void RegisterAchievement(string id, string name, Sprite icon, string lockedDescription, string unlockedDescription, bool hideIconWhenLocked)
        {
            registeredAchievements.Add(id, new Achievement(id, name, icon, lockedDescription, unlockedDescription, hideIconWhenLocked));
        }

        /// <summary>
        /// Completes this achievement across all saves. Should only be called while in a save.
        /// </summary>
        /// <param name="id">The ID of the achievement.</param>
        public static void CompleteAchievement(string id)
        {
            bool completedFirstTime = !IsAchievementComplete(id);
            PlayerPrefs.SetInt(GetPlayerPrefName(id), 1);
            if (completedFirstTime)
            {
                ErrorMessage.AddMessage($"You have completed <color=#ADF8FFFF>{registeredAchievements[id].name}</color>!");
                //show popup
            }
        }

        /// <summary>
        /// Checks whether an achievement has been complete or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsAchievementComplete(string id)
        {
            return PlayerPrefs.GetInt(GetPlayerPrefName(id), 0) == 1;
        }

        private static string GetPlayerPrefName(string achievementId)
        {
            return $"ALAchievement_{achievementId}";
        }

        internal struct Achievement
        {
            public string id;
            public string name;
            public Sprite icon;
            public string lockedDescription;
            public string unlockedDescription;
            public bool hideWhenLocked;

            public Achievement(string id, string name, Sprite icon, string lockedDescription, string unlockedDescription, bool hideWhenLocked)
            {
                this.id = id;
                this.name = name;
                this.icon = icon;
                this.lockedDescription = lockedDescription;
                this.unlockedDescription = unlockedDescription;
                this.hideWhenLocked = hideWhenLocked;
            }
        }
    }
}
