using UnityEngine;
using ECCLibrary;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class TeleporterNetwork
    {
        public string classIdRoot;
        public TeleporterPrimaryPrefab primaryTeleporter;
        public TeleporterFramePrefab auxiliaryTeleporter;
        public Vector3 masterCoords, auxiliaryCoords;
        public float masterAngle, auxiliaryAngle;

        public TeleporterNetwork(string classIdRoot, string teleporterId, Vector3 masterCoords, float masterAngle, Vector3 auxiliaryCoords, float auxiliaryAngle)
        {
            this.classIdRoot = classIdRoot;
            this.masterCoords = masterCoords;
            this.auxiliaryCoords = auxiliaryCoords;
            this.masterAngle = masterAngle;
            this.auxiliaryAngle = auxiliaryAngle;
            primaryTeleporter = new TeleporterPrimaryPrefab(string.Format("{0}Primary", classIdRoot), teleporterId, GetPlayerSpawnPosition(auxiliaryCoords, auxiliaryAngle), auxiliaryAngle);
            auxiliaryTeleporter = new TeleporterFramePrefab(string.Format("{0}Auxiliary", classIdRoot), teleporterId, GetPlayerSpawnPosition(masterCoords, masterAngle), masterAngle);
        }
        
        public void Patch()
        {
            primaryTeleporter.Patch();
            auxiliaryTeleporter.Patch();
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(primaryTeleporter.ClassID, masterCoords, string.Format("{0}Primary", classIdRoot), 50f, Vector3.up * masterAngle));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(auxiliaryTeleporter.ClassID, auxiliaryCoords, string.Format("{0}Auxiliary", classIdRoot), 50f, Vector3.up * auxiliaryAngle));
        }

        Vector3 GetPlayerSpawnPosition(Vector3 coords, float angle)
        {
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            return coords + (direction * 3f);
        }
    }
}
