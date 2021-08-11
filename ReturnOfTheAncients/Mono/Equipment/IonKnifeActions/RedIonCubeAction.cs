using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class RedIonCubeAction : IIonKnifeAction
    {
        float timeDmgPlayerAgain;

        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = new[] { 40f, 80f };
            ionKnife.AttackDistance = 1.8f;
            ionKnife.DamageType = new[] { DamageType.Heat, DamageType.Normal };
            ionKnife.PlaySwitchSound("event:/env/pink_artifact_loop");
            ionKnife.VfxEventType = VFXEventTypes.heatBlade;
            ionKnife.ResourceBonus = 3;

            ionKnife.SetMaterialColors(new Color(1f, 0.2f, 0f), new Color(1f, 0.2f, 0f), 
                Color.red, new Color(0.5f, 0.5f, 0.5f));
            ionKnife.SetLightAppearance(new Color(1f, 0.2f, 0f), 8f);
        }

        public void OnUpdate(IonKnife ionKnife)
        {
            if (Time.time > timeDmgPlayerAgain)
            {
                Player.main.liveMixin.TakeDamage(1f, ionKnife.transform.position, DamageType.Heat, ionKnife.gameObject);
                timeDmgPlayerAgain = Time.time + 1f;
            }
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            if (IonKnife.IsCreature(hitLiveMixin))
            {
                Utils.PlayFMODAsset(ionKnife.StrongHitFishSound, ionKnife.transform);
                Player.main.liveMixin.AddHealth(8f);
            }
        }
    }
}