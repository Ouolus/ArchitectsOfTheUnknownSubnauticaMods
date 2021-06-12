using UnityEngine;
using ArchitectsLibrary.Utility;

namespace RotA.Mono.Modules
{
    public class DashOnKeyPress : MonoBehaviour
    {
        Exosuit exosuit;

        public void Start()
        {
            if (!uGUI.isLoading)
            {
                ErrorMessage.AddMessage(string.Format("Press {0} to initiate an Ion dash.", LanguageUtils.FormatKeyCode(Mod.config.PrawnSuitDashKey)));
            }
            exosuit = GetComponent<Exosuit>();
        }

        void Update()
        {
            if(Player.main.currentMountedVehicle == exosuit)
            {
                if (Input.GetKeyDown(Mod.config.PrawnSuitDashKey))
                {
                    float thrustPowerBefore = exosuit.thrustPower;
                    exosuit.thrustPower = 0f;
                    exosuit.useRigidbody.AddForce(GetThrustForce(thrustPowerBefore), ForceMode.VelocityChange);
                }
            }
        }

        Vector3 GetThrustForce(float thrustPower)
        {
            return MainCamera.camera.transform.forward * thrustPower * 30f;
        }
    }
}
