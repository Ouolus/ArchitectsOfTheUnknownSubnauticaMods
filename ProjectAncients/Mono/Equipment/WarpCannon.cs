using UnityEngine;

namespace ProjectAncients.Mono.Equipment
{
    public class WarpCannon : PlayerTool
    {
        public Animator animator;
        public FMODAsset fireSound;
        public FMODAsset altFireSound;
        float timeCanUseAgain = 0f;
        public float maxDistance = 20f;
        public float minDistanceInBase = 1f;
        public float maxDistanceInBase = 15f;
        public float surveyRadius = 0.2f;
        public float maxChargeSeconds = 3f;
        bool handDown = false;
        float timeStartedCharging = 0f;

        public override bool OnRightHandDown()
        {
            if (Time.time > timeCanUseAgain)
            {
                timeStartedCharging = Time.time;
                handDown = true;
                return true;
            }
            return false;

        }

        public override bool OnRightHandUp()
        {
            float timeCharged = Time.time - timeStartedCharging;
            float chargeScale = Mathf.Clamp(timeCharged / maxChargeSeconds, 0.1f, timeCharged);
            if (TryUse(chargeScale))
            {
                timeCanUseAgain = Time.time + 2f;
                Utils.PlayFMODAsset(fireSound, transform.position, 20f);
                animator.SetTrigger("use");
                handDown = false;
                return true;
            }
            return false;
        }

        bool TryUse(float chargeScale)
        {
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
                    CharacterController controller = ((GroundMotor)Player.main.playerController.groundController).controller;
                    bool controllerWasEnabled = controller.enabled;
                    controller.enabled = false;
                    Player.main.transform.position = currentWarpPos;
                    controller.enabled = controllerWasEnabled;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, maxDistance * chargeScale, -1, QueryTriggerInteraction.Ignore))
                {
                    Player.main.transform.position = hit.point + (hit.normal);
                }
                else
                {
                    Player.main.transform.position = mainCam.position + (mainCam.forward * maxDistance * chargeScale);
                }
                return true;
            }
        }

        bool SurveyBaseWarpPosition(float distance, out Vector3 landingPosition)
        {
            Transform playerTransform = Player.main.transform;
            var mainCameraForward = MainCameraControl.main.transform.forward;
            landingPosition = playerTransform.position + (new Vector3(mainCameraForward.x, 0f, mainCameraForward.z) * distance);
            var hitColliders = Physics.OverlapSphere(landingPosition + new Vector3(0f, 1f, 0f), surveyRadius, -1, QueryTriggerInteraction.Ignore);
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
