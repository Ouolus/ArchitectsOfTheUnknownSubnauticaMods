using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using UWE;

namespace ArchitectsLibrary.Buildables
{
    /// <summary>
    /// Only public for RotA's OmegaCubePedestal.
    /// </summary>
    public class BuildableIonCubePedestal : GenericPrecursorDecoration
    {
        public BuildableIonCubePedestal(string classId, string displayName, string desc) : base(classId, displayName, desc)
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(1f, 0.4f, 1f)) };

        protected override string GetOriginalClassId => "ea65ef91-e875-4157-99f9-a8f4f6dc92f8";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            DeleteChildComponentIfExists<PrefabPlaceholder>(prefab);
            DeleteChildComponentIfExists<PrefabPlaceholdersGroup>(prefab);
#if SN1
            GameObject ionCubePlaceholderObj = prefab.gameObject.SearchChild("PrecursorIonCrystal(Placeholder)");
            PrefabDatabase.TryGetPrefab(IonCubeClassId, out GameObject ionCubePrefab);
            var decorationalIonCube = GameObject.Instantiate(ionCubePrefab);
            decorationalIonCube.transform.parent = ionCubePlaceholderObj.transform.parent;
            decorationalIonCube.transform.localPosition = ionCubePlaceholderObj.transform.localPosition;
            decorationalIonCube.transform.localRotation = ionCubePlaceholderObj.transform.localRotation;
            decorationalIonCube.transform.localScale = ionCubePlaceholderObj.transform.localScale;
            DeleteChildComponentIfExists<PrefabIdentifier>(decorationalIonCube);
            DeleteChildComponentIfExists<Pickupable>(decorationalIonCube);
            DeleteChildComponentIfExists<TechTag>(decorationalIonCube);
            DeleteChildComponentIfExists<LargeWorldEntity>(decorationalIonCube);
            DeleteChildComponentIfExists<SkyApplier>(decorationalIonCube);
            DeleteChildComponentIfExists<WorldForces>(decorationalIonCube);
            DeleteChildComponentIfExists<Rigidbody>(decorationalIonCube);
            DeleteChildComponentIfExists<ResourceTracker>(decorationalIonCube);

            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (2)").GetComponentInChildren<Collider>().enabled = false;
            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (3)").GetComponentInChildren<Collider>().enabled = false;
            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (4)").GetComponentInChildren<Collider>().enabled = false;
            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (5)").GetComponentInChildren<Collider>().enabled = false;
            prefab.transform.GetChild(0).transform.localPosition = new Vector3(0f, 0.3f, 0f);

            decorationalIonCube.SetActive(true);
#endif
        }

#if !SN1
        protected override IEnumerator EditPrefabAsyncOnly(GameObject prefab)
        {
            GameObject ionCubePlaceholderObj = prefab.gameObject.SearchChild("PrecursorIonCrystal(Placeholder)");
            var prefabRequest = PrefabDatabase.GetPrefabAsync(IonCubeClassId);
            yield return prefabRequest;
            prefabRequest.TryGetPrefab(out GameObject ionCubePrefab);
            var decorationalIonCube = GameObject.Instantiate(ionCubePrefab);
            decorationalIonCube.transform.parent = ionCubePlaceholderObj.transform.parent;
            decorationalIonCube.transform.localPosition = ionCubePlaceholderObj.transform.localPosition;
            decorationalIonCube.transform.localRotation = ionCubePlaceholderObj.transform.localRotation;
            decorationalIonCube.transform.localScale = ionCubePlaceholderObj.transform.localScale;
            DeleteChildComponentIfExists<PrefabIdentifier>(decorationalIonCube);
            DeleteChildComponentIfExists<Pickupable>(decorationalIonCube);
            DeleteChildComponentIfExists<TechTag>(decorationalIonCube);
            DeleteChildComponentIfExists<LargeWorldEntity>(decorationalIonCube);
            DeleteChildComponentIfExists<SkyApplier>(decorationalIonCube);
            DeleteChildComponentIfExists<WorldForces>(decorationalIonCube);
            DeleteChildComponentIfExists<Rigidbody>(decorationalIonCube);
            DeleteChildComponentIfExists<ResourceTracker>(decorationalIonCube);

            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (2)").GetComponent<Collider>().enabled = false;
            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (3)").GetComponent<Collider>().enabled = false;
            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (4)").GetComponent<Collider>().enabled = false;
            prefab.SearchChild("precursor_block_solid_04_04_04_v2 (5)").GetComponent<Collider>().enabled = false;
            prefab.transform.GetChild(0).transform.localPosition = new Vector3(0f, 0.1f, 0f);

            decorationalIonCube.SetActive(true);

        }
#endif


        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(AUHandler.AlienCompositeGlassTechType, 1), new Ingredient(TechType.PrecursorIonCrystal, 1) });
        }

        protected override string GetSpriteName => "IonCubePedestal";

        protected virtual string IonCubeClassId { get { return "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a"; } }
    }
}
