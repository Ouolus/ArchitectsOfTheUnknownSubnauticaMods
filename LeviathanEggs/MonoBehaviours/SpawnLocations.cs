using UnityEngine;
namespace LeviathanEggs.MonoBehaviours
{
    public class SpawnLocations : MonoBehaviour
    {
        TechType techType;
        void Awake()
        {
            techType = gameObject.GetComponent<TechTag>().type;
        }
        void Start()
        {
            ErrorMessage.AddMessage($"{techType.AsString()} spawned in {gameObject.transform.position} coordinates.");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SpawnLocations[] gameObjects = FindObjectsOfType<SpawnLocations>();
                foreach (var obj in gameObjects)
                {
                    ErrorMessage.AddMessage($"{obj.techType.AsString()} location '{obj.gameObject.transform.position}'");
                }
            }
        }
    }
}
