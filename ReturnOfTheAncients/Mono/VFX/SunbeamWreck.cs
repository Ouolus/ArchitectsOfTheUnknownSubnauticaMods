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

        float aboveWaterGravity = -150;

        float belowWaterGravity = 150;

        Vector3 initialForce = new Vector3(30f, 0f, -100);

        float waterSurfaceLevel = 50f;

        float waterBuoyancyLevel = -50f;

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
            }
            else
            {
                velocity = new Vector3(0f, velocity.y, 0f);
            }

            transform.Translate(velocity * Time.deltaTime);
        }
    }
}
