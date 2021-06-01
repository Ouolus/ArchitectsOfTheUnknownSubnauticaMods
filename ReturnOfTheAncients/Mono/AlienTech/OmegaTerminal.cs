using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Story;

namespace RotA.Mono
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
            ErrorMessage.AddMessage("Project \"Omega\" formula incomplete. Further research required.");
        }

        public void FailInteractionAgain()
        {
            ErrorMessage.AddMessage("Project \"Omega\" formula incomplete. Further research required.");
        }

        public void SuccessInteraction()
        {
            ErrorMessage.AddMessage("Research Specimen Omega biometric data uploaded to terminal. Project Omega formula finalized. Beginning fabrication process... Fabrication complete.");

        }

        public void OnHandHover(GUIHand hand)
        {
            if (!fabricator.FabricatorEnabled())
            {
                HandReticle.main.SetInteractText(LanguageCache.GetButtonFormat(Mod.omegaTerminalInteract, GameInput.Button.LeftHand));
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
                return;
            }
            if (fabricator.CanGenerateCube())
            {
                HandReticle.main.SetInteractText(LanguageCache.GetButtonFormat(Mod.omegaTerminalRegenerateCube, GameInput.Button.LeftHand));
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }
            else
            {
                HandReticle.main.SetIcon(HandReticle.IconType.HandDeny, 1f);
            }
        }
    }
}
