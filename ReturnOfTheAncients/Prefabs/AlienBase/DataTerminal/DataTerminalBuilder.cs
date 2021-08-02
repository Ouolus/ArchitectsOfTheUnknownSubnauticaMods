namespace RotA.Prefabs.AlienBase.DataTerminal
{
    using Interfaces;
    using AlienBase;
    using UnityEngine;
    
    public class DataTerminalBuilder : IDataTerminalBuilder
    {
        DataTerminal _dataTerminal;

        public DataTerminalBuilder()
        {
            Reset();
        }

        public void SetupAudio(string audioPrefix, string subtitles)
        {
            _dataTerminal.AudioSettings = new RAudioSettings(audioPrefix, subtitles);
        }

        public void SetupPingClassIds(string[] pingClassIds)
        {
            _dataTerminal.PingClassIds = pingClassIds;
        }

        public void SetupAchievement(string achievementId)
        {
            _dataTerminal.AchievementId = achievementId;
        }

        public void SetupFX(Color? fxColor, bool hideSymbol)
        {
            _dataTerminal.FxSettings = new RFxSettings(fxColor, hideSymbol);
        }

        public void SetupStoryGoal(string encyKey, float delay = 5)
        {
            _dataTerminal.StoryGoalSettings = new RStoryGoalSettings(encyKey, delay);
        }

        public void SetupUnlockables(TechType[] techTypesToUnlock = null, TechType techTypeToAnalyze = TechType.None, float delay = 0)
        {
            _dataTerminal.Unlockables = new RUnlockables(techTypesToUnlock, techTypeToAnalyze, delay);
        }

        public void SetupTemplateTerminal(string terminalClassId)
        {
            _dataTerminal.TerminalClassId = terminalClassId;
        }

        public void SetupInteractable(bool interactable)
        {
            _dataTerminal.Interactable = interactable;
        }

        public DataTerminal GetTerminal()
        {
            var dataTerminal = _dataTerminal;
            
            Reset();
            
            return dataTerminal;
        }

        private void Reset()
        {
            _dataTerminal = new DataTerminal();
            SetupTemplateTerminal(DataTerminalPrefab.blueTerminalCID);
            SetupAudio("DataTerminalOutpost", "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            SetupPingClassIds(null);
            SetupStoryGoal(null);
            SetupInteractable(true);
        }
    }
}