using System;
using ArchitectsLibrary.Handlers;
using RotA.Interfaces;
using RotA.Mono.Equipment.IonKnifeActions;
using UnityEngine;

namespace RotA.Mono.Equipment
{
    [RequireComponent(typeof(EnergyMixin))]
    public class IonKnife : PlayerTool
    {
        // the blade object to disable when the knife is depleted
        public GameObject bladeObject;
        
        IIonKnifeAction _currentAction;
        
        public override string animToolName => TechType.Knife.AsString(true);

        public override bool OnRightHandDown()
        {
            return !energyMixin.IsDepleted();
        }

        #region Event Initializations 
        void OnEnable()
        {
            energyMixin.onPoweredChanged += OnPoweredChanged;
            energyMixin.batterySlot.onAddItem += OnBatteryAdded;
            energyMixin.batterySlot.onRemoveItem += OnBatteryRemoved;
        }

        void OnDisable()
        {
            energyMixin.onPoweredChanged -= OnPoweredChanged;
            energyMixin.batterySlot.onAddItem -= OnBatteryAdded;
            energyMixin.batterySlot.onRemoveItem -= OnBatteryRemoved;
        }

        void OnPoweredChanged(bool powered)
        {
            if (bladeObject.activeSelf != powered)
            {
                bladeObject.SetActive(powered);
            }
        }

        void OnBatteryRemoved(InventoryItem _)
        {
            _currentAction = null;
        }
        #endregion
        
        // also an event initializer but it's useful to keep it unregioned :)
        void OnBatteryAdded(InventoryItem item)
        {
            var tt = item.item.GetTechType();

            // add more if needed
            if (tt == TechType.PrecursorIonCrystal)
                _currentAction = new PrecursorIonCrystalAction();
            else if (tt == AUHandler.ElectricubeTechType)
                _currentAction = new ElectricubeAction();
            else if (tt == AUHandler.RedIonCubeTechType)
                _currentAction = new RedIonCubeAction();
            else if (tt == AUHandler.OmegaCubeTechType)
                _currentAction = new OmegaCubeAction();
        }
    }
}
