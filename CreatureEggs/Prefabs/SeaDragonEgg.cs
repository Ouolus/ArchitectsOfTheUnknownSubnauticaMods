using UnityEngine;
using System.Collections.Generic;
using CreatureEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static CreatureEggs.Helpers.AssetsBundleHelper;
namespace CreatureEggs.Prefabs
{
    class SeaDragonEgg : LeviathanEgg
    {
        public SeaDragonEgg()
            :base("SeaDragonEgg", "Sea Dragon Egg", "Sea Dragons hatch from these.")
        {
            LateEnhancements += InitializeObject;
        }

        public override GameObject Model => LoadGameObject("SeaDragonEgg.prefab");
        public override TechType HatchingCreature => TechType.SeaDragon;
        public override Sprite ItemSprite => LoadSprite("SeaDragonEgg");
        public override string AssetsFolder => Main.AssetsFolder;

        public override List<SpawnLocation> CoordinatedSpawns => new()
        {
            new SpawnLocation(new Vector3(280.3136f, -1424.429f, 47.60991f), new Vector3(358.9967f, 359.7135f, 31.86873f)),
            new SpawnLocation(new Vector3(64.13924f, -1190.633f, 144.4159f), new Vector3(22.92112f, 355.978f, 340.3481f)),
            new SpawnLocation(new Vector3(-42.29882f, -1165.697f, 28.27014f), new Vector3(297.1068f, 0.0009694131f, -0.001585252f))
        };

        public void InitializeObject(GameObject prefab)
        {
            GameObject seaDragonEgg = Resources.Load<GameObject>("WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell");
            Renderer[] aRenderers = seaDragonEgg.GetComponentsInChildren<Renderer>();
            Material shell = null;
            Shader shader = Shader.Find("MarmosetUBER");
            foreach (var renderer in aRenderers)
            {
                if (renderer.name.StartsWith("Creatures_eggs_17"))
                {
                    shell = renderer.material;
                    break;
                }
                if (shell != null)
                    break;
            }
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.material.shader = shader;

                renderer.material = shell;
            }
            seaDragonEgg.SetActive(false);

            prefab.AddComponent<SpawnLocations>();
        }
    }
}
