using ECCLibrary;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace RotA.Prefabs.AlienBase.Teleporter
{
    public class TeleporterNetwork
    {
        public string classIdRoot;
        public TeleporterPrimaryPrefab primaryTeleporter;
        public TeleporterFramePrefab auxiliaryTeleporter;
        public Vector3 masterCoords, auxiliaryCoords;
        public float masterAngle, auxiliaryAngle;

        public TeleporterNetwork(string classIdRoot, Vector3 masterCoords, float masterAngle, Vector3 auxiliaryCoords, float auxiliaryAngle, bool disablePlatformOnPrimary = false, bool disablePlatformOnAuxiliary = false, bool omegaTeleporter = false, TeleporterPrimaryPrefab.CustomItemSettings customItemSettings = default)
        {
            this.classIdRoot = classIdRoot;
            this.masterCoords = masterCoords;
            this.auxiliaryCoords = auxiliaryCoords;
            this.masterAngle = masterAngle;
            this.auxiliaryAngle = auxiliaryAngle;
            primaryTeleporter = new TeleporterPrimaryPrefab(string.Format("{0}Primary", classIdRoot), classIdRoot, GetPlayerSpawnPosition(auxiliaryCoords, auxiliaryAngle), auxiliaryAngle, disablePlatformOnPrimary, omegaTeleporter, customItemSettings);
            auxiliaryTeleporter = new TeleporterFramePrefab(string.Format("{0}Auxiliary", classIdRoot), classIdRoot, GetPlayerSpawnPosition(masterCoords, masterAngle), masterAngle, disablePlatformOnAuxiliary, omegaTeleporter);
        }

        public void Patch()
        {
            primaryTeleporter.Patch();
            auxiliaryTeleporter.Patch();
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new(primaryTeleporter.ClassID, masterCoords));
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new(auxiliaryTeleporter.ClassID, auxiliaryCoords));
        }

        Vector3 GetPlayerSpawnPosition(Vector3 coords, float angle)
        {
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            return coords + (direction * 1.5f) + new Vector3(0f, 2.15f, 0f);
        }
    }
}
