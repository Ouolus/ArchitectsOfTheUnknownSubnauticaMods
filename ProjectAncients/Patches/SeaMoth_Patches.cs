using HarmonyLib;
using ProjectAncients.Mono.Modules;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    class SeaMoth_Patches
    {
        [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleUse))]
        [HarmonyPostfix]
        static void OnUpgradeModuleUse_Postfix(SeaMoth __instance, TechType techType, int slotID)
        {
            if (techType == Mod.electricalDefenseMk2.TechType)
            {
                var obj = Object.Instantiate(__instance.seamothElectricalDefensePrefab);
                obj.name = "ElectricalDefenseMK2";
                
                var ed = obj.GetComponent<ElectricalDefense>() ?? obj.GetComponentInParent<ElectricalDefense>();
                if (ed is not null)
                {
                    Object.Destroy(ed);
                }
                
                var edMk2 = obj.EnsureComponent<ElectricalDefenseMK2>();
                if (edMk2 is not null)
                {
                    edMk2.fxElectSpheres = __instance.seamothElectricalDefensePrefab.GetComponent<ElectricalDefense>().fxElecSpheres;
                    edMk2.defenseSound = __instance.seamothElectricalDefensePrefab.GetComponent<ElectricalDefense>().defenseSound;
                }

                float charge = __instance.quickSlotCharge[slotID];
                float slotCharge = __instance.GetSlotCharge(slotID);

                var electricalDefense = Utils
                    .SpawnZeroedAt(obj, __instance.transform)
                    .GetComponent<ElectricalDefenseMK2>();
                if (electricalDefense is not null)
                {
                    electricalDefense.charge = charge;
                    electricalDefense.chargeScalar = slotCharge;
                }
                __instance.quickSlotTimeUsed[slotID] = Time.time;
                __instance.quickSlotCooldown[slotID] = 5;
            }
        }
    }
}