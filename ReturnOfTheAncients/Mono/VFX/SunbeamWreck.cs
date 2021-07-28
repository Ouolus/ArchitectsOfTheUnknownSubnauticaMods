using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotA.Mono.VFX
{
    public class SunbeamWreck : MonoBehaviour
    {
        Vector3 velocity;

        float aboveWaterGravity = -250f;

        float belowWaterGravity = 50;

        Vector3 initialForce = new Vector3(-140f, 120f, -320f);

        float waterSurfaceLevel = 50f;

        float waterBuoyancyLevel = -50f;

        bool hasTouchedWater = false;

        void Start()
        {
            velocity = initialForce;
        }

        void Update()
        {
            if (transform.position.y > waterSurfaceLevel)
            {
                velocity += new Vector3(0f, aboveWaterGravity, 0f) * Time.deltaTime;
            }
            else if (transform.position.y < waterBuoyancyLevel)
            {
                velocity += new Vector3(0f, belowWaterGravity, 0f) * Time.deltaTime;
                if (!hasTouchedWater)
                {
                    TouchWater();
                }
            }
            else
            {
                velocity = new Vector3(0f, velocity.y, 0f);
            }

            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        void TouchWater()
        {
            velocity = new Vector3(0f, 0f, 0f);
            hasTouchedWater = true;
        }
    }
}
