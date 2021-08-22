using Story;
using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class OmegaTerminal : HandTarget, IHandTarget
    {
        public OmegaFabricatorRoot fabricator;

        public void OnHandClick(GUIHand hand)
        {
            if (fabricator.FormulaUnlocked())
            {
                if (StoryGoalManager.main.OnGoalComplete(fabricator.interactSuccessGoal.key))
                {
                    SuccessInteraction();
                }
                else
                {
                    SuccessInteractionAgain();
                }
            }
            else
            {
                if (StoryGoalManager.main.OnGoalComplete(fabricator.interactFailGoal.key))
                {
                    FailInteraction();
                }
                else
                {
                    FailInteractionAgain();
                }
            }
        }

        public void FailInteraction()
        {
            CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("OmegaFabricatorFail"), "OmegaFabricatorFailVoiceline");
        }

        public void FailInteractionAgain()
        {
            ErrorMessage.AddMessage("Project \"Omega\" formula incomplete. Further research required.");
        }

        public void SuccessInteraction()
        {
            CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("OmegaFabricatorSuccess"), "OmegaFabricatorSuccessVoiceline");
            fabricator.AttemptToGenerateCube();
        }

        public void SuccessInteractionAgain()
        {
            if (fabricator.CanGenerateCube())
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("OmegaFabricatorFabricate"), "OmegaFabricatorSuccessAgainVoiceline");
                fabricator.AttemptToGenerateCube();
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!fabricator.FabricatorEnabled())
            {
                HandReticle.main.SetInteractText(Mod.omegaTerminalHoverText, Mod.omegaTerminalInteract, true, true, HandReticle.Hand.Left);
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
                return;
            }
            if (fabricator.CanGenerateCube())
            {
                HandReticle.main.SetInteractText(Mod.omegaTerminalHoverText, Mod.omegaTerminalRegenerateCube, true, true, HandReticle.Hand.Left);
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
                return;
            }
            HandReticle.main.SetInteractText(Mod.omegaTerminalHoverText, true, HandReticle.Hand.None);
            HandReticle.main.SetIcon(HandReticle.IconType.HandDeny, 1f);
        }
    }
}
