using SMLHelper.V2.Options.Attributes;
using SMLHelper.V2.Json;

namespace LeviathanEggs.Configurations
{
    [Menu("Creature Eggs plus", SaveOn = MenuAttribute.SaveEvents.ChangeValue, LoadOn = MenuAttribute.LoadEvents.MenuOpened | MenuAttribute.LoadEvents.MenuRegistered)]
    public class Config : ConfigFile
    {
        /*[Toggle("Global Staged Growth", Tooltip = "makes every single Creature that has a next stager creature (i.e: for Sea Emperor Baby it's the Sea Emperor Juvenile) grow up till they reach the Adult Stage")]
        public bool StagedGrowth = true;*/

        [Toggle("Global Growth", Tooltip = "makes every single creature in the game grow up until the reach the default size. (1.00)")]
        public bool GlobalGrowth = false;
    }
}
