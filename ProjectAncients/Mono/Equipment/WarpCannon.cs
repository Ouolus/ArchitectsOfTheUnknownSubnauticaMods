using UnityEngine;

namespace ProjectAncients.Mono.Equipment
{
    public class WarpCannon : PlayerTool
    {
        public Animator animator;
        public FMODAsset fireSound;
        public FMODAsset altFireSound;
        float timeCanUseAgain = 0f;

        public override bool OnRightHandDown()
        {
            if (Time.time > timeCanUseAgain)
            {
                timeCanUseAgain = Time.time + 2f;
                Utils.PlayFMODAsset(fireSound, transform.position, 20f);
                ErrorMessage.AddMessage("Warp forward");
                animator.SetTrigger("use");
                return true;
            }
            return false;
        }

        public override bool OnAltDown()
        {
            if (Time.time > timeCanUseAgain)
            {
                timeCanUseAgain = Time.time + 3f;
                Utils.PlayFMODAsset(altFireSound, transform.position, 20f);
                ErrorMessage.AddMessage("Warp forward alt-fire!");
                animator.SetTrigger("use_alt");
                return true;
            }
            return false;
        }

        public override string animToolName => "stasisrifle";
    }
}
