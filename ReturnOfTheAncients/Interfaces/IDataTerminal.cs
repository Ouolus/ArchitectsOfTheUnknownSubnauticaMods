using UnityEngine;

namespace RotA.Interfaces
{
    using Prefabs.AlienBase;
    
    public interface IDataTerminal
    {
        RAudioSettings AudioSettings { get; set; }
        
        RFxSettings FxSettings { get; set; }
        
        RStoryGoalSettings StoryGoalSettings { get; set; }
        
        RUnlockables Unlockables { get; set; }

        string[] PingClassIds { get; set; }
         
        string AchievementId { get; set; }

        string TerminalClassId { get; set; }
        
        bool Interactable { get; set; }
    }
}