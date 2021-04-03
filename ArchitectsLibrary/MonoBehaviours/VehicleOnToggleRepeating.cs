using ArchitectsLibrary.Interfaces;
using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    internal class VehicleOnToggleRepeating : MonoBehaviour
    {
        public IVehicleOnToggleRepeating vehicleOnToggle;
        public Vehicle vehicle;

        public TechType techType;

        public int slotID;

        void OnEnable() => InvokeRepeating(nameof(DoThing), 0f, vehicleOnToggle.RepeatingActionCooldown);

        void OnDisable() => CancelInvoke(nameof(DoThing));

        void DoThing()
        {
            vehicleOnToggle.DoRepeatingAction(slotID, vehicle);
        }
    }
}