using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class PosterFlicker  : MonoBehaviour
    {
        public Renderer renderer;
        float timeFlickerAgain = 0f;

        void Update()
        {
            if(Time.time > timeFlickerAgain)
            {
                timeFlickerAgain = Time.time + Random.Range(0.05f, 0.1f);
                renderer.material.SetColor("_Color", new Color(1f, 1.5f, 1f, Random.Range(0.4f, 0.55f)));
            }
        }
    }
}
