using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace ArchitectsLibrary.Configuration
{
    [Menu("Architects Library", SaveOn = MenuAttribute.SaveEvents.ChangeValue | MenuAttribute.SaveEvents.SaveGame,
        LoadOn = MenuAttribute.LoadEvents.MenuOpened | MenuAttribute.LoadEvents.MenuRegistered)]
    class Config : ConfigFile
    {
        [Keybind("Decrement Size Button", Tooltip = "The button to decrement the size of the Precursor decoration objects")]
        public KeyCode DecrementSize = KeyCode.Comma;

        [Keybind("Increment Size Button", Tooltip = "The button to increment the size of the Precursor decoration objects")]
        public KeyCode IncrementSize = KeyCode.Period;
    }
}