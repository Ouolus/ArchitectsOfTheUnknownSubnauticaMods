using UnityEngine;

namespace RotA.Mono
{
    public class SetTechTypeOnStart : MonoBehaviour
    {
        public TechType type;

        void Start()
        {
            gameObject.EnsureComponent<TechTag>().type = type;
        }
    }
}
