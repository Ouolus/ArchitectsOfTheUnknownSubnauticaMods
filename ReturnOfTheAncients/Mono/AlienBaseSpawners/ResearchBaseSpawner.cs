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
            
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-886.3828f, -202.0417f, -569.2833f), new Vector3(17.53328f, 5.449856f, 34.30387f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-881.7793f, -201.4561f, -556.8263f), new Vector3(358.7383f, 359.5172f, 41.88034f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-899.9372f, -212.8077f, -554.4672f), new Vector3(355.8499f, 0.2901385f, 352.0053f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-895.7101f, -207.3998f, -569.8304f), new Vector3(22.46861f, 6.085391f, 29.9639f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-872.4451f, -197.3629f, -559.0836f), new Vector3(358.9864f, 359.946f, 6.092449f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-881.7217f, -197.2678f, -581.2143f), new Vector3(7.454872f, 0.8958662f, 13.68609f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-868.9159f, -196.9121f, -566.4913f), new Vector3(2.146841f, 0.1172011f, 6.24889f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-870.4085f, -193.5893f, -584.7972f), new Vector3(10.8496f, 0.7330649f, 7.707815f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-859.1404f, -193.3646f, -575.5086f), new Vector3(4.823329f, 0.3031821f, 7.189247f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-849.6245f, -192.2948f, -580.6376f), new Vector3(1.352244f, 0.1537388f, 12.97182f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-844.4916f, -193.1719f, -590.3148f), new Vector3(351.4302f, 0.1242448f, 358.3419f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-839.6558f, -192.5452f, -580.223f), new Vector3(358.8148f, 0.006393833f, 359.3818f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-827.1651f, -194.2734f, -590.5538f), new Vector3(352.5652f, 0.1378824f, 357.8781f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-820.8176f, -193.6505f, -590.1338f), new Vector3(350.9109f, 359.3884f, 7.68268f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-829.6092f, -191.7808f, -578.1346f), new Vector3(336.974f, 357.5009f, 12.22479f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-860.9232f, -193.1857f, -587.629f), new Vector3(11.78806f, 359.9927f, 359.9293f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-889.2529f, -200.5325f, -576.9055f), new Vector3(21.1193f, 4.758039f, 25.12779f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-820.6724f, -190.5986f, -581.2459f), new Vector3(333.3828f, 356.8796f, 13.13683f), Vector3.one * 2f));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_blueLight, new Vector3(-834.8636f, -193.3038f, -590.0609f), new Vector3(0.6323666f, 359.982f, 356.737f), Vector3.one * 2f));
        }

        protected override float MainTerminalZOffset => 12f;
    }
}
