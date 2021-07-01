using UnityEngine;

namespace LeviathanEggs.MonoBehaviours
{
    public class DroidDeploy : PlayerTool
    {
        DroidWelder _droidWelder;
        
        
        public override string animToolName => TechType.Constructor.AsString(true);

        void Start() => _droidWelder = GetComponent<DroidWelder>();
        
        public override void OnToolUseAnim(GUIHand guiHand)
        {
            if (guiHand.GetTool() == this)
            {
                pickupable.Drop(GetDropPosition());
                _droidWelder.SetSubRoot(Player.main.currentSub);
                pickupable.Unplace();
            }
        }

        public override bool OnRightHandDown()
        {
            return Player.main.GetCurrentSub();
        }

        Vector3 GetDropPosition()
        {
            var camTransform = MainCameraControl.main.transform;
            var pos = camTransform.forward * 1.07f + camTransform.position;
            if (Physics.Raycast(new Ray(camTransform.position, camTransform.forward), out var hitInfo, 1.07f))
            {
                pos = hitInfo.point + hitInfo.normal * 0.2f;
            }

            return pos;
        }
    }
}