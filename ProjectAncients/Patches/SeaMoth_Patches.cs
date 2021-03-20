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
            float cooldown = 0;
            if (techType == Mod.electricalDefenseMk2.TechType)
            {
                var obj = Object.Instantiate(__instance.seamothElectricalDefensePrefab);
                
                var ed = obj.GetComponent<ElectricalDefense>() ?? obj.GetComponentInParent<ElectricalDefense>();
                if (ed is not null)
                {
                    var edMk2 = obj.EnsureComponent<ElectricalDefenseMK2>();
                    if (edMk2 is not null)
                    {
                        edMk2.fxElectSpheres = ed.fxElecSpheres;
                        edMk2.defenseSound = ed.defenseSound;
                        
                        Object.Destroy(ed, 0.1f);
                    }
                }

                float charge = __instance.quickSlotCharge[slotID];
                float slotCharge = __instance.GetSlotCharge(slotID);

                var electricalDefense = Utils
                    .SpawnZeroedAt(__instance.seamothElectricalDefensePrefab, __instance.transform)
                    .GetComponent<ElectricalDefenseMK2>();
                if (electricalDefense is not null)
                {
                    electricalDefense.charge = charge;
                    electricalDefense.chargeScalar = slotCharge;
                }
                cooldown = 5;
            }

            __instance.quickSlotTimeUsed[slotID] = Time.time;
            __instance.quickSlotCooldown[slotID] = cooldown;
        }
    }
}