using UnityEngine;

namespace RotA.Mono.Equipment
{
    public class WarpCannonAnimations : MonoBehaviour
    {
        public Animator animator;

        float spinSpeedTarget = 0.05f;
        float actualSpinSpeed = 0f;
        float batteryTarget = 1f;
        float actualBattery = 1f;
        bool isOverridingSpinSpeed = false;
        float overrideSpinSpeed;

        readonly int param_speed = Animator.StringToHash("speed");
        readonly int param_battery = Animator.StringToHash("battery");
        readonly int param_fire = Animator.StringToHash("fire");
        readonly int param_fireFast = Animator.StringToHash("fire_fast");

        const float warpCannonScreenShakeScale = 4f;

        public float SpinSpeed
        {
            set => spinSpeedTarget = Mathf.Clamp(value, 0.05f, 2f);
        }

        public float BatteryPercent
        {
            set => batteryTarget = value;
        }

        private float SpinSpeedToDisplay
        {
            get
            {
                if (batteryTarget <= 0.02f)
                {
                    return 0f;
                }
                else
                {
                    return actualSpinSpeed;
                }
            }
        }

        private float SpinSpeedTarget
        {
            get
            {
                if (isOverridingSpinSpeed)
                {
                    return overrideSpinSpeed;
                }
                else
                {
                    return spinSpeedTarget;
                }
            }
        }

        private void Update()
        {
            actualSpinSpeed = Mathf.MoveTowards(actualSpinSpeed, SpinSpeedTarget, Time.deltaTime);
            actualBattery = Mathf.MoveTowards(actualBattery, batteryTarget, Time.deltaTime * 2f);
            animator.SetFloat(param_speed, SpinSpeedToDisplay);
            animator.SetFloat(param_battery, actualBattery);
            if (SpinSpeedToDisplay > 0.1f)
            {
                MainCameraControl.main.ShakeCamera(SpinSpeedToDisplay * warpCannonScreenShakeScale, 0.3f, MainCameraControl.ShakeMode.BuildUp, 1f);
            }
        }

        public void PlayFireAnimation()
        {
            animator.SetTrigger(param_fire);
        }

        public void PlayFastFireAnimation()
        {
            animator.SetTrigger(param_fireFast);
        }

        public void SetSpinSpeedWithoutAcceleration(float newSpeed, bool setTargetSpeed)
        {
            actualSpinSpeed = newSpeed;
            if (setTargetSpeed)
            {
                spinSpeedTarget = newSpeed;
            }
        }

        public void SetOverrideSpinSpeed(float overrideSpinSpeed, float seconds)
        {
            this.overrideSpinSpeed = overrideSpinSpeed;
            isOverridingSpinSpeed = true;
            Invoke(nameof(StopOverrideSpinSpeed), seconds);
        }

        private void StopOverrideSpinSpeed()
        {
            isOverridingSpinSpeed = false;
        }
    }
}
