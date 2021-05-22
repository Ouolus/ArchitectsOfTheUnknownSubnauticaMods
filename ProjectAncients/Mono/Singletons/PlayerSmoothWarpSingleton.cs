using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace ProjectAncients.Mono
{
    public class PlayerSmoothWarpSingleton : MonoBehaviour
    {
        static PlayerSmoothWarpSingleton main;

        public static PlayerSmoothWarpSingleton Main
        {
            get
            {
                if(main == null)
                {
                    main = new GameObject("SmoothWarp").AddComponent<PlayerSmoothWarpSingleton>();
                }
                return main;
            }
        }

        public static bool PlayerInTransit { get; private set; }
        Vector3 startPos;
        Vector3 endPos;
        float startTime;
        float speed;
        static bool saveControllerEnabled;

        public static void StartSmoothWarp(Vector3 start, Vector3 end, float speed)
        {
            PlayerInTransit = true;
            Main.startPos = start;
            Main.endPos = end;
            Main.startTime = Time.time;
            Main.speed = speed;
            UpdatePlayerState(true);
        }

        void Update()
        {
            if (PlayerInTransit)
            {
                float percentDone = Mathf.Clamp01((Time.time - startTime) * speed);
                Player.main.transform.position = Vector3.Lerp(startPos, endPos, percentDone);
                if(percentDone >= 1f)
                {
                    EndSmoothWarp();
                }
            }
        }

        static void UpdatePlayerState(bool beingWarped)
        {
            PlayerInTransit = beingWarped;
            var player = Player.main;
            CharacterController controller = ((GroundMotor)Player.main.playerController.groundController).controller;
            if (beingWarped)
            {
                Player.main.playerAnimator.SetBool("precursor_elevator_decend", true);
                saveControllerEnabled = controller.enabled;
                controller.enabled = false;
            }
            else
            {
                Player.main.playerAnimator.SetBool("precursor_elevator_decend", false);
                float oceanLevel = 0f;
#if SN1_exp
                oceanLevel = Ocean.main.defaultOceanLevel;
#else
                oceanLevel = Ocean.main.GetOceanLevel();
#endif
                if (Player.main.transform.position.y > oceanLevel || Player.main.precursorOutOfWater)
                {
                    controller.enabled = true;
                }
                else
                {
                    controller.enabled = saveControllerEnabled;
                }
            }
        }
        public static void EndSmoothWarp()
        {
            UpdatePlayerState(false);
        }
    }
}
