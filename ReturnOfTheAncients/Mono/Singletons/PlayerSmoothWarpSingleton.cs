using UnityEngine;

namespace RotA.Mono.Singletons
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
                saveControllerEnabled = controller.enabled;
                controller.enabled = false;
            }
            else
            {
                float oceanLevel = 0f;
#if SN1
                oceanLevel = Ocean.main.GetOceanLevel();
#else
                oceanLevel = Ocean.GetOceanLevel();
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
