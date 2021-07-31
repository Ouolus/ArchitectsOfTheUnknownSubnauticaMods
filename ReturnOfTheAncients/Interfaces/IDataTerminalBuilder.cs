using RotA.Prefabs.AlienBase;
using UnityEngine;

namespace RotA.Interfaces
{
    public interface IDataTerminalBuilder
    {
        /// <summary>
        /// A method that takes enough information to setup an AudioSettings
        /// </summary>
        /// <param name="audioPrefix">The audio prefix string</param>
        /// <param name="subtitles">Subtitles</param>
        void SetupAudio(string audioPrefix, string subtitles);

        /// <summary>
        /// Setup PingType classIDs
        /// </summary>
        /// <param name="pingClassIds"></param>
        void SetupPingClassIds(string[] pingClassIds);
                
        /// <summary>
        /// Setup Achievement id
        /// </summary>
        /// <param name="achievementId"></param>
        void SetupAchievement(string achievementId);
        
        /// <summary>
        /// Setup Storygoal
        /// </summary>
        /// <param name="encyKey"></param>
        /// <param name="delay"></param>
        void SetupStoryGoal(string encyKey, float delay = 5);
                
        /// <summary>
        /// Setup Visual Effect
        /// </summary>
        /// <param name="fxColor"></param>
        /// <param name="hideSymbol"></param>
        void SetupFX(Color? fxColor, bool hideSymbol);
        
        /// <summary>
        /// Setup TechTypes that get unlocked and/or a TechType to analyze when interacted with the data terminal. 
        /// </summary>
        /// <param name="techTypesToUnlock"></param>
        /// <param name="techTypeToAnalyze"></param>
        void SetupUnlockables(TechType[] techTypesToUnlock, TechType techTypeToAnalyze, float delay);
        
        /// <summary>
        /// Template Data Terminal ClassID
        /// </summary>
        /// <param name="templateTerminalClassId"></param>
        void SetupTemplateTerminal(string templateTerminalClassId);

        /// <summary>
        /// Setup whether or not the DataTerminal should be interactable (whether should have <see cref="StoryHandTarget"/> or not.)
        /// </summary>
        /// <param name="interactable"></param>
        void SetupInteractable(bool interactable);

        /// <summary>
        /// Gets the current built terminal
        /// </summary>
        /// <returns></returns>
        DataTerminal GetTerminal();
    }
}