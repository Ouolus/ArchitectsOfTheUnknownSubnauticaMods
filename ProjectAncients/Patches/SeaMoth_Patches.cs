using HarmonyLib;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    class SeaMoth_Patches
    {
        [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleChange))]
        [HarmonyPostfix]
        static void OnUpgradeModuleUse_Postfix(SeaMoth __instance, TechType techType, int slotID)
        {
            if (techType == Mod.electricalDefenseMk2.TechType)
            {
                float charge = __instance.quickSlotCharge[slotID];
                float slotCharge = __instance.GetSlotCharge(slotID);
                var electricalDefense = Utils.SpawnZeroedAt(__instance.seamothElectricalDefensePrefab, __instance.transform)
                    .GetComponent<ElectricalDefense>();
                electricalDefense.charge = charge;
                electricalDefense.chargeScalar = slotCharge;
            }
        }
    }
}