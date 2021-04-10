using UnityEngine;
using ECCLibrary;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class TeleporterNetwork
    {
        public string classIdRoot;
        public TeleporterPrimaryPrefab primaryTeleporter;
        public TeleporterFramePrefab auxiliaryTeleporter;
        public Vector3 masterCoords, masterEuler, auxiliaryCoords, auxiliaryEuler;

        public TeleporterNetwork(string classIdRoot, string teleporterId, Vector3 masterCoords, Vector3 masterEuler, Vector3 masterPlayerPos, float masterPlayerAngle, Vector3 auxiliaryCoords, Vector3 auxiliaryEuler, Vector3 auxiliaryPlayerPos, float auxiliaryPlayerAngle)
        {
            this.classIdRoot = classIdRoot;
            this.masterCoords = masterCoords;
            this.masterEuler = masterEuler;
            this.auxiliaryCoords = auxiliaryCoords;
            this.auxiliaryEuler = auxiliaryEuler;
            primaryTeleporter = new TeleporterPrimaryPrefab(string.Format("{0}Primary", classIdRoot), teleporterId, auxiliaryPlayerPos, masterPlayerAngle);
            auxiliaryTeleporter = new TeleporterFramePrefab(string.Format("{0}Auxiliary", classIdRoot), teleporterId, masterPlayerPos, auxiliaryPlayerAngle);
        }
        
        public void Patch()
        {
            primaryTeleporter.Patch();
            auxiliaryTeleporter.Patch();
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(primaryTeleporter.ClassID, masterCoords, string.Format("{0}Primary", classIdRoot), 50f, masterEuler));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(auxiliaryTeleporter.ClassID, auxiliaryCoords, string.Format("{0}Auxiliary", classIdRoot), 50f, auxiliaryEuler));
        }
    }
}
