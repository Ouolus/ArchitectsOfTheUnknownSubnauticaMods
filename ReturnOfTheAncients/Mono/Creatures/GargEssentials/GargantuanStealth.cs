using System;

namespace RotA.Mono.Creatures.GargEssentials
{
    using ECCLibrary;
    using UnityEngine;
    using static GargantuanConditions;
    
    class GargantuanStealth : MonoBehaviour
    {
        public GargantuanBehaviour gargBehaviour;

        private const float kStealthMaxYLevel = -200f;

        public bool StealthActive()
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
            
        }
    }
}