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
        LastTarget lastTarget;
        Quaternion defaultLocalRotation;
        Vector3 defaultUp;

        void Start()
        {
            lastTarget = GetComponentInParent<LastTarget>();
            defaultLocalRotation = transform.localRotation;
        }

        void Update()
        {
            if (lastTarget.target)
            {
                Vector3 direction = (lastTarget.target.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-transform.up, direction), Time.deltaTime * 300f);
            }
            else
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, defaultLocalRotation, Time.deltaTime * 150f);
            }
        }
    }
}
