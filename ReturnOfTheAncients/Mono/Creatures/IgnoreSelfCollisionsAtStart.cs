using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RotA.Mono.Creatures
{
    public class IgnoreSelfCollisionsAtStart : MonoBehaviour
    {
        public List<Collider> collidersToIgnoreEachOther;

        void Start()
        {
            for (int i = 0; i < collidersToIgnoreEachOther.Count; i++)
            {
                for (int j = 0; j < collidersToIgnoreEachOther.Count; j++)
                {
                    if (collidersToIgnoreEachOther[i] != collidersToIgnoreEachOther[j])
                    {
                        Physics.IgnoreCollision(collidersToIgnoreEachOther[i], collidersToIgnoreEachOther[j]);
                    }
                }
            }
            GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        }
    }
}
