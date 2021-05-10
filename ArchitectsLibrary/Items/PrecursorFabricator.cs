using System.Collections;
using ArchitectsLibrary.Utility;
using FMODUnity;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace ArchitectsLibrary.Items
{
    class PrecursorFabricator : Buildable
    {
        GameObject _processedPrefab;
        internal CraftTree.Type TreeType { get; private set; }
        internal ModCraftTreeRoot Root { get; private set; }
        public PrecursorFabricator()
            : base("PrecursorFabricator", "Alien Fabricator", "Advanced alien fabricator. Basic Alterra fabricator refitted with advanced alien technology, capable of creating powerful artifacts.")
        {
            OnFinishedPatching += () =>
            {
                Root = CraftTreeHandler.CreateCustomCraftTreeAndType(ClassID, out CraftTree.Type craftTreeType);
                TreeType = craftTreeType;
            };
        }

        public override TechCategory CategoryForPDA { get; } = TechCategory.InteriorModule;
        public override TechGroup GroupForPDA { get; } = TechGroup.InteriorModules;

        public override bool UnlockedAtStart { get; } = false;
        
#if SN1
        public override GameObject GetGameObject()
        {
            if (_processedPrefab is not null)
            {
                var go = GameObject.Instantiate(_processedPrefab);
                go.SetActive(true);
                return go;
            }
            
            var prefab = Main.fabBundle.LoadAsset<GameObject>("PrecursorFabricator");

            var obj = new GameObject("PrecursorFabricator");
            
            var model = Object.Instantiate(prefab);
            model.name = "model";
            model.transform.parent = obj.transform;
            model.transform.localPosition = Vector3.zero;

            var sa = obj.EnsureComponent<SkyApplier>();
            sa.renderers = model.GetAllComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;
            
            MaterialUtils.ApplySNShaders(model);
            MaterialUtils.ApplyPrecursorMaterials(model, 6f);

            obj.EnsureComponent<TechTag>().type = TechType;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;

            Rigidbody body = obj.AddComponent<Rigidbody>();
            body.isKinematic = true;

            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;

            Constructable con = obj.AddComponent<Constructable>();
            con.techType = TechType;
            con.allowedInBase = true;
            con.allowedInSub = true;
            con.allowedOnWall = true;
            con.allowedOutside = false;
            con.allowedOnCeiling = false;
            con.allowedOnGround = false;
            con.model = model;
            con.rotationEnabled = false;
            obj.EnsureComponent<ConstructableBounds>();
            
            var fabObj = CraftData.GetPrefabForTechType(TechType.Fabricator);
            Fabricator originalFab = fabObj.GetComponent<Fabricator>();

            CrafterLogic logic = obj.EnsureComponent<CrafterLogic>();
            var fab = obj.EnsureComponent<MonoBehaviours.PrecursorFabricator>();
            fab.animator = obj.GetComponentInChildren<Animator>();
            fab.animator.runtimeAnimatorController = originalFab.animator.runtimeAnimatorController;
            fab.animator.avatar = originalFab.animator.avatar;
            fab.sparksPS = originalFab.sparksPS;
            fab.craftTree = TreeType;
            fab.spawnAnimationDelay = 3f;
            
            var workbenchObj = CraftData.GetPrefabForTechType(TechType.Workbench);
            var originalWorkbench = workbenchObj.GetComponent<Workbench>();

            Material beamMat = new Material(originalWorkbench.fxLaserBeam[0].GetComponent<MeshRenderer>().material);
            beamMat.color = Color.green;

            // beams setup
            fab.leftBeam = model.transform.Find("FabricatorMain/submarine_fabricator_01/fabricator/overhead/printer_left/fabricatorBeam").gameObject;
            fab.leftBeam.transform.localPosition = new(0f, 0f, 0.01f);
            fab.leftBeam.transform.localScale = new(0.5f, 0.5f, 0.7f);
            fab.leftBeam.GetComponent<MeshRenderer>().material = beamMat;
            fab.rightBeam = model.transform.Find("FabricatorMain/submarine_fabricator_01/fabricator/overhead/printer_right/fabricatorBeam 1").gameObject;
            fab.rightBeam.transform.localPosition = new(0f, 0f, -0.01f);
            fab.rightBeam.transform.localScale = new(0.5f, 0.5f, 0.7f);
            fab.rightBeam.GetComponent<MeshRenderer>().material = beamMat;

            fab.animator.SetBool(AnimatorHashID.open_fabricator, false);
            fab.crafterLogic = logic;
            fab.ghost = obj.EnsureComponent<CrafterGhostModel>();
            fab.ghost.itemSpawnPoint = model.transform.Find("FabricatorMain/submarine_fabricator_01/fabricator/printBed/spawnPoint");

            // sounds setup
            fab.openSound = ScriptableObject.CreateInstance<FMODAsset>();
            fab.openSound.path = "event:/player/key terminal_open";
            fab.closeSound = ScriptableObject.CreateInstance<FMODAsset>();
            fab.closeSound.path = "event:/player/key terminal_close";
            fab.soundOrigin = fab.transform;
            fab.fabricateSound = model.AddComponent<StudioEventEmitter>();
            fab.fabricateSound.Event = "event:/env/antechamber_scan_loop";

            //particles recolors
            var particleSystems = obj.GetComponentsInChildren<ParticleSystemRenderer>(true);
            foreach(ParticleSystemRenderer ps in particleSystems)
            {
                ps.material.SetColor("_Color", new Color(0f, 1.5f, 0f));
            }

            fab.fabLight = model.transform.Find("FabLight").GetComponent<Light>();

            fab.handOverText = $"Use {FriendlyName}";

            fab.powerRelay = PowerSource.FindRelay(fab.transform);
            
            _processedPrefab = GameObject.Instantiate(obj);
            _processedPrefab.SetActive(false);

            obj.SetActive(true);
            return obj;
        }
#endif
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (_processedPrefab is not null)
            {
                var go = GameObject.Instantiate(_processedPrefab);
                go.SetActive(true);
                gameObject.Set(go);
            }
            
            var prefab = Main.fabBundle.LoadAsset<GameObject>("PrecursorFabricator");

            var obj = new GameObject("PrecursorFabricator");
            
            var model = Object.Instantiate(prefab);
            model.name = "model";
            model.transform.parent = obj.transform;
            model.transform.localPosition = Vector3.zero;

            var sa = obj.EnsureComponent<SkyApplier>();
            sa.renderers = model.GetAllComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;
            
            MaterialUtils.ApplySNShaders(model);
            MaterialUtils.ApplyPrecursorMaterials(model, 6f);

            obj.EnsureComponent<TechTag>().type = TechType;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;

            Rigidbody body = obj.AddComponent<Rigidbody>();
            body.isKinematic = true;

            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;

            Constructable con = obj.AddComponent<Constructable>();
            con.techType = TechType;
            con.allowedInBase = true;
            con.allowedInSub = true;
            con.allowedOnWall = true;
            con.allowedOutside = false;
            con.allowedOnCeiling = false;
            con.allowedOnGround = false;
            con.model = model;
            con.rotationEnabled = false;
            obj.EnsureComponent<ConstructableBounds>();

            var task = CraftData.GetPrefabForTechTypeAsync(TechType.Fabricator);
            yield return task;
            var fabObj = task.GetResult();
            Fabricator originalFab = fabObj.GetComponent<Fabricator>();

            CrafterLogic logic = obj.EnsureComponent<CrafterLogic>();
            var fab = obj.EnsureComponent<MonoBehaviours.PrecursorFabricator>();
            fab.animator = obj.GetComponentInChildren<Animator>();
            fab.animator.runtimeAnimatorController = originalFab.animator.runtimeAnimatorController;
            fab.animator.avatar = originalFab.animator.avatar;
            fab.sparksPS = originalFab.sparksPS;
            fab.craftTree = TreeType;
            fab.spawnAnimationDelay = 3f;

            var task2 = CraftData.GetPrefabForTechTypeAsync(TechType.Workbench);
            yield return task2;
            var workbenchObj = task2.GetResult();
            var originalWorkbench = workbenchObj.GetComponent<Workbench>();

            Material beamMat = new Material(originalWorkbench.fxLaserBeam[0].GetComponent<MeshRenderer>().material);
            beamMat.color = Color.green;

            // beams setup
            fab.leftBeam = model.transform.Find("FabricatorMain/submarine_fabricator_01/fabricator/overhead/printer_left/fabricatorBeam").gameObject;
            fab.leftBeam.transform.localPosition = new(0f, 0f, 0.01f);
            fab.leftBeam.transform.localScale = new(0.5f, 0.5f, 0.7f);
            fab.leftBeam.GetComponent<MeshRenderer>().material = beamMat;
            fab.rightBeam = model.transform.Find("FabricatorMain/submarine_fabricator_01/fabricator/overhead/printer_right/fabricatorBeam 1").gameObject;
            fab.rightBeam.transform.localPosition = new(0f, 0f, -0.01f);
            fab.rightBeam.transform.localScale = new(0.5f, 0.5f, 0.7f);
            fab.rightBeam.GetComponent<MeshRenderer>().material = beamMat;
            
            fab.animator.SetBool(AnimatorHashID.open_fabricator, false);
            fab.crafterLogic = logic;
            fab.ghost = obj.EnsureComponent<CrafterGhostModel>();
            fab.ghost.itemSpawnPoint = model.transform.Find("FabricatorMain/submarine_fabricator_01/fabricator/printBed/spawnPoint");

            // sounds setup
            fab.openSound = ScriptableObject.CreateInstance<FMODAsset>();
            fab.openSound.path = "event:/player/key terminal_open";
            fab.closeSound = ScriptableObject.CreateInstance<FMODAsset>();
            fab.closeSound.path = "event:/player/key terminal_close";
            fab.soundOrigin = fab.transform;
            fab.fabricateSound = model.AddComponent<StudioEventEmitter>();
            fab.fabricateSound.Event = "event:/env/antechamber_scan_loop";

            //particles recolors
            var particleSystems = obj.GetComponentsInChildren<ParticleSystemRenderer>(true);
            foreach (ParticleSystemRenderer ps in particleSystems)
            {
                ps.material.SetColor("_Color", new Color(0f, 1.5f, 0f));
            }

            fab.fabLight = model.transform.Find("FabLight").GetComponent<Light>();

            fab.handOverText = $"Use {FriendlyName}";

            fab.powerRelay = PowerSource.FindRelay(fab.transform);
            
            _processedPrefab = GameObject.Instantiate(obj);
            _processedPrefab.SetActive(false);

            obj.SetActive(true);
            gameObject.Set(obj);
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.Titanium, 2)
                }
            };
        }
    }
}