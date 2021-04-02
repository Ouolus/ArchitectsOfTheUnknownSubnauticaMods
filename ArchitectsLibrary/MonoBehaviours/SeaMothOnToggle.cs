using ArchitectsLibrary.Interfaces;
using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    internal class SeaMothOnToggle : MonoBehaviour
    {
        public ISeaMothOnToggle seaMothOnToggle;
        public SeaMoth seaMoth;

        public TechType techType;

        public int slotID;

        void OnEnable() => InvokeRepeating(nameof(DoThing), 0f, seaMothOnToggle.Cooldown);

        void OnDisable() => CancelInvoke(nameof(DoThing));

        void DoThing()
        {
            seaMothOnToggle.OnToggle(slotID, seaMoth);
        }
    }
}