using ArchitectsLibrary.Handlers;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class ResearchBaseSpawner : CacheBaseSpawner
    {
        protected override string MainTerminalClassId => Mod.guardianTerminal.ClassID;

        protected override string TabletClassId => null;

        public override IEnumerator ConstructBase()
        {
            //Inside
            yield return StartCoroutine(base.ConstructBase());
            yield return StartCoroutine(SpawnPrefab(Mod.door_researchBase.ClassID, new Vector3(centerLocalX, floorLocalY, 24f)));
            yield return StartCoroutine(SpawnPrefab(Mod.researchBaseTerminal.ClassID, new Vector3(centerLocalX + 22f, floorLocalY, 0f), new Vector3(0f, 90f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 22f, floorLocalY, 3f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 22f, floorLocalY, -3f)));

            yield return StartCoroutine(SpawnPrefab(Mod.archElectricityTerminal.ClassID, new Vector3(centerLocalX - 22f, floorLocalY, 0f), new Vector3(0f, -90f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 22f, floorLocalY, 3f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 22f, floorLocalY, -3f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX, floorLocalY + 1f, -4f)));

            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX, floorLocalY, 6f), Mod.rifleRelic.ClassID, new Vector3(0f, 1.25f, 0f))); //11.77, -13.88, 10.00

            yield return StartCoroutine(SpawnPrefab(Mod.blackHole.ClassID, new Vector3(centerLocalX, floorLocalY + 4.3f, 0f)));

            yield return StartCoroutine(SpawnPrefab(CraftData.GetClassIdForTechType(AUHandler.PrecursorAlloyIngotTechType), new Vector3(centerLocalX, floorLocalY + 0.1f, 20f), new Vector3(0f, 37f, 0f)));

            yield return StartCoroutine(SpawnPrefab(Mod.redTabletHolder.ClassID, new Vector3(centerLocalX, floorLocalY, 17 + 1f), Vector3.up * 180f));
            yield return StartCoroutine(SpawnPrefab(supplies_redTablet, new Vector3(centerLocalX, floorLocalY + 2.5f, 17.5f - 0.825f + 1f), new Vector3(90f, 0f, 0f), Vector3.one * 1.5f));

            //Outside
            yield return StartCoroutine(SpawnPrefabGlobally(Mod.ghostSkeletonPose3.ClassID, new Vector3(-866.40f, -191.00f, -581.00f), new Vector3(355.00f, 320.72f, 0f), Vector3.one));
            yield return StartCoroutine(SpawnPrefabGlobally(Mod.guardianTailfinModel.ClassID, new Vector3(-847.15f, -193.53f, -593.49f), new Vector3(353.23f, 0f, 358.68f), Vector3.one));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-861.9288f, -193.3078f, -587.0382f), new Vector3(11.78806f, 359.9927f, 359.9293f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-869.1056f, -193.8968f, -582.0572f), new Vector3(6.095434f, 0.5774486f, 10.81343f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-861.2693f, -193.5382f, -576.5853f), new Vector3(7.906917f, 0.6367463f, 9.193762f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-847.1284f, -192.1513f, -580.4708f), new Vector3(353.577f, 0.1505132f, 357.318f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-835.0873f, -193.2803f, -592.3302f), new Vector3(358.1264f, 0.05534172f, 356.6166f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-828.8983f, -192.0322f, -579.1288f), new Vector3(338.317f, 357.4143f, 13.44182f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-819.386f, -190.897f, -582.4304f), new Vector3(333.1848f, 355.131f, 20.22565f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-820.9568f, -193.136f, -586.7079f), new Vector3(341.8121f, 358.7778f, 7.624599f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-868.8239f, -195.9651f, -572.7787f), new Vector3(17.86773f, 2.085975f, 13.21204f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-876.6642f, -195.4521f, -583.0635f), new Vector3(10.41053f, 2.076391f, 22.50126f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-887.3671f, -199.7482f, -576.8648f), new Vector3(18.63419f, 3.699184f, 22.27052f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-881.0107f, -199.8296f, -564.2679f), new Vector3(10.34504f, 3.377594f, 36.08095f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-873.6371f, -197.3778f, -564.8266f), new Vector3(358.6205f, 359.9558f, 3.672434f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-884.9455f, -204.3033f, -556.1657f), new Vector3(357.8743f, 359.1771f, 42.32252f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-899.9855f, -212.8859f, -555.6268f), new Vector3(355.8499f, 0.2901385f, 352.0053f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-901.4789f, -210.2087f, -567.8195f), new Vector3(30.44141f, 2.636955f, 9.670456f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-888.0885f, -203.6084f, -568.261f), new Vector3(18.10385f, 5.665552f, 34.5088f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-884.0432f, -201.625f, -565.7502f), new Vector3(14.76676f, 5.072172f, 37.74142f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-886.3569f, -200.9488f, -572.1064f), new Vector3(15.88102f, 3.901271f, 27.44349f), Vector3.one * 3f));
            yield return StartCoroutine(SpawnPrefabGlobally(natural_grass_green, new Vector3(-899.7899f, -205.5513f, -576.6423f), new Vector3(23.60654f, 4.788297f, 22.62831f), Vector3.one * 3f));
            
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-894.6136f, -201.1311f, -564.0109f), new Vector3(316.3175f, 346.2894f, 33.39309f), Vector3.one * 1f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-880.7109f, -192.7862f, -571.0246f), new Vector3(6.688025f, 1.020864f, 17.33839f), Vector3.one * 1f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-855.3006f, -190.3299f, -583.9695f), new Vector3(356.1317f, 0.1070935f, 356.8296f), Vector3.one * 1f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-864.6073f, -187.7225f, -583.7346f), new Vector3(270f, 0f, 0f), Vector3.one * 1f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-840.0343f, -191.2774f, -585.4749f), new Vector3(356.1317f, 0.1070936f, 356.8296f), Vector3.one * 1f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-829.215f, -188.6837f, -583.7346f), new Vector3(270f, 0f, 0f), Vector3.one * 1f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-868.9732f, -195.1799f, -576.115f), new Vector3(14.9749f, 2.321379f, 17.52697f), Vector3.one * 1f));
        }

        protected override float MainTerminalZOffset => 12f;
    }
}
