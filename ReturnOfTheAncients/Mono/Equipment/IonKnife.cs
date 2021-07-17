using UnityEngine;

namespace RotA.Mono.Equipment
{
    [RequireComponent(typeof(EnergyMixin))]
    public class IonKnife : PlayerTool
    {
        public override string animToolName => TechType.Knife.AsString(true);
    }
}
