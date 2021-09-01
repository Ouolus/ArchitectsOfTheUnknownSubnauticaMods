using Oculus.Newtonsoft.Json;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace RotA
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
        
        [JsonIgnore]
        public float GetRoarScreenShakeNormalized => RoarScreenShakeIntensity / 100f;
        
        [Keybind("Warp to base key", Tooltip = "The key that needs to be pressed (twice) to return to your base.")]
        public KeyCode WarpToBaseKey = KeyCode.B;

        [Keybind("Ion dash key", Tooltip = "The key that needs to be pressed to initiate an ion dash if you have the upgrade module for the Prawn Suit.")]
        public KeyCode PrawnSuitDashKey = KeyCode.LeftShift;
    }
}
