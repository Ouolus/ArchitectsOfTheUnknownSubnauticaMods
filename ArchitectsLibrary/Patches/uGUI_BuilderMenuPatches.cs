namespace ArchitectsLibrary.Patches
{
    using Handlers;
    using Interfaces;
    using HarmonyLib;
    
    internal static class uGUI_BuilderMenuPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(uGUI_BuilderMenu), nameof(uGUI_BuilderMenu.Show));
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(uGUI_BuilderMenuPatches), nameof(ShowPrefix)));
            harmony.Patch(orig, prefix);
        }

        static void ShowPrefix()
        {
            if (Inventory.main.GetHeldTool() is not BuilderTool builder)
                return;

            ApplyCorrectGroups(builder);
        }

        static void ApplyCorrectGroups(BuilderTool tool)
        {
            BuilderManager.Initialize();
            BuilderManager.EnsureCorrectGroups(tool.GetComponent<IBuilderGroups>());
        }
    }
}