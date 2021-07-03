using RotA.Mono.Creatures.GargEssentials;
using UnityEngine;

namespace RotA.Mono.Modules
{
    public class ZapOnDamage : MonoBehaviour, IOnTakeDamage
    {
        public GameObject zapPrefab;
        public float cooldown = 10f;
        public float energyCost = 30f;
        public float superchargeEnergyCost = 300f;
        private Vehicle vehicle;
        private LiveMixin myLiveMixin;
        private float timeCanZapAgain;

        void Start()
        {
            vehicle = GetComponent<Vehicle>();
            myLiveMixin = gameObject.GetComponent<LiveMixin>();
            myLiveMixin.damageReceivers = myLiveMixin.gameObject.GetComponents<IOnTakeDamage>();
        }

        public void Zap(bool superCharge)
        {
            var obj = Object.Instantiate(zapPrefab);
            obj.name = "AutoZap";

            var fxElectSpheres = zapPrefab.GetComponent<ElectricalDefense>().fxElecSpheres;
            var defenseSound = zapPrefab.GetComponent<ElectricalDefense>().defenseSound;

            var ed = obj.GetComponent<ElectricalDefense>();
            Object.DestroyImmediate(ed);

            var edMk2 = obj.EnsureComponent<ElectricalDefenseMK2>();
            if (edMk2 is not null)
            {
                edMk2.fxElectSpheres = fxElectSpheres;
            }

            var electricalDefense = Utils.SpawnZeroedAt(obj, transform).GetComponent<ElectricalDefenseMK2>();

            if (electricalDefense is not null)
            {
                if (superCharge)
                {
                    electricalDefense.attackType = ElectricalDefenseMK2.AttackType.ArchitectElectricity;
                }
                else
                {
                    electricalDefense.attackType = ElectricalDefenseMK2.AttackType.SmallElectricity;
                }
                electricalDefense.charge = 1f;
                electricalDefense.chargeScalar = 1f;
            }

            obj.SetActive(true);
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            bool shouldSuperCharge = ShouldSuperCharge(damageInfo, out bool superchargeFailed);
            if (GetCanZap(damageInfo, shouldSuperCharge))
            {
                if (superchargeFailed)
                {
                    ErrorMessage.AddMessage("Prawn suit must be above 75% power to perform ionic pulse.");
                }
                if (vehicle is null || vehicle.ConsumeEnergy(GetEnergyUsage(shouldSuperCharge)))
                {
                    Zap(shouldSuperCharge);
                    timeCanZapAgain = Time.time + 5f;
                }
            }
        }

        public bool GetCanZap(DamageInfo damageInfo, bool shouldSuperCharge)
        {
            if (damageInfo == null)
            {
                //Why are we even checking??
                return false;
            }
            if (damageInfo.damage < 15)
            {
                //Not enough damage, a waste of charge.
                return false;
            }
            if (Time.time < timeCanZapAgain)
            {
                //Still on cooldown.
                return false;
            }
            if (vehicle is not null && !vehicle.HasEnoughEnergy(GetEnergyUsage(shouldSuperCharge) + 1f))
            {
                //Not worth using the vehicle's energy at this point
                return false;
            }
            return true;
        }

        public float GetEnergyUsage(bool superCharge)
        {
            if (superCharge)
            {
                return superchargeEnergyCost;
            }
            return energyCost;
        }

        public bool ShouldSuperCharge(DamageInfo damageInfo, out bool superChargeFailed)
        {
            superChargeFailed = false;
            if (damageInfo == null)
            {
                return false;
            }
            if (damageInfo.dealer == null)
            {
                return false;
            }
            if (damageInfo.dealer.GetComponent<GargantuanBehaviour>() is null)
            {
                return false;
            }
            if (vehicle is not null && !vehicle.HasEnoughEnergy(superchargeEnergyCost + 1f))
            {
                superChargeFailed = true;
                return false;
            }
            return true;
        }
    }
}
