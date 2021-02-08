using UnityEngine;
namespace LeviathanEggs.MonoBehaviours
{
    public class ReadyToGrow : MonoBehaviour
    {
        StagedGrowing _stagedGrowing;
        void Awake() => _stagedGrowing = GetComponent<StagedGrowing>();
        void Start()
        {
            _stagedGrowing ??= gameObject.EnsureComponent<StagedGrowing>();
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
