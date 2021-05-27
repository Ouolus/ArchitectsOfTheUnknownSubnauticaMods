using UnityEngine;
using System.Collections;

namespace RotA.Mono
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
