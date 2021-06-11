using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using UWE;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// Creates an Ion Cube Pedestal.
    /// <remarks>Only public for RotA's OmegaCubePedestal.</remarks>
    /// </summary>
    public class BuildableIonCubePedestal : GenericPrecursorDecoration
    {
        /// <summary>
        /// Creates a new Buildable Ion Cube Pedestal.
        /// </summary>
        /// <param name="classId">Class ID</param>
        /// <param name="displayName">Item's display name</param>
        /// <param name="description">Item's description</param>
        public BuildableIonCubePedestal(string classId, string displayName, string description) : base(classId, displayName, description)
        {
        }

        protected sealed override ConstructableSettings GetConstructableSettings => new (true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected sealed override OrientedBounds[] GetBounds => new[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(1f, 0.4f, 1f)) };
 
        protected sealed override string GetOriginalClassId => "ea65ef91-e875-4157-99f9-a8f4f6dc92f8";
 
        protected sealed override bool ExteriorOnly => false;
 
        protected sealed override void EditPrefab(GameObject prefab)
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
        protected sealed override IEnumerator EditPrefabAsyncOnly(GameObject prefab)
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


        /// <summary>
        /// Recipe for this buildable.
        /// </summary>
        /// <returns></returns>
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(AUHandler.AlienCompositeGlassTechType, 1), new Ingredient(TechType.PrecursorIonCrystal, 1) });
        }
        
        protected override string GetSpriteName => "IonCubePedestal";

        /// <summary>
        /// The Ion Cube's Class ID.
        /// </summary>
        protected virtual string IonCubeClassId => "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a";
    }
}
