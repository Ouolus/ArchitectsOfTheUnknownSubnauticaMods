using ArchitectsLibrary.API;
using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class OmegaCubeAction : IIonKnifeAction, IIonKnifeRightHand, IIonKnifeUsedTool
    {
        FMOD_StudioEventEmitter chargingSound;

        float hitForce = 120f;
        float useMassPercent = 0.6f; //percent of how much the knife's knockback is affected by the creature's mass. a higher value means it knocks bigger creatures such as leviathans less far.

        bool isCharging;
        float timeStartedCharging;
        float maxChargeSeconds = 3.7f;
        float chargeAmount;

        bool swing;

        float[] baseDamageAmounts = new[] { 40f, 60f, 40f };

        float[] cachedDamageAmounts = new float[3];

        public void Initialize(IonKnife ionKnife)
        {
            if (chargingSound == null)
            {
                chargingSound = ionKnife.gameObject.EnsureComponent<FMOD_StudioEventEmitter>();
                chargingSound.path = SNAudioEvents.Paths.AntechamberConstructIonCubeLoop;
            }
            
            ionKnife.Damage = baseDamageAmounts;
            ionKnife.AttackDistance = 3f;
            ionKnife.DamageType = new[] { DamageType.Heat, DamageType.Electrical, DamageType.Normal };
            ionKnife.PlaySwitchSound("event:/env/damage/cold_loop");
            ionKnife.VfxEventType = VFXEventTypes.diamondBlade;
            ionKnife.ResourceBonus = 0;
            ionKnife.UseTextLanguageKey = Mod.ionKnifeOmegaCube;

            ionKnife.SetMaterialColors(new Color(.3f, .3f, .3f), Color.white,
                new Color(1f, 2f, 1.25f), new Color(.5f, .5f, .5f));

            ionKnife.SetLightAppearance(new Color(1f, 1.5f, 1.25f) / 1.5f, 20f, 1.2f);
        }

        public void OnSwing(IonKnife ionKnife, LiveMixin hitLiveMixin, GameObject hitGameObject)
        {
            if (IonKnife.IsCreature(hitLiveMixin))
            {
                Utils.PlayFMODAsset(ionKnife.StrongHitFishSound, ionKnife.transform);
            }
            if (hitGameObject != null)
            {
                Rigidbody hitRb = hitGameObject.GetComponentInParent<Rigidbody>();
                if (hitRb != null)
                {
                    Vector3 playerDirection = ionKnife.usingPlayer.viewModelCamera.transform.forward;
                    if (chargeAmount > 0f)
                    {
                        hitRb.AddForce(playerDirection * (hitForce * useMassPercent) * chargeAmount, ForceMode.Impulse);
                        hitRb.AddForce(playerDirection * (hitForce * (1f - useMassPercent)) * chargeAmount, ForceMode.VelocityChange);
                    }
                }

                var tag = hitGameObject.GetComponent<TechTag>();
                if (tag)
                {
                    if (tag.type == TechType.GarryFish)
                    {
                        AchievementServices.ChangeAchievementCompletion("YeetGarryfish", 1);
                    }
                }
            }
            ionKnife.Damage = GetDamageAmountsArray(1f - chargeAmount);
            chargeAmount = 0f;
        }

        float[] GetDamageAmountsArray(float multiplier)
        {
            for (int i = 0; i < cachedDamageAmounts.Length; i++)
            {
                cachedDamageAmounts[i] = baseDamageAmounts[i] * multiplier;
            }
            return cachedDamageAmounts;
        }

        public void OnUpdate(IonKnife ionKnife)
        {
            if (chargeAmount >= 0.2f)
            {
                MainCameraControl.main.ShakeCamera(chargeAmount * 3f, 0.1f, MainCameraControl.ShakeMode.Linear, 1f);
            }
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
                return true;
            }
            if (isCharging)
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
            var result = swing;
            if (swing) swing = false;
            return result;
        }

        void BeginCharge()
        {
            if (isCharging)
                return;
            
            chargingSound.StartEvent();

            isCharging = true;
            swing = false;
            timeStartedCharging = Time.time;
        }

        void Charge()
        {
            if (!isCharging)
                return;
            
            var timeCharged = Time.time - timeStartedCharging;
            var chargeScale = Mathf.Clamp01(timeCharged / maxChargeSeconds);
            chargeAmount = chargeScale;
        }

        void EndCharge()
        {
            if (chargingSound.GetIsStartingOrPlaying())
                chargingSound.Stop(false);
            
            swing = true;
            isCharging = false;
        }
    }
}