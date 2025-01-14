﻿using Story;
using System;
using ArchitectsLibrary.API;
using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class InfectionTesterOpenDoor : HandTarget, IHandTarget, IStoryGoalListener
    {
        private void BroadcastDoorsOpen()
        {
            base.transform.parent.parent.BroadcastMessage("ToggleDoor", true, SendMessageOptions.RequireReceiver);
        }

        private bool DoorIsOpen()
        {
            var door = transform.parent.parent.GetComponentInChildren<PrecursorDoorway>();
            if (door)
            {
                return door.isOpen;
            }
            return false;
        }

        private bool DoorIsClosed()
        {
            return !DoorIsOpen();
        }

        private void Start()
        {
            StoryGoalManager main = StoryGoalManager.main;
            if (main)
            {
                playerCured = main.IsGoalComplete(onPlayerCuredGoal.key);
                if (!playerCured)
                {
                    main.AddListener(this);
                }
            }
        }

        public void NotifyGoalComplete(string key)
        {
            if (string.Equals(key, onPlayerCuredGoal.key, StringComparison.OrdinalIgnoreCase))
            {
                playerCured = true;
            }
        }

        private void SetOpen(bool isOpen)
        {
            if (isOpen)
            {
                openLoopSound.Play();
            }
            else
            {
                openLoopSound.Stop();
            }
            opened = isOpen;
            cinematic.animator.SetBool("open", isOpen);
        }

        public void OnHandHover(GUIHand hand)
        {
            if (DoorIsOpen() || usingPlayer != null || !opened)
            {
                return;
            }
            HandReticle.main.SetInteractText(LanguageSystem.Get("InfectionTesterTerminalHandTarget"));
            HandReticle.main.SetIcon(HandReticle.IconType.Interact, 1f);
        }

        public void OnHandClick(GUIHand hand)
        {
            if (usingPlayer == null && PlayerCinematicController.cinematicModeCount <= 0 && opened)
            {
                usingPlayer = hand.player;
                Inventory.main.ReturnHeld(true);
                if (playerCured)
                {
                    Utils.PlayFMODAsset(curedUseSound, transform, 20f);
                }
                else
                {
                    Utils.PlayFMODAsset(useSound, transform, 20f);
                }
                usingPlayer.playerAnimator.SetBool("using_tool_first", false);
                usingPlayer.playerAnimator.SetBool("cured", playerCured);
                cinematic.animator.SetBool("first_use", false);
                cinematic.animator.SetBool("cured", playerCured);
                cinematic.StartCinematicMode(hand.player);
                Invoke(playerCured ? "SetLightAccessGranted" : "SetLightAccessDenied", 5.75f);
            }
        }

        private void SetLightAccessGranted()
        {
            glowMaterial.SetColor(ShaderPropertyID._Color, Color.green);
            glowRing.Play();
        }

        private void SetLightAccessDenied()
        {
            glowMaterial.SetColor(ShaderPropertyID._Color, Color.red);
            glowRing.Play();
        }

        public void OnPlayerCinematicModeEnd()
        {
            if (usingPlayer)
            {
                if (!playerCured)
                {
                    Utils.PlayFMODAsset(accessDeniedSound, transform, 20f);
                }
                else
                {
                    Utils.PlayFMODAsset(accessGrantedSound, transform, 20f);
                    BroadcastDoorsOpen();
                }
                SetOpen(false);
                ignorePlayer = true;
                usingPlayer.playerAnimator.SetBool("using_tool_first", false);
            }
            usingPlayer = null;
        }

        public void OnTerminalAreaEnter()
        {
            if (DoorIsClosed())
            {
                if (ignorePlayer)
                {
                    ignorePlayer = false;
                    return;
                }
                SetOpen(true);
            }
        }

        public void OnTerminalAreaExit()
        {
            if (DoorIsClosed())
            {
                SetOpen(false);
            }
        }

        public FMODAsset accessGrantedSound;

        public FMODAsset accessDeniedSound;

        public PlayerCinematicController cinematic;

        public FMODAsset useSound;

        public FMODAsset curedUseSound;

        public FMOD_CustomLoopingEmitter openLoopSound;

        public StoryGoal onPlayerCuredGoal;

        public ParticleSystem glowRing;

        public Material glowMaterial;

        private Player usingPlayer;

        private bool opened;

        private bool ignorePlayer;

        private bool playerCured;
    }
}
