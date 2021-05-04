using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace ProjectAncients
{
    [Menu("Return of the Ancients", SaveOn = MenuAttribute.SaveEvents.ChangeValue, LoadOn = MenuAttribute.LoadEvents.MenuOpened | MenuAttribute.LoadEvents.MenuRegistered)]
    public class Config : ConfigFile
    {
        [Toggle("Override main menu", Tooltip = "Whether to use the Return of the Ancients main menu effects. Restart required.")]
        public bool OverrideMainMenu = true;
        [Toggle("Override loading screen", Tooltip = "Whether to use the custom Return of the Ancients loading screen. You may want to disable this to use any custom loading screen mods, or if you prefer the default loading screen.")]
        public bool OverrideLoadingScreen = true;
        [Slider(Label = "Roar screen shake intensity", Tooltip = "The intensity of the shaking effect created by the Gargantuan Adult's roar.", DefaultValue = 50f, Min = 0f, Max = 100f, Step = 1f)]
        public float RoarScreenShakeIntensity = 50f;
        public float GetRoarScreenShakeNormalized
        {
            get
            {
                return RoarScreenShakeIntensity / 100f;
            }
        }
    }
}
