using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections;
using UnityEngine;

namespace RotA.Prefabs
{
    /// <summary>
    /// A placeholder item for something cool!
    /// </summary>
    public class ComingSoonItem : Craftable
    {
        public ComingSoonItem(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData() { craftAmount = 1, Ingredients = new System.Collections.Generic.List<Ingredient>() { new Ingredient(TechType.Titanium, 1) } };
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = CraftData.GetPrefabForTechType(TechType.Titanium);
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            obj.GetComponent<TechTag>().type = TechType;
            obj.GetComponent<PrefabIdentifier>().ClassId = ClassID;

            return obj;
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.Titanium);
            yield return task;
            var prefab = task.GetResult();
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            obj.GetComponent<TechTag>().type = TechType;
            obj.GetComponent<PrefabIdentifier>().ClassId = ClassID;

            gameObject.Set(obj);
        }
    }
}
