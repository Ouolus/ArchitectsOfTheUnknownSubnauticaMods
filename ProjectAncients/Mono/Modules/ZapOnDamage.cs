using UnityEngine;
using ProjectAncients.Prefabs.Modules;

namespace ProjectAncients.Mono.Modules
{
    public class ZapOnDamage : MonoBehaviour
    {
        public GameObject zapPrefab;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Zap();
            }
        }

        public void Zap()
        {
            var obj = Object.Instantiate(zapPrefab);
            obj.name = "PrawnSuitZap";

            var fxElectSpheres = zapPrefab.GetComponent<ElectricalDefense>().fxElecSpheres;
            var defenseSound = zapPrefab.GetComponent<ElectricalDefense>().defenseSound;

            var ed = obj.GetComponent<ElectricalDefense>() ?? obj.GetComponentInParent<ElectricalDefense>();
            if (ed is not null)
            {
                Object.Destroy(ed);
            }

            var edMk2 = obj.EnsureComponent<ElectricalDefenseMK2>();
            if (edMk2 is not null)
            {
                edMk2.fxElectSpheres = fxElectSpheres;
                edMk2.defenseSound = defenseSound;
            }

            var electricalDefense = Utils.SpawnZeroedAt(obj, transform).GetComponent<ElectricalDefenseMK2>();

            if (electricalDefense is not null)
            {
                electricalDefense.charge = 1f;
                electricalDefense.chargeScalar = 1f;
            }
        }
    }
}
