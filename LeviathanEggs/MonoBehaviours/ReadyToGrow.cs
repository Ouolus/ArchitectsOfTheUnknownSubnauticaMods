using UnityEngine;
namespace LeviathanEggs.MonoBehaviours
{
    public class ReadyToGrow : MonoBehaviour
    {
        void Start()
        {
            gameObject.EnsureComponent<StagedGrowing>();
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
