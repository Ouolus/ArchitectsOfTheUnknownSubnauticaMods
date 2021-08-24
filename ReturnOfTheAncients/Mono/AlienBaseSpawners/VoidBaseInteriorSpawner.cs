using ArchitectsLibrary.Handlers;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class VoidBaseInteriorSpawner : AlienBaseSpawner
    {
        private float firstFloorY = 0;
        private float firstCeilingY = 15;
        private float secondFloorY = 18;
        private float secondCeilingY = 33;

        public override IEnumerator ConstructBase()
        {
            //Entrance hallway
            yield return StartCoroutine(SpawnPrefabsArray(pedestal_empty2, 4f, new Vector3(1, 1, 10), Vector3.one, new Vector3(-3f + 0.5f, 2f, 40f)));
            yield return StartCoroutine(SpawnPrefabsArray(pedestal_empty2, 4f, new Vector3(1, 1, 10), Vector3.one, new Vector3(3f + 1.5f + 1.5f, 2f, 40f)));
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_interior_infectionTest.ClassID, new Vector3(0f, 0f, 13f), Vector3.zero));
            yield return StartCoroutine(SpawnPrefab(Mod.warpCannonTerminal.ClassID, new Vector3(4f, 0f, 17f), Vector3.up * 90f));
            yield return StartCoroutine(SpawnPrefab(pedestal_ionCrystal_square, new Vector3(-4f, 0f, 22f), Vector3.zero));

            yield return StartCoroutine(SpawnPrefab(pedestal_ionCrystal_square, new Vector3(4f, 0f, 22f), Vector3.zero));

            yield return StartCoroutine(SpawnPrefab(CraftData.GetClassIdForTechType(AUHandler.PrecursorAlloyIngotTechType), new Vector3(0.2f, 0.1f, 22.75f), Vector3.up * -24f));

            //Aquarium
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(-1.71f, 0.72f, -14.81f), new Vector3(6, 42, 4)));
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(-1.82f, 2.17f, -24.65f), new Vector3(-5, -9, 5)));
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(5.33f, 1.47f, -26.71f), new Vector3(23, 147, 5)));
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(-8.68f, 0.42f, -23.05f), new Vector3(-9, 5, 12)));
            yield return StartCoroutine(SpawnPrefabsArray(creature_rockgrub, 0.1f, Vector3.one * 3f, Vector3.one * 0.1f, new Vector3(0f, firstFloorY + 4f, -25)));

            yield return StartCoroutine(SpawnPrefab(Mod.aquariumGuppy.ClassID, new Vector3(0f, 7.81f, -22.34f)));
            yield return StartCoroutine(SpawnPrefab(Mod.aquariumGuppy.ClassID, new Vector3(0f, 6f, -22.34f)));
            yield return StartCoroutine(SpawnPrefab(Mod.aquariumGuppy.ClassID, new Vector3(0f, 4f, -22.34f)));
            yield return StartCoroutine(SpawnPrefab(Mod.aquariumSkeleton.ClassID, new Vector3(0f, -2.5f, 0f)));

            //Aquarium room
            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(25.62f, firstFloorY, 2.72f), new Vector3(0f, -135f, 0f), Vector3.one * 1.2f)); //Left doorway on bottom floor
            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(-25.62f, firstFloorY, 2.72f), new Vector3(0f, 135f, 0f), Vector3.one * 1.2f)); //Right doorway on bottom floor
            yield return StartCoroutine(SpawnPrefab(Mod.voidBaseTerminal.ClassID, new Vector3(0f, 0f, -8), new Vector3(0f, -180, 0f)));

            yield return StartCoroutine(SpawnPrefab(light_strip_animated_long, new Vector3(21f, 4.2f, -44.7f), new Vector3(0f, -45f, 19f)));
            yield return StartCoroutine(SpawnPrefab(light_strip_animated_long, new Vector3(-21f, 4.2f, -44.7f), new Vector3(0f, -135f, 19f)));
            yield return StartCoroutine(SpawnPrefab(light_strip_animated_long, new Vector3(30.5f, 13.1f, -24), new Vector3(0f, 90f, 159f)));
            yield return StartCoroutine(SpawnPrefab(light_strip_animated_long, new Vector3(-30.5f, 13.1f, -24), new Vector3(0f, 90f, 159f)));

            //Upper aquarium room
            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(25.62f, secondFloorY, 2.72f), new Vector3(0f, -135f, 0f), Vector3.one * 1.2f));//Second floor doorway on left
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_interior_left.ClassID, new Vector3(25.62f, secondFloorY, 2.72f), new Vector3(0f, 45f, 0f), Vector3.one * 1.2f));//Forcefield

            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(-25.62f, secondFloorY, 2.72f), new Vector3(0f, 135f, 0f), Vector3.one * 1.2f));//Second floor doorway on right
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_interior_right.ClassID, new Vector3(-25.62f, secondFloorY, 2.72f), new Vector3(0f, 315f, 0f), Vector3.one * 1.2f));//Forcefield

            yield return StartCoroutine(SpawnPrefab(light_big_ceiling_animated, new Vector3(0f, 27.5f, -22.5f), new Vector3(0f, 180f, 0f), Vector3.one * 3.10f));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(0f, 26.5f, -15.5f), new Vector3(180f, 0f, 0f), Vector3.one * 2.25f));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(0f, 26.5f, -30f), new Vector3(180f, 0f, 0f), Vector3.one * 2.25f));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(-7.25f, 26.5f, -22.5f), new Vector3(180f, 0f, 0f), Vector3.one * 2.25f));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(7.25f, 26.5f, -22.5f), new Vector3(180f, 0f, 0f), Vector3.one * 2.25f));

            //Egg pedestal
            Vector3 centerRelicPlatformPosition = new Vector3(0f, secondFloorY, 32f);
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform2, centerRelicPlatformPosition + new Vector3(0f, 0.5f, 0f), Vector3.zero, new Vector3(2.7f, 2.2f, 2.7f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(4f, 0f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(-4f, 0f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(0f, 0f, 4f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(0f, 0f, -4f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(4f, 0f, 4f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(4f, 0f, -4f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(-4f, 0f, 4f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, centerRelicPlatformPosition + new Vector3(-4f, 0f, -4f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty1, centerRelicPlatformPosition + new Vector3(-4f, 1f, -4f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty1, centerRelicPlatformPosition + new Vector3(-4f, 1f, 4f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty1, centerRelicPlatformPosition + new Vector3(4f, 1f, -4f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty1, centerRelicPlatformPosition + new Vector3(4f, 1f, 4f)));
            yield return StartCoroutine(SpawnPrefab(Mod.gargEgg.ClassID, new Vector3(0f, secondFloorY + 1.5f, 32f)));
            yield return StartCoroutine(SpawnPrefab(light_small_pointlight, new Vector3(4f, secondFloorY + 2.5f, 32f - 4f)));
            yield return StartCoroutine(SpawnPrefab(light_small_pointlight, new Vector3(4f, secondFloorY + 2.5f, 32f + 4f)));
            yield return StartCoroutine(SpawnPrefab(light_small_pointlight, new Vector3(-4f, secondFloorY + 2.5f, 32f - 4f)));
            yield return StartCoroutine(SpawnPrefab(light_small_pointlight, new Vector3(-4f, secondFloorY + 2.5f, 32f + 4f)));
            yield return StartCoroutine(SpawnPrefab(light_volumetric_2, new Vector3(0f, secondCeilingY, 32f), Vector3.right * 90f));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(-3f, secondFloorY + 1f, 32f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(3f, secondFloorY + 1f, 32f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(0f, secondFloorY + 8f, 32f), Vector3.zero, new Vector3(1.4f, 1f, 1.4f)));
            yield return StartCoroutine(SpawnPrefab(prop_claw, new Vector3(0f, secondFloorY + 3f, 32f), Vector3.zero, new Vector3(2f, 2f, 2f)));

            //Egg room
            yield return StartCoroutine(SpawnPrefab(prop_claw, new Vector3(20.64f, secondCeilingY - 2.5f - 4.72f - 4.72f, 31.32f), Vector3.up * 45f)); //columns must have a scale of 0.59f to fit with the claws nicely, which puts the columns at 4.72m tall
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(20.64f, secondCeilingY - 4.72f - 4.72f, 31.32f), Vector3.up * 45f, Vector3.one * 0.59f));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(20.64f, secondCeilingY - 4.72f, 31.32f), Vector3.up * 45f, Vector3.one * 0.59f));

            yield return StartCoroutine(SpawnPrefab(prop_claw, new Vector3(-30, secondCeilingY - 2.5f - 4.72f - 4.72f, 22f), Vector3.up * 135f));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-30, secondCeilingY - 4.72f - 4.72f, 22f), Vector3.up * 135f, Vector3.one * 0.59f));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-30, secondCeilingY - 4.72f, 22f), Vector3.up * 135f, Vector3.one * 0.59f));

            yield return StartCoroutine(SpawnPrefab(prop_tableRectangle, new Vector3(20.64f, secondFloorY, 31.32f), Vector3.zero, Vector3.one * 0.67f));
            yield return StartCoroutine(SpawnPrefab(prop_tableRectangle, new Vector3(20.64f + 1.4f, secondFloorY, 31.32f), Vector3.zero, Vector3.one * 0.67f));
            yield return StartCoroutine(SpawnPrefab(prop_tableRectangle, new Vector3(20.64f + 2.8f, secondFloorY, 31.32f), Vector3.zero, Vector3.one * 0.67f));
            yield return StartCoroutine(SpawnPrefab(supplies_cutefishegg, new Vector3(20.64f, secondFloorY + 1f, 31.32f), Vector3.up * -60f));

            Vector3 specimenCasePos = new Vector3(-18.17f, secondFloorY, 31f);
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(0f, 0f, -12f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(-3f, 0f, -12f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(3f, 0f, -12f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(0f, 0f, -6f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(-3f, 0f, -6f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(3f, 0f, -6f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos, Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(-3f, 0f, 0f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(3f, 0f, 0f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(0f, 0f, 6f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(-3f, 0f, 6f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(3f, 0f, 6f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(0f, 0f, 12f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(-3f, 0f, 12f), Vector3.up * 0f));
            yield return StartCoroutine(SpawnPrefab(prop_specimensCase, specimenCasePos + new Vector3(3f, 0f, 12f), Vector3.up * 0f));


            yield return StartCoroutine(SpawnPrefab(prop_microscope, new Vector3(-37f, secondFloorY, 32f), Vector3.up * -225f));
            yield return StartCoroutine(SpawnPrefab(prop_microscope, new Vector3(-39f, secondFloorY, 30f), Vector3.up * -225f));
            yield return StartCoroutine(SpawnPrefab(prop_genericMap, new Vector3(0f, 20f, 14.4f), Vector3.zero, Vector3.one * 3f));

            yield return StartCoroutine(SpawnPrefab(pedestal_ionCrystal_square, new Vector3(-37.39f, secondFloorY, -1.61f), Vector3.up * -313f));//Ion cube next to dissection tank

            yield return StartCoroutine(SpawnPrefab(prop_tableRectangle, new Vector3(-30.3f, secondFloorY, 22.65f), Vector3.zero, Vector3.one * 0.67f));
            yield return StartCoroutine(SpawnPrefab(prop_tableRectangle, new Vector3(-30.3f + 1.4f, secondFloorY, 22.65f), Vector3.zero, Vector3.one * 0.67f));
            yield return StartCoroutine(SpawnPrefab(prop_tableRectangle, new Vector3(-30.3f - 1.4f, secondFloorY, 22.65f), Vector3.zero, Vector3.one * 0.67f));
            yield return StartCoroutine(SpawnPrefab(supplies_ionCrystal, new Vector3(-30.3f, secondFloorY + 1f, 22.65f), Vector3.up * 5f));

            yield return StartCoroutine(SpawnPrefab(prop_dissectionTank, new Vector3(-45f, secondFloorY, 5f), Vector3.up * 135f, Vector3.one * 3f));

            yield return StartCoroutine(SpawnPrefab(light_small_spotlight_2, new Vector3(-45f, secondCeilingY, 5f), Vector3.right * 90f));
            yield return StartCoroutine(SpawnPrefab(light_verybig_novolumetrics, new Vector3(-45f, secondCeilingY, 5f), new Vector3(0f, 45f, 180f)));

            yield return StartCoroutine(SpawnPrefab(light_small_spotlight_2, new Vector3(45f, secondCeilingY, 5f), Vector3.right * 90f));
            yield return StartCoroutine(SpawnPrefab(light_verybig_novolumetrics, new Vector3(45f, secondCeilingY, 5f), new Vector3(0f, 45f, 180f)));
            yield return StartCoroutine(SpawnPrefab(Mod.eggRoomTerminal.ClassID, new Vector3(45, 18, 5), Vector3.up * 135));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(49, 18, 9), Vector3.up * -45f));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(41, 18, 1), Vector3.up * -45f));

            yield return StartCoroutine(GenerateCable(new Vector3(401, -382f, -1875f - 20f), Vector3.up, new Vector3(401, -382f + 15f, -1875f - 20f), Vector3.up, Vector3.zero, 0f));

            yield return StartCoroutine(GenerateCable(new Vector3(345f, -382f, -1875f - 20f), Vector3.up, new Vector3(345f, -382f + 15f, -1875f - 20f), Vector3.up, Vector3.zero, 0f));

            //Left lower room (with relics)
            yield return StartCoroutine(SpawnPrefab(Mod.cachePingsTerminal.ClassID, new Vector3(22, 0f, 28), new Vector3(0f, -45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(Mod.sonicDeterrentTerminal.ClassID, new Vector3(58.5f, 0f, 15), new Vector3(0f, 90, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(18, 0f, 24), new Vector3(0f, -45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(26, 0f, 32), new Vector3(0f, -45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_ionCrystal_square, new Vector3(40, 0f, 27), new Vector3(0f, -135f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, new Vector3(33.78f, -0.5f, 11.34f), new Vector3(0f, 45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, new Vector3(26.22f, -0.5f, 18.9f), new Vector3(0f, 45f, 0f)));
            yield return SpawnRelicInCase(new Vector3(33.78f, 0.5f, 11.34f), Mod.builderRelic.ClassID, new Vector3(0f, 1.35f, 0f), new Vector3(0f, -135f, 0f));
            yield return SpawnRelicInCase(new Vector3(26.22f, 0.5f, 18.9f), Mod.bladeRelic.ClassID, new Vector3(0f, 1.35f, 0f), new Vector3(0f, -135f, 0f));

            yield return StartCoroutine(SpawnPrefab(light_small_spotlight_2, new Vector3(22, firstCeilingY, 28), Vector3.right * 90f));
            yield return StartCoroutine(SpawnPrefab(light_verybig_novolumetrics, new Vector3(22, firstCeilingY, 28), new Vector3(0f, 45f, 180f)));

            yield return StartCoroutine(SpawnPrefab(Mod.gargPoster.ClassID, new Vector3(43.7f, 2.2f, 28.4f), new Vector3(0f, 225f, 0f), Vector3.one * 1.5f));

            //Right lower room (with omega fabricator)
            yield return StartCoroutine(SpawnPrefab(Mod.voidbaseSpamTerminal.ClassID, new Vector3(-22, 0f, 28), new Vector3(0f, 45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-18, 0f, 24), new Vector3(0f, 45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-26, 0f, 32), new Vector3(0f, 45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_ionCrystal_square, new Vector3(-40, 0f, 27), new Vector3(0f, 45, 0f)));

            yield return StartCoroutine(SpawnPrefab(light_small_spotlight_2, new Vector3(-22, firstCeilingY, 28), Vector3.right * 90f));
            yield return StartCoroutine(SpawnPrefab(light_verybig_novolumetrics, new Vector3(-22, firstCeilingY, 28), new Vector3(0f, 45f, 180f)));

            yield return StartCoroutine(SpawnPrefab(Mod.omegaCubeFabricator.ClassID, new Vector3(-36, 0, 14)));
        }
    }
}
