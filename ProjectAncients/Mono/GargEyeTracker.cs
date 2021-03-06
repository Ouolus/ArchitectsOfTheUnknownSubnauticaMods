using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectAncients.Mono
{
    public class GargEyeTracker : MonoBehaviour
    {
        Quaternion defaultLocalRotation;

        void Start()
        {
            defaultLocalRotation = transform.localRotation;
        }

        void Update()
        {
            Transform target = GetTarget();
            if (target)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-transform.up, direction), Time.deltaTime * 300f);
            }
            else
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, defaultLocalRotation, Time.deltaTime * 150f);
            }
        }

        Transform GetTarget()
        {
            return MainCameraControl.main.transform;
        }
    }
}
