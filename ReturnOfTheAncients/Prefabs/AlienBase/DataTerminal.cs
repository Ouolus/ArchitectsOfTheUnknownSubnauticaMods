
namespace RotA.Prefabs.AlienBase
{
    using Interfaces;
    
    public struct DataTerminal : IDataTerminal
    {
        public RAudioSettings AudioSettings { get; set; }
        public RFxSettings FxSettings { get; set; }
        public RStoryGoalSettings StoryGoalSettings { get; set; }
        public RUnlockables Unlockables { get; set; }

        public string[] PingClassIds { get; set; }
        
        public string AchievementId { get; set; }
        
        public string TerminalClassId { get; set; }
        
        public bool Interactable { get; set; }
    }
}