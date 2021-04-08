using UnityEngine;

namespace ProjectAncients.Mono.AlienTech
{
    /// <summary>
    /// Abstract class. Implement to call your own methods on this object when the player enters or exits a certain biome.
    /// </summary>
    public abstract class OnBiomeChanged : MonoBehaviour
    {
        /// <summary>
        /// The biome that will trigger  <see cref="OnTargetBiomeEntered"/> and  <see cref="OnTargetBiomeExited"/>.
        /// </summary>
        public string targetBiome;

        /// <summary>
        /// The interval between the biome checks. A value of 0 (every frame) may have a slight impact on performance.
        /// </summary>
        public float updateInterval = 0.5f;

        private float timeCallEnterAgain;
        private float timeCallExitAgain;
        private bool wasInBiome;

        /// <summary>
        /// Called when your targetBiome is enterered.
        /// </summary>
        protected abstract void OnTargetBiomeEntered();
        /// <summary>
        /// Called when your targetBiome is exited.
        /// </summary>
        protected abstract void OnTargetBiomeExited();

        /// <summary>
        /// Call this method to disallow <see cref="OnTargetBiomeEntered"/> from being called for 'cooldown' seconds.
        /// </summary>
        public void SetEnterCooldown(float cooldown)
        {
            timeCallEnterAgain = Time.time + cooldown;
        }

        /// <summary>
        /// Call this method to disallow <see cref="OnTargetBiomeExited"/> from being called for 'cooldown' seconds.
        /// </summary>
        public void SetExitCooldown(float cooldown)
        {
            timeCallExitAgain = Time.time + cooldown;
        }

        private void Start()
        {
            InvokeRepeating("CheckBiome", Random.value, updateInterval);
        }

        private void CheckBiome()
        {
            if(!CanCallEnterMethod && !CanCallExitMethod)
            {
                return;
            }
            bool nowInBiome = string.Equals(Player.main.GetBiomeString(), targetBiome, System.StringComparison.OrdinalIgnoreCase);
            if (!wasInBiome && nowInBiome)
            {
                if (CanCallEnterMethod)
                {
                    OnTargetBiomeEntered();
                }
            }
            if (wasInBiome && !nowInBiome)
            {
                if (CanCallExitMethod)
                {
                    OnTargetBiomeExited();
                }
            }
            wasInBiome = nowInBiome;
        }

        private bool CanCallEnterMethod
        {
            get
            {
                return Time.time > timeCallEnterAgain;
            }
        }

        private bool CanCallExitMethod
        {
            get
            {
                return Time.time > timeCallExitAgain;
            }
        }
    }
}
