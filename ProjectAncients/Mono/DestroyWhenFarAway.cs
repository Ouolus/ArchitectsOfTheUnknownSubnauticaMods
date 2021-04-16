using UnityEngine;

namespace ProjectAncients.Mono
{
    public class DestroyWhenFarAway : MonoBehaviour
    {
        public float maxDistance = 80f;

        void Start()
        {
            InvokeRepeating("CheckDistance", Random.value, 2f);
        }

        void CheckDistance()
        {
            if(Vector3.Distance(MainCameraControl.main.transform.position, transform.position) < maxDistance)
            {

            }
        }
    }
}
