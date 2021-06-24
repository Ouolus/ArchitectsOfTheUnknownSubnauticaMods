using UnityEngine;

namespace RotA.Mono
{
    public class DestroyWhenFarAway : MonoBehaviour
    {
        public float maxDistance = 80f;
        public bool destroyWhileInAlienBase = true;

        void Start()
        {
            InvokeRepeating("CheckDistance", Random.value, 2f);
        }

        void CheckDistance()
        {
            if (Vector3.Distance(MainCameraControl.main.transform.position, transform.position) > maxDistance)
            {
                Destroy(gameObject);
                return;
            }
            if (destroyWhileInAlienBase)
            {
                string biomeString = Player.main.GetBiomeString();
                if (biomeString.StartsWith("precursor", System.StringComparison.OrdinalIgnoreCase) || biomeString.StartsWith("prison", System.StringComparison.OrdinalIgnoreCase))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
