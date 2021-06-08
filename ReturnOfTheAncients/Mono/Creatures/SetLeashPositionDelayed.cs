using System.Collections;
using UnityEngine;

namespace RotA.Mono.Creatures
{
    public class SetLeashPositionDelayed : MonoBehaviour
    {
        public Vector3 leashPosition;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<Creature>().leashPosition = leashPosition;
            Destroy(this);
        }
    }
}
