using UnityEngine;

namespace ProjectAncients.Mono.Equipment
{
    public class WarpCannon : PlayerTool
    {
        public Animator animator;
        public FMODAsset fireSound;
        public FMODAsset altFireSound;
        public FMOD_StudioEventEmitter chargeLoop;
        float timeCanUseAgain = 0f;
        public float maxDistance = 40f;
        public float minDistanceInBase = 1f;
        public float maxDistanceInBase = 20f;
        public float surveyRadius = 0.2f;
        public float maxChargeSeconds = 1.5f;
        bool handDown = false;
        float timeStartedCharging = 0f;
        public float warpSpeed = 4;
        public GameObject warpInPrefab;
        public GameObject warpOutPrefab;
        public FireMode fireMode = FireMode.Warp;

        public override bool OnRightHandDown()
        {
            if (handDown)
            {
                return true;
            }
            if (Time.time > timeCanUseAgain)
            {
                if(fireMode == FireMode.Manipulate)
                {
                    return FireManipulateMode();
                }
                else
                {
                    return FireWarpMode();
                }
            }
            return false;
        }

        bool FireWarpMode()
        {
            timeStartedCharging = Time.time;
            handDown = true;
            chargeLoop.StartEvent();
            return true;
        }

        bool FireManipulateMode()
        {
            if (Player.main.IsInSub())
            {
                ErrorMessage.AddMessage("Cannot fire Warping Device while in Manipulate Mode while inside bases.");
                return false;
            }
            return true;
        }

        void Update()
        {
            animator.SetFloat("charge", GetChargePercent());
        }

        public override void OnHolster()
        {
            StopCharging();
        }

        public override string GetCustomUseText()
        {
            if(fireMode == FireMode.Warp)
            {
                return LanguageCache.GetButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyWarpKey, GameInput.Button.AltTool);
            }
            if (fireMode == FireMode.Manipulate)
            {
                return LanguageCache.GetButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyManipulateKey, GameInput.Button.AltTool);
            }
            return base.GetCustomUseText();
        }

        public override bool OnAltDown()
        {
            if(Time.time < timeCanUseAgain)
            {
                return false;
            }
            if(GetChargePercent() > 0f)
            {
                return false;
            }
            if (fireMode == FireMode.Warp)
            {
                fireMode = FireMode.Manipulate;
                return true;
            }
            if (fireMode == FireMode.Manipulate)
            {
                fireMode = FireMode.Warp;
                return true;
            }
            return false;
        }

        public override bool OnRightHandUp()
        {
            float chargeScale = GetChargePercent();
            if(Time.time > timeCanUseAgain && handDown)
            {
                if (TryUse(chargeScale, out Vector3 warpPos))
                {
                    if (chargeLoop.GetIsStartingOrPlaying())
                    {
                        chargeLoop.Stop(false);
                    }
                    float delay = 0.5f;
                    if (chargeScale > 0.5f)
                    {
                        delay = 1f;
                    }
                    timeCanUseAgain = Time.time + delay;
                    Utils.PlayFMODAsset(fireSound, warpPos, 20f);
                    animator.SetTrigger("use");
                    handDown = false;
                    return true;
                }
                else
                {
                    StopCharging();
                    return true;
                }
            }
            return false;
        }

        private void StopCharging()
        {
            if (chargeLoop.GetIsStartingOrPlaying())
            {
                chargeLoop.Stop(false);
            }
            timeCanUseAgain = Time.time + 0.5f;
            handDown = false;
        }

        float GetChargePercent()
        {
            if (!handDown)
            {
                return 0f;
            }
            if(fireMode == FireMode.Manipulate)
            {
                return 0f;
            }
            float timeCharged = Time.time - timeStartedCharging;
            float chargeScale = Mathf.Clamp(timeCharged / maxChargeSeconds, 0.2f, 1f);
            return chargeScale;
        }

        int GetOutsideLayerMask()
        {
            return LayerMask.GetMask("Default", "Useable", "NotUseable", "TerrainCollider");
        }

        bool TryUse(float chargeScale, out Vector3 targetPosition)
        {
            targetPosition = default;
            Transform mainCam = MainCamera.camera.transform;
            SubRoot currentSubRoot = Player.main.currentSub;
            if (currentSubRoot)
            {
                if (currentSubRoot.rb is not null)
                {
                    if (!currentSubRoot.rb.isKinematic)
                    {
                        return false;
                    }
                }
                bool shouldTeleport = false;
                float teleportDistance = float.MinValue;
                Vector3 currentWarpPos = Player.main.transform.position;
                for (int i = (int)minDistanceInBase; i <= (int)maxDistanceInBase; i++)
                {
                    float testDistance = i * chargeScale;
                    Vector3 warpDir = new Vector3(mainCam.forward.x, 0f, mainCam.forward.z);
                    if (Physics.Raycast(mainCam.position, warpDir, out RaycastHit hit, testDistance + 1f, -1, QueryTriggerInteraction.Ignore))
                    {
                        continue;
                    }
                    if (SurveyBaseWarpPosition(testDistance, out Vector3 warpPos))
                    {
                        if (testDistance > teleportDistance)
                        {
                            teleportDistance = testDistance;
                            currentWarpPos = warpPos;
                            shouldTeleport = true;
                        }
                    }
                }
                if (shouldTeleport)
                {
                    targetPosition = currentWarpPos;
                    MovePlayerWhileInBase(currentWarpPos);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, maxDistance * chargeScale, GetOutsideLayerMask(), QueryTriggerInteraction.Ignore))
                {
                    targetPosition = hit.point + (hit.normal);
                }
                else
                {
                    targetPosition = mainCam.position + (mainCam.forward * maxDistance * chargeScale);
                }
                MovePlayerWhileInWater(targetPosition);
                return true;
            }
        }

        void MovePlayerWhileInBase(Vector3 position)
        {
            PlayerSmoothWarpSingleton.StartSmoothWarp(Player.main.transform.position, position, warpSpeed);
        }

        void MovePlayerWhileInWater(Vector3 position)
        {
            Instantiate(warpInPrefab, Player.main.transform.position, MainCamera.camera.transform.rotation);
            Instantiate(warpOutPrefab, position, MainCamera.camera.transform.rotation);
            //Player.main.transform.position = position;
            PlayerSmoothWarpSingleton.StartSmoothWarp(Player.main.transform.position, position, warpSpeed * 2f);
        }

        bool SurveyBaseWarpPosition(float distance, out Vector3 landingPosition)
        {
            Transform playerTransform = Player.main.transform;
            var mainCameraForward = MainCameraControl.main.transform.forward;
            landingPosition = playerTransform.position + (new Vector3(mainCameraForward.x, 0f, mainCameraForward.z) * distance);
            var hitColliders = Physics.OverlapSphere(landingPosition + new Vector3(0f, 0f, 0f), surveyRadius, -1, QueryTriggerInteraction.Ignore);
            if (hitColliders == null)
            {
                return true;
            }
            if (hitColliders.Length == 0)
            {
                return true;
            }
            return false;
        }

        /*public override bool OnAltDown()
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
        }*/

        public override string animToolName => "stasisrifle";

        public enum FireMode
        {
            Warp,
            Manipulate
        }
    }
}
