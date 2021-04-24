using System.Collections;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;
using ProjectAncients.Mono;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class InfectionTesterTerminal : Spawnable
    {
        const string baseClassId = "b1f54987-4652-4f62-a983-4bf3029f8c5b";

        public InfectionTesterTerminal(string classId)
            : base(classId, "Forcefield Control", ".")
        {
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };
#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab(baseClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            PrecursorDisableGunTerminal disableGun = obj.GetComponentInChildren<PrecursorDisableGunTerminal>(true);
            DisableEmissiveOnStoryGoal disableEmissive = obj.GetComponent<DisableEmissiveOnStoryGoal>();
            if (disableEmissive)
            {
                Object.DestroyImmediate(disableEmissive);
            }
            var openDoor = disableGun.gameObject.AddComponent<InfectionTesterOpenDoor>();
            openDoor.glowMaterial = disableGun.glowMaterial;
            openDoor.glowRing = disableGun.glowRing;
            openDoor.useSound = disableGun.useSound;
            openDoor.openLoopSound = disableGun.openLoopSound;
            openDoor.curedUseSound = disableGun.curedUseSound;
            openDoor.accessGrantedSound = disableGun.accessGrantedSound;
            openDoor.accessDeniedSound = disableGun.accessDeniedSound;
            openDoor.cinematic = disableGun.cinematic;
            openDoor.onPlayerCuredGoal = disableGun.onPlayerCuredGoal;
            Object.DestroyImmediate(disableGun);

            var triggerArea_old = obj.GetComponentInChildren<PrecursorDisableGunTerminalArea>();
            var triggerArea = triggerArea_old.gameObject.AddComponent<InfectionTesterTriggerArea>();
            Object.DestroyImmediate(triggerArea_old);
            triggerArea.terminal = openDoor;
            obj.SetActive(false);
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(baseClassId);
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            PrecursorDisableGunTerminal disableGun = obj.GetComponentInChildren<PrecursorDisableGunTerminal>(true);
            DisableEmissiveOnStoryGoal disableEmissive = obj.GetComponent<DisableEmissiveOnStoryGoal>();
            if (disableEmissive)
            {
                Object.DestroyImmediate(disableEmissive);
            }
            var openDoor = disableGun.gameObject.AddComponent<InfectionTesterOpenDoor>();
            openDoor.glowMaterial = disableGun.glowMaterial;
            openDoor.glowRing = disableGun.glowRing;
            openDoor.useSound = disableGun.useSound;
            openDoor.openLoopSound = disableGun.openLoopSound;
            openDoor.curedUseSound = disableGun.curedUseSound;
            openDoor.accessGrantedSound = disableGun.accessGrantedSound;
            openDoor.accessDeniedSound = disableGun.accessDeniedSound;
            openDoor.cinematic = disableGun.cinematic;
            openDoor.onPlayerCuredGoal = disableGun.onPlayerCuredGoal;
            Object.DestroyImmediate(disableGun);

            var triggerArea_old = obj.GetComponentInChildren<PrecursorDisableGunTerminalArea>();
            var triggerArea = triggerArea_old.gameObject.AddComponent<InfectionTesterTriggerArea>();
            Object.DestroyImmediate(triggerArea_old);
            triggerArea.terminal = openDoor;
            obj.SetActive(false);
            
            gameObject.Set(obj);
        }
#endif
    }
}
