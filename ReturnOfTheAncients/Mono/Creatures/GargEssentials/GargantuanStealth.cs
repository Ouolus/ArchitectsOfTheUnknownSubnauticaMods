using System;

namespace RotA.Mono.Creatures.GargEssentials
{
    using ECCLibrary;
    using UnityEngine;
    using static GargantuanConditions;
    
    class GargantuanStealth : MonoBehaviour
    {
        public GargantuanBehaviour gargBehaviour;
        public bool StealthActive { get; private set; }

        private const float kStealthMaxYLevel = -200f;

        private void UpdateStealthState()
        {
            StealthActive = GetStealthState();
        }

        private bool GetStealthState()
        {
            if (gargBehaviour.lastTarget.target != null)
            {
                return false;
            }

            if (transform.position.y > kStealthMaxYLevel)
            {
                return false;
            }

            return true;
        }

        private void Update()
        {
            UpdateStealthState();
        }
    }
}