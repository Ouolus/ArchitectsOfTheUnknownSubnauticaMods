using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class PrecursorIonCrystalAction : MonoBehaviour, IIonKnifeAction
    {
        public void EndAction(IonKnife ionKnife)
        {

        }

        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = new[] { 25f, 25f };
            ionKnife.AttackDistance = 1.2f;
            ionKnife.DamageType = new[] { DamageType.Normal, DamageType.Electrical };
            ionKnife.PlaySwitchSound("event:/loot/prec_crystal_loop");
            ionKnife.VfxEventType = VFXEventTypes.knife;
            ionKnife.ResourceBonus = 0;

            ionKnife.SetMaterialColors(new Color(.20f, .55f, .14f), new Color(.55f, .92f, .48f),
                new Color(.39f, 1f, .20f), new Color(.28f, .96f, .16f));
            ionKnife.SetLightAppearance(new Color(.20f, .55f, .14f), 6f);
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}