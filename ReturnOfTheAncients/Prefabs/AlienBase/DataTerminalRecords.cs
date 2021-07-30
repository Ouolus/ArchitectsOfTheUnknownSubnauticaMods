using UnityEngine;

namespace RotA.Prefabs.AlienBase
{
    public record RAudioSettings(string AudioPrefix, string Subtitles);

    public record RFxSettings(Color? FxColor, bool HideSymbol);

    public record RStoryGoalSettings(string EncyKey, float Delay);

    public record RUnlockables(TechType[] TechTypesToUnlock, TechType TechTypeToAnalyze);
}