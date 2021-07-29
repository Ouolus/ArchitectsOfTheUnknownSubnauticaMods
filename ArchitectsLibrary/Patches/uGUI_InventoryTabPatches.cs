using System.Text;
using ArchitectsLibrary.MonoBehaviours;
using HarmonyLib;

namespace ArchitectsLibrary.Patches
{
    internal static class uGUI_InventoryTabPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(uGUI_InventoryTab), nameof(uGUI_InventoryTab.GetTooltip));
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(uGUI_InventoryTabPatches), nameof(GetToolTipPrefix)));
            harmony.Patch(orig, prefix);
        }

        static bool GetToolTipPrefix(InventoryItem item, ref string __result)
        {
            if (item.item.gameObject.TryGetComponent(out PrecursorIonStorage _))
            {
                var sb = new StringBuilder();
                sb.AppendLine(TooltipFactory.InventoryItem(item));
                TooltipFactory.WriteDescription(sb, Language.main.Get(TooltipFactory.techTypeTooltipStrings.Get(item.item.GetTechType())));
                __result = sb.ToString();
                return false;
            }

            return true;
        }
    }
}