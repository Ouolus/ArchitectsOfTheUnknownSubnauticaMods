using UnityEngine;
using UWE;
using ArchitectsLibrary.API;

namespace RotA.Mono.AlienTech
{
    public class DataTerminalAchievement : MonoBehaviour
    {
        public string achievement;

        public void OnStoryHandTarget()
        {
            AchievementServices.CompleteAchievement(achievement);
        }
    }
}
