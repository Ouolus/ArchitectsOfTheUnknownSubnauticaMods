using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class OmegaCubeAction : IIonKnifeAction, IIonKnifeRightHand, IIonKnifeUsedTool
    {
        float hitForce = 65f;
        float useMassPercent = 0.6f; //percent of how much the knife's knockback is affected by the creature's mass. a higher value means it knocks bigger creatures such as leviathans less far.

        bool isCharging;
        float timeStartedCharging;
        float maxChargeSeconds = 1.7f;
        float chargeAmount;

        bool rightHandUp;

        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = new[] { 40f, 60f, 40f };
            ionKnife.AttackDistance = 1.8f;
            ionKnife.DamageType = new[] { DamageType.Heat, DamageType.Electrical, DamageType.Normal };
            ionKnife.PlaySwitchSound("event:/env/damage/cold_loop");
            ionKnife.VfxEventType = VFXEventTypes.diamondBlade;
            ionKnife.ResourceBonus = 0;
            ionKnife.UseTextLanguageKey = Mod.ionKnifeOmegaCube;

            ionKnife.SetMaterialColors(new Color(.3f, .3f, .3f), Color.white,
                new Color(1f, 2f, 1.25f), new Color(.5f, .5f, .5f));

            ionKnife.SetLightAppearance(new Color(1f, 1.5f, 1.25f) / 1.5f, 20f, 1.5f);
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            if (IonKnife.IsCreature(hitLiveMixin))
            {
                Utils.PlayFMODAsset(ionKnife.StrongHitFishSound, ionKnife.transform);
            }
            Rigidbody hitRb = hitLiveMixin.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                Vector3 playerDirection = ionKnife.usingPlayer.viewModelCamera.transform.forward;
                if (chargeAmount > 0f)
                {
                    hitRb.AddForce(playerDirection * (hitForce * useMassPercent) * chargeAmount, ForceMode.Impulse);
                    hitRb.AddForce(playerDirection * (hitForce * (1f - useMassPercent)) * chargeAmount, ForceMode.VelocityChange);
                }
            }
        }

        public void OnUpdate(IonKnife ionKnife)
        {

        }

        public bool OnRightHandDown(IonKnife ionKnife)
        {
            BeginCharge();
            return false;
        }

        public bool OnRightHandHeld(IonKnife ionKnife)
        {
            if (chargeAmount >= 1f)
            {
                EndCharge();
                return false;
            }
            
            Charge();
            return true;
        }

        public bool OnRightHandUp(IonKnife ionKnife)
        {
            EndCharge();
            return true;
        }

        public bool GetUsedToolThisFrame(IonKnife ionKnife)
        {
            var result = rightHandUp;
            if (rightHandUp) rightHandUp = false;
            return result;
        }

        void BeginCharge()
        {
            if (isCharging)
                return;

            isCharging = true;
            rightHandUp = false;
            timeStartedCharging = Time.time;
        }

        void Charge()
        {
            if (!isCharging)
            {
                return;
            }
            float timeCharged = Time.time - timeStartedCharging;
            float chargeScale = Mathf.Clamp01(timeCharged / maxChargeSeconds);
            chargeAmount = chargeScale;
        }

        void EndCharge()
        {
            rightHandUp = true;
            isCharging = false;
        }
    }
}