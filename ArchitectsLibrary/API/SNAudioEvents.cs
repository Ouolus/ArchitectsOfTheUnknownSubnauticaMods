using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A class that helps with using base-game audio.
    /// </summary>
    public static class SNAudioEvents
    {
        /// <summary>
        /// Returns a new instance of an FMOD asset, with the path <paramref name="audioPath"/>. Meant for using sounds from the base game.
        /// </summary>
        /// <param name="audioPath"></param>
        /// <returns></returns>
        public static FMODAsset GetFmodAsset(string audioPath)
        {
            FMODAsset asset = ScriptableObject.CreateInstance<FMODAsset>();
            asset.path = audioPath;
            return asset;
        }

        /// <summary>
        /// A list of all audio event paths in the base game.
        /// </summary>
        public static class Paths
        {
            public const string Andi1 = "event:/creature/andi/1";
            public const string Andi2 = "event:/creature/andi/2";
            public const string AndiA = "event:/creature/andi/a";
            public const string AndiB = "event:/creature/andi/b";
            public const string AndiC = "event:/creature/andi/c";
            public const string AndiStart = "event:/creature/andi/start";
            public const string AndiStop = "event:/creature/andi/stop";
            public const string SkyrayFlap = "event:/creature/bird_small/flap";
            public const string SkyrayIdle = "event:/creature/bird_small/idle";
            public const string BiterBite = "event:/creature/biter/bite_1";
            public const string BiterIdle = "event:/creature/biter/idle_1";
            public const string BleederAttachLoop = "event:/creature/bleeder/attach_loop";
            public const string BleederDetach = "event:/creature/bleeder/detach";
            public const string BleederIdle = "event:/creature/bleeder/idle";
            public const string BleederPain = "event:/creature/bleeder/pain";
            public const string BleederPunch = "event:/creature/bleeder/punch";
            public const string BlighterBite = "event:/creature/blood_kelp_biter/bite";
            public const string BlighterIdle = "event:/creature/blood_kelp_biter/idle";
            public const string BloodCrawlerAttack = "event:/creature/bloodcrawler/attack";
            public const string BloodCrawlerIdle = "event:/creature/bloodcrawler/idle";
            public const string BonesharkBite = "event:/creature/boneshark/bite";
            public const string BonesharkIdle = "event:/creature/boneshark/idle";
            public const string BonesharkRoar = "event:/creature/boneshark/roar";
            public const string CrabsnakeAlert = "event:/creature/crabsnake/alert";
            public const string CrabsnakeAttack = "event:/creature/crabsnake/attack";
            public const string CrabsnakeCinematicAttack = "event:/creature/crabsnake/attack_cine";
            public const string CrabsnakeIdleRoar = "event:/creature/crabsnake/idle_swim";
            public const string CrabsquidAttack = "event:/creature/crabsquid/attack";
            public const string CrabsquidCharge = "event:/creature/crabsquid/charge";
            public const string CrabsquidEMPAttack = "event:/creature/crabsquid/emp";
            public const string CrabsquidIdle = "event:/creature/crabsquid/idle";
            public const string CrashFishAngryLoop = "event:/creature/crash/angry_loop";
            public const string CrashFishAttack = "event:/creature/crash/attack";
            public const string CrashFishDie = "event:/creature/crash/die";
            public const string CrashFishIdle = "event:/creature/crash/idle";
            public const string CrashFishInflate = "event:/creature/crash/inflate";
            public const string CrashFishOpenPod = "event:/creature/crash/open_pod";
            public const string CaveCrawlerAttack = "event:/creature/crawler/attack";
            public const string CaveCrawlerIdle = "event:/creature/crawler/idle";
            public const string CaveCrawlerJump = "event:/creature/crawler/jump";
            public const string CutefishFollow = "event:/creature/cute_fish/command_follow";
            public const string CutefishStay = "event:/creature/cute_fish/command_stay";
            public const string CutefishDeath = "event:/creature/cute_fish/death";
            public const string CutefishFlinch = "event:/creature/cute_fish/flinch";
            public const string CutefishIdle = "event:/creature/cute_fish/idle";
            public const string CutefishInteractFeed = "event:/creature/cute_fish/interact_feed";
            public const string CutefishInteractHighFive = "event:/creature/cute_fish/interact_hifive";
            public const string CutefishInteractMagic = "event:/creature/cute_fish/interact_magic";
            public const string CutefishInteractPetting = "event:/creature/cute_fish/interact_petting";
            public const string CutefishSayBye = "event:/creature/cute_fish/say_bye";
            public const string CutefishTease1 = "event:/creature/cute_fish/tease_1";
            public const string CutefishTease2 = "event:/creature/cute_fish/tease_2";
            public const string EnzymeCureAKAGarryfishIdle = "event:/creature/enzyme_cure";
            public const string EnzymeTreatmentAKAGarryfishIdle = "event:/creature/enzyme_treatment";
            public const string GarryfishIdle = "event:/creature/garry/idle";
            public const string GasopodDeath = "event:/creature/gasopod/death";
            public const string GasopodIdle = "event:/creature/gasopod/idle";
            public const string GasopodPain = "event:/creature/gasopod/pain";
            public const string GasopodPodBurst = "event:/creature/gasopod/pod_burst";
            public const string GasopodPodRelease = "event:/creature/gasopod/pod_release";
            public const string GhostLeviathanBite = "event:/creature/ghostleviathan/bite";
            public const string GhostLeviathanCharage = "event:/creature/ghostleviathan/charge";
            public const string GhostLeviathanIdle = "event:/creature/ghostleviathan/idle";
            public const string GhostrayIdle = "event:/creature/ghostray/idle";
            public const string GlowraySing = "event:/creature/glowray/sing";
            public const string JumperIdle = "event:/creature/jumper/idle";
            public const string LavaLarvaAttach = "event:/creature/larva/attach";
            public const string LavaLarvaIdle = "event:/creature/larva/idle";
            public const string LavaLarvaPain = "event:/creature/larva/pain";
            public const string LavaLizardBite = "event:/creature/lavalizard/bite";
            public const string LavaLizardCharge = "event:/creature/lavalizard/charge";
            public const string LavaLizardLavaDive = "event:/creature/lavalizard/lava_dive";
            public const string LavaLizardRoar = "event:/creature/lavalizard/roar";
            public const string LavaLizardSpit = "event:/creature/lavalizard/spit";
            public const string LavaLizardSpitHit = "event:/creature/lavalizard/spit_hit";
            public const string LavaLizardWound = "event:/creature/lavalizard/wound";
            public const string SeaEmperorBabyCinematic = "event:/creature/magistrate/cin_baby_interact";
            public const string SeaEmperorAppearCinematic = "event:/creature/magistrate/cin_emp_appear";
            public const string SeaEmperorClearSandCinematic = "event:/creature/magistrate/cin_emp_sand_clear";
            public const string SeaEmperorLandingCinematicAkaSeaTreaderStomp = "event:/creature/magistrate/cin_landing";
            public const string SeaEmperorAreYouHereToPlay = "event:/creature/magistrate/encounter_1";
            public const string SeaEmperorMyYoungNeedToHatch = "event:/creature/magistrate/encounter_2";
            public const string SeaEmperorWithThePassageYouHaveOpened = "event:/creature/magistrate/encounter_3";
            public const string SeaEmperorMyYoungAreSwimmingForTheShallows = "event:/creature/magistrate/encounter_4";
            public const string SeaEmperorBabyIdle = "event:/creature/magistrate/enzyme_ball_drop";
            public const string SeaEmperorEnzymeSpray = "event:/creature/magistrate/enzyme_spray";
            public const string SeaEmperorBreathingLoop = "event:/creature/magistrate/face_loop";
            public const string SeaEmperorIdleUnused = "event:/creature/magistrate/idle";
            public const string SeaEmperorRoarUnused = "event:/creature/magistrate/roar";
            public const string SeaEmperorSayHelloUnused = "event:/creature/magistrate/say_hello";
            public const string SeaEmperorSpeakUnused = "event:/creature/magistrate/speak";
            public const string SeaEmperorSwimLoop = "event:/creature/magistrate/swimloop";
            public const string SeaEmperorTelepathy1 = "event:/creature/magistrate/telepathy_1";
            public const string SeaEmperorTelepathy2 = "event:/creature/magistrate/telepathy_2";
            public const string SeaEmperorTelepathy3 = "event:/creature/magistrate/telepathy_3";
            public const string MesmerAttack = "event:/creature/mesmer/attack";
            public const string MesmerIdle = "event:/creature/mesmer/idle";
            public const string MesmerizeEnd = "event:/creature/mesmer/mesmerize_end";
            public const string MesmerizeLoop = "event:/creature/mesmer/mesmerize_loop";
            public const string MesmerizeStart = "event:/creature/mesmer/mesmerize_start";
            public const string OculusIdle = "event:/creature/oculus/idle";
            public const string PeeperIdle = "event:/creature/peeper/chirp";
            public const string RaySing = "event:/creature/ray/sing";
            public const string ReaperAttackPlayer = "event:/creature/reaper/attack_player";
            public const string ReaperAttackPlayerBite = "event:/creature/reaper/attack_player_bite";
            public const string ReaperAttackPlayerClaw = "event:/creature/reaper/attack_player_claw";
            public const string ReaperAttackSeamoth = "event:/creature/reaper/attack_seamoth";
            public const string ReaperIdleRoar = "event:/creature/reaper/idle";
            public const string ReefbackIdle = "event:/creature/reefback/idle";
            public const string AlienRobotAttack = "event:/creature/robocrawler/attack";
            public const string AlienRobotIdle = "event:/creature/robocrawler/idle";
            public const string AlienRobotJumpUnused = "event:/creature/robocrawler/jump";
            public const string SandsharkAlert = "event:/creature/sandshark/alert";
            public const string SandsharkAttack = "event:/creature/sandshark/attack";
            public const string SandsharkBite = "event:/creature/sandshark/bite";
            public const string SandsharkBurrow = "event:/creature/sandshark/burrow";
            public const string SandsharkDeath = "event:/creature/sandshark/death";
            public const string SandsharkIdle = "event:/creature/sandshark/idle";
            public const string SandsharkMoveSand = "event:/creature/sandshark/move_sand";
            public const string SandsharkPain = "event:/creature/sandshark/pain";
            public const string SeaDragonAttackPrawnCinematic = "event:/creature/seadragon/attack_mech_cin";
            public const string SeaDragonAttackPlayerBite = "event:/creature/seadragon/attack_player_bite";
            public const string SeaDragonAttackPlayerClaw = "event:/creature/seadragon/attack_player_claw";
            public const string SeaDragonFireAttackLoop = "event:/creature/seadragon/fireattack_loop";
            public const string SeaDragonIdleRoar = "event:/creature/seadragon/idle";
            public const string AmpeelBite = "event:/creature/shocker/bite";
            public const string AmpeelIdleElectricityLoop = "event:/creature/shocker/idle_loop";
            public const string AmpeelShock = "event:/creature/shocker/shock";
            public const string TigerPlantHitPlayer = "event:/creature/spike_plant/player_hit";
            public const string TigerPlantShoot = "event:/creature/spike_plant/shoot";
            public const string RiverProwlerBite = "event:/creature/spine_eel/bite";
            public const string RiverProwlerIdle = "event:/creature/spine_eel/idle";
            public const string RiverProwlerRoar = "event:/creature/spine_eel/roar";
            public const string SeaTreaderAttackDown = "event:/creature/tred/attack_down";
            public const string SeaTreaderAttackDownNoStomp = "event:/creature/tred/attack_down_nostomp";
            public const string SeaTreaderAttackSwing = "event:/creature/tred/attack_swing";
            public const string SeaTreaderIdle = "event:/creature/tred/idle";
            public const string SeaTreaderPoop1 = "event:/creature/tred/shit_fall";
            public const string SeaTreaderPoop2 = "event:/creature/tred/shit_from_ass";
            public const string SeaTreaderPoop3 = "event:/creature/tred/shit_sequence";
            public const string SeaTreaderPoop4 = "event:/creature/tred/shit_sequence_2";
            public const string SeaTreaderStep = "event:/creature/tred/step";
            public const string SeaTreaderStomp = "event:/creature/tred/stomp";
            public const string StalkerBite = "event:/creature/trident/bite";
            public const string StalkerCharge = "event:/creature/trident/charge";
            public const string StalkerDeath = "event:/creature/trident/death";
            public const string StalkerRoar = "event:/creature/trident/roar";
            public const string StalkerToothBreak = "event:/creature/trident/tooth_break";
            public const string StalkerWound = "event:/creature/trident/wound";
            public const string WarperIdle = "event:/creature/warper/idle";
            public const string WarperPortalClose = "event:/creature/warper/portal_close";
            public const string WarperPortalOpen = "event:/creature/warper/portal_open";
            public const string WarperSwipe = "event:/creature/warper/swipe";
            public const string AntechamberLLightsOn = "event:/env/antechamber_lights_on";
            public const string AntechamberPillarDown = "event:/env/antechamber_pillar_down";
            public const string AntechamberPillarUp = "event:/env/antechamber_pillar_up";
            public const string AntechamberConstructIonCubeEnd = "event:/env/antechamber_scan_end";
            public const string AntechamberConstructIonCubeLoop = "event:/env/antechamber_scan_loop";
            public const string AntechamberConstructIonCubeStart = "event:/env/antechamber_scan_start";
            public const string AuroraSparksLoop = "event:/env/aurora_sparks_loop";
            /*
event:/env/background/arctic_background
event:/env/background/blood_kelp_background
event:/env/background/crash_zone
event:/env/background/debris_fall
event:/env/background/debris_fall_fire
event:/env/background/dunes_background
event:/env/background/fire
event:/env/background/floating_islands_background
event:/env/background/floating_islands_background
event:/env/background/generator_room
event:/env/background/grandreef_background
event:/env/background/grasy_background
event:/env/background/jellyshroom_caves
event:/env/background/kelp_background
event:/env/background/koosh_background
event:/env/background/lava_background
event:/env/background/lava_river_loop
event:/env/background/lost_river_background
event:/env/background/lostriverbase_background
event:/env/background/mountain_cave
event:/env/background/mountain_underwater
event:/env/background/mushroom_background
event:/env/background/prec_aquarium
event:/env/background/prec_base_background
event:/env/background/prec_base_background
event:/env/background/prec_cave_loop
event:/env/background/prec_generator_room_ambience
event:/env/background/reef_background
event:/env/background/shake
event:/env/background/ship_ambience
event:/env/background/ship_explode
event:/env/background/ship_explode_2
event:/env/background/shockwave
event:/env/background/small_explode
event:/env/background/sparse_reef_background
event:/env/background/surface_generic_background
event:/env/background/tredpath
event:/env/background/twisty_bridges_background
event:/env/background/underislands_background
event:/env/background/vent_loop
event:/env/background/wreak ambience
event:/env/background/wreak_ambience_big
event:/env/break_coral
event:/env/bubble_pop
event:/env/bubble_pop_player
event:/env/close_underwater_door
event:/env/creature_teleport
event:/env/damage/acid_loop
event:/env/damage/cold_loop
event:/env/damage/heat_loop
event:/env/damage/pressure_loop
event:/env/damage/shock
event:/env/geyser_erupt
event:/env/green_artifact_loop
event:/env/gun_return
event:/env/gun_turn
event:/env/incubator_egg_hatch
event:/env/incubator_powerup
event:/env/input_number
event:/env/keypad_correct
event:/env/keypad_door_open
event:/env/keypad_wrong
event:/env/koosh_ball_death
event:/env/koosh_ball_spores
event:/env/music/antechamber_music
event:/env/music/arctic_background_music
event:/env/music/aurora_reveal
event:/env/music/blood_kelp_background_music
event:/env/music/crash_zone_music
event:/env/music/death_music
event:/env/music/dunes_background_music
event:/env/music/exo_room_music
event:/env/music/firefighting_music
event:/env/music/floating_islands_background_music
event:/env/music/generator_room_music
event:/env/music/grandreef_background_music
event:/env/music/grasy_background (2)_music
event:/env/music/grasy_background_music
event:/env/music/hatch_music
event:/env/music/jellyshroom_caves_music
event:/env/music/kelp_background_music
event:/env/music/koosh_background_music
event:/env/music/lava_background_music
event:/env/music/lost_river_background_music
event:/env/music/lost_river_ghost_tree
event:/env/music/lostriver_tree
event:/env/music/lostriverbase_background_music
event:/env/music/mountain_underwater_music
event:/env/music/mushroom_background_music
event:/env/music/prec_aquarium_music
event:/env/music/prec_base_background_music
event:/env/music/prec_cave_music
event:/env/music/reef_background_music
event:/env/music/ship_ambience_music
event:/env/music/sparse_reef_background_music
event:/env/music/tredpath_music
event:/env/music/twisty_bridges_background_music
event:/env/music/underislands_background_music
event:/env/music/wreak ambience_music
event:/env/music/wreak_ambience_big_music
event:/env/music/wreak_ambience_big_music
event:/env/music/zen_music
event:/env/open_underwater_door
event:/env/pink_artifact_loop
event:/env/pipe_room_pipes_loop
event:/env/plant_cut
event:/env/power_teleporter
event:/env/prec_artifact_loop
event:/env/prec_foce_field_deactivate
event:/env/prec_forcefield_loop
event:/env/prec_light_on
event:/env/prec_light_on_2
event:/env/prec_pillar_down
event:/env/prec_pillar_up
event:/env/prec_vent_enter
event:/env/prec_vent_exit
event:/env/prec_vent_loop
event:/env/shroom_in
event:/env/shroom_out
event:/env/slime_killed
event:/env/slime_stick
event:/env/splash
event:/env/teleporter_on_loop
event:/env/use_teleporter_use_loop
event:/env/weather
event:/interface/hover
event:/interface/new_recipe
event:/interface/off_long
event:/interface/on_glitchy
event:/interface/on_long
event:/interface/option_tweek
event:/interface/ping
event:/interface/placeholder
event:/interface/select
event:/interface/text_type
event:/loot/break_breakable
event:/loot/databox/box_open
event:/loot/floater/floater_hold_loop
event:/loot/floater/floater_pickup
event:/loot/floater/floater_place
event:/loot/hit_breakable
event:/loot/loot_locker_open
event:/loot/new_PDA_data
event:/loot/pickup_advancedwiringkit
event:/loot/pickup_aluminumoxide
event:/loot/pickup_battery
event:/loot/pickup_bigwaterbottle
event:/loot/pickup_bleach
event:/loot/pickup_computerchip
event:/loot/pickup_copper
event:/loot/pickup_default
event:/loot/pickup_diamond
event:/loot/pickup_disinfectedwater
event:/loot/pickup_egg
event:/loot/pickup_filteredwater
event:/loot/pickup_fins
event:/loot/pickup_fish
event:/loot/pickup_glass
event:/loot/pickup_gold
event:/loot/pickup_lead
event:/loot/pickup_lithium
event:/loot/pickup_lubricant
event:/loot/pickup_magnesium
event:/loot/pickup_magnetite
event:/loot/pickup_organic
event:/loot/pickup_powercell
event:/loot/pickup_precursorioncrystal
event:/loot/pickup_quartz
event:/loot/pickup_reactorrod
event:/loot/pickup_salt
event:/loot/pickup_scrapmetal
event:/loot/pickup_seatreaderpoop
event:/loot/pickup_silver
event:/loot/pickup_suit
event:/loot/pickup_tank
event:/loot/pickup_titanium
event:/loot/pickup_uraninitecrystal
event:/loot/pickup_uranium
event:/loot/pickup_wiringkit
event:/loot/prec_crystal_loop
event:/main_menu/credits_music
event:/main_menu/intro
event:/main_menu/intro_background
event:/main_menu/intro_cin
event:/main_menu/intro_cin_music
event:/main_menu/menu_background
event:/main_menu/music
event:/main_menu/unmute_all
event:/player/access_denied
event:/player/access_granted
event:/player/adviseSelfScan
event:/player/aurora_countdown
event:/player/aurora_last_transmission
event:/player/aurora_warning_1
event:/player/aurora_warning_2
event:/player/aurora_warning_3
event:/player/batterly_low
event:/player/blast_off
event:/player/blood_loss
event:/player/breathing
event:/player/bubbles
event:/player/change_mission
event:/player/cold_damage
event:/player/coughing
event:/player/cube terminal_close
event:/player/cube terminal_open
event:/player/cube terminal_use
event:/player/damage
event:/player/data_terminal_use
event:/player/depth_100
event:/player/depth_200
event:/player/drink
event:/player/drink_stillsuit
event:/player/eat
event:/player/elevator_down
event:/player/elevator_up
event:/player/end_freedive
event:/player/enzyme_cure
event:/player/enzyme_cure
event:/player/enzyme_cure_pda
event:/player/food_critical
event:/player/food_low
event:/player/food_very_low
event:/player/footstep_dirt
event:/player/footstep_dirt_sprint
event:/player/footstep_grass
event:/player/footstep_grass_sprint
event:/player/footstep_metal
event:/player/footstep_metal_sprint
event:/player/footstep_precursor_base
event:/player/footstep_rocket
event:/player/goal_airsack
event:/player/goal_BiomeKelpForest
event:/player/goal_BiomeKelpForest2
event:/player/goal_BiomePrecursorGunUpper
event:/player/goal_BiomePrecursorGunUpper2
event:/player/goal_BiomePrecursorGunUpper3
event:/player/goal_BiomePrecursorGunUpper4
event:/player/goal_Bladderfish
event:/player/goal_CookedPeeper
event:/player/goal_Copper
event:/player/goal_danger
event:/player/goal_Fins
event:/player/goal_Intro1
event:/player/goal_Knife
event:/player/goal_lifepod1
event:/player/goal_lifepod2
event:/player/goal_lifepod3
event:/player/goal_PDALoading
event:/player/goal_Scanner
event:/player/gun_disabled
event:/player/gun_disabled_pda
event:/player/gun_door_open
event:/player/gun_door_sick
event:/player/gunterminal_access_denied
event:/player/gunterminal_access_granted
event:/player/gunterminal_close
event:/player/gunterminal_first_use
event:/player/gunterminal_open_loop
event:/player/gunterminal_use
event:/player/gunterminal_use_healed
event:/player/hand_infection_cin
event:/player/hatch_eggs
event:/player/heat_damage
event:/player/hungry_damage
event:/player/infection_heal_cin
event:/player/infection_scan_advise
event:/player/infection_scan_advise_full
event:/player/invalid_construction
event:/player/inventory_secured
event:/player/jump
event:/player/key terminal_close
event:/player/key terminal_open
event:/player/key_terminal_use
event:/player/main_brief
event:/player/mapped
event:/player/new_creature
event:/player/new_objective_added
event:/player/new_tech
event:/player/nighttime
event:/player/open_pda
event:/player/oxygen_10
event:/player/oxygen_10_sec
event:/player/oxygen_25
event:/player/oxygen_30_sec
event:/player/oxygen_50
event:/player/Pain
event:/player/Pain_no_tank
event:/player/Pain_no_tank_light
event:/player/Pain_over_time
event:/player/Pain_surface
event:/player/Precursor_Gun_ControlRoom
event:/player/Precursor_Gun_DisableDenied
event:/player/Precursor_Gun_LostRiverHint
event:/player/Precursor_Gun_LostRiverHint2
event:/player/Precursor_LostRiverBase_Log_1
event:/player/Precursor_LostRiverBase_Log_2
event:/player/Precursor_LostRiverBase_Log_4
event:/player/Precursor_LostRiverBase_Log_5
event:/player/Precursor_LostRiverBase_Log_6
event:/player/Precursor_LostRiverBase_Log_7
event:/player/Precursor_LostRiverBase_Log3
event:/player/Puke
event:/player/Puke_underwater
event:/player/radiation
event:/player/radiation_fix
event:/player/radiation_remaining
event:/player/scan_aurora
event:/player/scan_planet
event:/player/selfScan1
event:/player/selfScan2
event:/player/selfScan3
event:/player/selfScan3b
event:/player/selfScan4
event:/player/selfScan5
event:/player/sick_reveal
event:/player/signal_upload
event:/player/single_breath
event:/player/start_freedive
event:/player/start_freedive_splash
event:/player/step
event:/player/stillsuit_equipped
event:/player/story/Aurora_RingRoom_Terminal2
event:/player/story/Deepgrandreef
event:/player/story/Ency_Aurora_RingRoom_Terminal3
event:/player/story/Ending_zinger
event:/player/story/Goal_BaseWindow
event:/player/story/Goal_Bench
event:/player/story/Goal_BiomeBloodKelp
event:/player/story/Goal_BiomeBloodKelp2
event:/player/story/Goal_BiomeCrashedShip
event:/player/story/Goal_BiomeCrashZone
event:/player/story/Goal_BiomeDeepGrandReef
event:/player/story/Goal_BiomeDeepGrandReef2
event:/player/story/Goal_BiomeDunes
event:/player/story/Goal_BiomeFloatingIsland
event:/player/story/Goal_BiomeGrassyPlateaus
event:/player/story/Goal_BiomeJellyCave
event:/player/story/Goal_BiomeJellyCave2
event:/player/story/Goal_BiomeKelpForestCave
event:/player/story/Goal_BiomeKooshZone
event:/player/story/Goal_BiomeLostRiver
event:/player/story/Goal_BiomeMountainCave
event:/player/story/Goal_BiomeMountains
event:/player/story/Goal_BiomeMushroomForest
event:/player/story/Goal_BiomeMushroomForestCave
event:/player/story/Goal_BiomeSafeShallowsCave
event:/player/story/Goal_BiomeSeaTreaderPath
event:/player/story/Goal_BiomeSparseReef
event:/player/story/Goal_BiomeUnderwaterIslands
event:/player/story/Goal_BiomeVoid
event:/player/story/Goal_Bioreactor
event:/player/story/Goal_Bleach
event:/player/story/Goal_Builder
event:/player/story/Goal_CookedGarryfish
event:/player/story/Goal_CookedHoverfish
event:/player/story/Goal_Cyclops
event:/player/story/Goal_Diamond
event:/player/story/Goal_Exo
event:/player/story/Goal_FarmingTray
event:/player/story/Goal_JellyCaveEntrance
event:/player/story/Goal_Location AuroraDriveInterior
event:/player/story/Goal_LocationAuroraDriveEntry
event:/player/story/Goal_LocationAuroraEntry
event:/player/story/Goal_LocationAuroraExoRoom
event:/player/story/Goal_LocationAuroraFurtherIn
event:/player/story/Goal_LocationAuroraHallway
event:/player/story/Goal_LocationAuroraInterior
event:/player/story/Goal_LostRiverEntrance
event:/player/story/Goal_Lubricant
event:/player/story/Goal_Moonpool
event:/player/story/Goal_NuclearReactor
event:/player/story/Goal_Observatory
event:/player/story/Goal_PurifiedWater
event:/player/story/Goal_RadSuit
event:/player/story/Goal_Room
event:/player/story/Goal_Seaglide
event:/player/story/Goal_Seamoth
event:/player/story/Goal_SecondarySystems
event:/player/story/Goal_Silver
event:/player/story/Goal_StasisRifle
event:/player/story/Goal_UnlockRadSuit
event:/player/story/Goal_WaterPark
event:/player/story/OvereatingWarning
event:/player/story/PDASunbeamDestroyEventOutOfRange
event:/player/story/Precursor_Gun_DataDownload1
event:/player/story/Precursor_Gun_DataDownload2
event:/player/story/Precursor_Gun_Disabled
event:/player/story/Precursor_LavaCastle_Log1
event:/player/story/Precursor_LavaCastle_Log2
event:/player/story/Precursor_LavaCastle_Log3
event:/player/story/Precursor_LavaCastle_Log4
event:/player/story/Precursor_Prison_Antechamber_Log1
event:/player/story/Precursor_Prison_Aquarium_EnvironmentLog1
event:/player/story/Precursor_Prison_Aquarium_EnvironmentLog2
event:/player/story/Precursor_Prison_Aquarium_EnvironmentLog3
event:/player/story/Precursor_Prison_Aquarium_EnvironmentLog4
event:/player/story/Precursor_Prison_Aquarium_EnvironmentLog5
event:/player/story/Precursor_Prison_ExteriorLog1
event:/player/story/Precursor_Prison_MoonPool_Log1
event:/player/story/RadioCaptainsQuartersCode
event:/player/story/RadioGrassy25NoSignalAlt
event:/player/story/RadioKelp28NoSignalAlt
event:/player/story/RadioMushroom24NoSignalAlt
event:/player/story/RadioPrecursor1
event:/player/story/RadioRadiationSuitNoSignalAlt
event:/player/story/RadioShallows22NoSignalAlt
event:/player/story/RadioWarper1
event:/player/story/RadioWarper2
event:/player/story/Rocket_ReadyGunStillActive
event:/player/story/Story_AuroraConsole2
event:/player/story/Story_AuroraConsole3
event:/player/story/Story_RadioWarning
event:/player/story/VO/Ency_Aurora_Cargo_PDA2
event:/player/story/VO/Ency_Aurora_Living_Area_PDA2b
event:/player/story/VO/Ency_DeepPDA1
event:/player/story/VO/Ency_DeepPDA2
event:/player/story/VO/Ency_DeepPDA3
event:/player/story/VO/Ency_DeepPDA4
event:/player/story/VO/Ency_InnerBiomeWreckLore7
event:/player/story/VO/Ency_IslandsPDABase1a
event:/player/story/VO/Ency_IslandsPDABase1bDesk
event:/player/story/VO/Ency_IslandsPDABase1Desk
event:/player/story/VO/Ency_IslandsPDABase1Interior
event:/player/story/VO/Ency_IslandsPDAExterior
event:/player/story/VO/Ency_JellyPDABrokenCorridor
event:/player/story/VO/Ency_JellyPDAExterior
event:/player/story/VO/Ency_JellyPDAObservatory
event:/player/story/VO/Ency_JellyPDARoom1Desk
event:/player/story/VO/Ency_JellyPDARoom1Locker
event:/player/story/VO/Ency_JellyPDARoom2Desk
event:/player/story/VO/Ency_JellyPDARoom2Locker
event:/player/story/VO/Ency_Lifepod1
event:/player/story/VO/Ency_Lifepod2
event:/player/story/VO/Ency_Lifepod3
event:/player/story/VO/Ency_Lifepod4
event:/player/story/VO/Ency_LifepodCrashZone1
event:/player/story/VO/Ency_LifepodCrashZone2
event:/player/story/VO/Ency_LifepodDecoy
event:/player/story/VO/Ency_LifepodKeenDialog
event:/player/story/VO/Ency_LifepodRandom
event:/player/story/VO/Ency_LifepodSeaglide
event:/player/story/VO/Ency_OuterBiomeWreckLore9
event:/player/story/VO/Ency_RendezvousFloatingIsland
event:/player/story/VO/RadioBloodKelp29
event:/player/story/VO/RadioGrassy21
event:/player/story/VO/RadioGrassy25
event:/player/story/VO/RadioKelp28
event:/player/story/VO/RadioMushroom24
event:/player/story/VO/RadioRadiationSuit (female)
event:/player/story/VO/RadioRadiationSuit (male)
event:/player/story/VO/RadioSecondOfficer
event:/player/story/VO/RadioShallows22
event:/player/sunbeam_destroy
event:/player/sunbeam_rescue
event:/player/surface
event:/player/surface_no_air
event:/player/surface_tank
event:/player/swim_surface
event:/player/swim_underwater
event:/player/thirsty_damage
event:/player/use_first_aid
event:/player/vitals_ok
event:/player/vomit
event:/player/water_critical
event:/player/water_low
event:/player/water_very_low
event:/sub/base/AI_nopower
event:/sub/base/base_background
event:/sub/base/base_background
event:/sub/base/bioreactor_death
event:/sub/base/bioreactor_hatch_close
event:/sub/base/bioreactor_hatch_open
event:/sub/base/bioreactor_working_loop
event:/sub/base/bulkhead_close_back
event:/sub/base/bulkhead_close_front
event:/sub/base/bulkhead_open_back
event:/sub/base/bulkhead_open_front
event:/sub/base/chargers/battery_charger_close
event:/sub/base/chargers/battery_charger_open
event:/sub/base/chargers/cell_charger_close
event:/sub/base/chargers/cell_charger_open
event:/sub/base/chargers/charge_loop
event:/sub/base/coffeemachine_idle
event:/sub/base/door_close
event:/sub/base/door_open
event:/sub/base/enter_hatch
event:/sub/base/enter_seamoth_left
event:/sub/base/enter_seamoth_right
event:/sub/base/exit_hatch
event:/sub/base/hull_breach
event:/sub/base/hull_decrease
event:/sub/base/hull_increase
event:/sub/base/hull_restored
event:/sub/base/hull_warning
event:/sub/base/make_coffee
event:/sub/base/map room/apear
event:/sub/base/map room/map_room_background
event:/sub/base/map room/option_tweek
event:/sub/base/map room/scan
event:/sub/base/map room/select
event:/sub/base/medkit_locker/close
event:/sub/base/medkit_locker/open
event:/sub/base/nuclear_reactor_death
event:/sub/base/nuclearreactor_working_loop
event:/sub/base/nuke_gen_loop
event:/sub/base/place
event:/sub/base/place
event:/sub/base/power_10_3D
event:/sub/base/power_30_3D
event:/sub/base/power_down
event:/sub/base/power_down_3D
event:/sub/base/power_down_3D
event:/sub/base/power_loop
event:/sub/base/power_loop
event:/sub/base/power_off
event:/sub/base/power_off
event:/sub/base/power_structure_damage
event:/sub/base/power_transmitter_death
event:/sub/base/power_up
event:/sub/base/power_up_3D
event:/sub/base/power_up_3D
event:/sub/base/pressure_damage_heavy
event:/sub/base/pressure_damage_heavy
event:/sub/base/pressure_damage_light
event:/sub/base/pressure_damage_light
event:/sub/base/pressure_damage_medium
event:/sub/base/pressure_damage_medium
event:/sub/base/seamoth_dock
event:/sub/base/solar_panel_death
event:/sub/base/thermal_plant_death
event:/sub/base/vendingmachine_use
event:/sub/base/water_filter_loop
event:/sub/base/water_filter_spawn
event:/sub/base/water_filter_spawn_bottle
event:/sub/base/welcome
event:/sub/base/welcome _3D
event:/sub/common/emergency_music
event:/sub/common/fishsplat
event:/sub/common/splash_in_and_out
event:/sub/common/sub_splash
event:/sub/common/surface_surface
event:/sub/cyclops/AI_abandon
event:/sub/cyclops/AI_ahead_flank
event:/sub/cyclops/AI_ahead_slow
event:/sub/cyclops/AI_ahead_standard
event:/sub/cyclops/AI_attack
event:/sub/cyclops/AI_cavitate
event:/sub/cyclops/AI_decoy
event:/sub/cyclops/AI_depth_update_1
event:/sub/cyclops/AI_depth_update_2
event:/sub/cyclops/AI_depth_warning_1
event:/sub/cyclops/AI_depth_warning_2
event:/sub/cyclops/AI_depth_warning_3
event:/sub/cyclops/AI_drain
event:/sub/cyclops/AI_emergency_speed
event:/sub/cyclops/AI_engine_down
event:/sub/cyclops/AI_engine_heat_critical
event:/sub/cyclops/AI_engine_overheat
event:/sub/cyclops/AI_engine_up
event:/sub/cyclops/AI_external_damage
event:/sub/cyclops/AI_fire_detected
event:/sub/cyclops/AI_fire_extinguished
event:/sub/cyclops/AI_fire_system
event:/sub/cyclops/AI_hull_crit
event:/sub/cyclops/AI_hull_low
event:/sub/cyclops/AI_leak
event:/sub/cyclops/AI_no_power
event:/sub/cyclops/AI_power_low
event:/sub/cyclops/AI_shields
event:/sub/cyclops/AI_silent_running
event:/sub/cyclops/AI_system_failure
event:/sub/cyclops/AI_welcome
event:/sub/cyclops/AI_welcome_attention
event:/sub/cyclops/climb_back_down
event:/sub/cyclops/climb_back_up
event:/sub/cyclops/climb_front_down
event:/sub/cyclops/climb_front_up
event:/sub/cyclops/control_room_ambience
event:/sub/cyclops/creature_attack_sfx
event:/sub/cyclops/cyclops_door_close
event:/sub/cyclops/cyclops_door_open
event:/sub/cyclops/cyclops_helm door_close
event:/sub/cyclops/cyclops_helm_door_open
event:/sub/cyclops/cyclops_loop_epic_fast
event:/sub/cyclops/cyclops_loop_fast
event:/sub/cyclops/cyclops_loop_normal
event:/sub/cyclops/cyclops_loop_rpm
event:/sub/cyclops/cyclops_loop_slow
event:/sub/cyclops/cyclops_stop
event:/sub/cyclops/decoy_loop
event:/sub/cyclops/docking_doors_close
event:/sub/cyclops/docking_doors_open
event:/sub/cyclops/docking_hatch_close
event:/sub/cyclops/docking_hatch_open
event:/sub/cyclops/engine_room
event:/sub/cyclops/enter_hatch
event:/sub/cyclops/exit_hatch
event:/sub/cyclops/explode
event:/sub/cyclops/explode_countdown
event:/sub/cyclops/floodlights_off
event:/sub/cyclops/floodlights_on
event:/sub/cyclops/horn
event:/sub/cyclops/impact_solid_hard
event:/sub/cyclops/impact_solid_medium
event:/sub/cyclops/impact_solid_soft
event:/sub/cyclops/install_mod
event:/sub/cyclops/interface_popup
event:/sub/cyclops/interface_select
event:/sub/cyclops/leak_loop
event:/sub/cyclops/lights_off
event:/sub/cyclops/lights_on
event:/sub/cyclops/load_decoy
event:/sub/cyclops/locker_close
event:/sub/cyclops/locker_close
event:/sub/cyclops/locker_open
event:/sub/cyclops/locker_open
event:/sub/cyclops/outer_hatch_close
event:/sub/cyclops/outer_hatch_open
event:/sub/cyclops/pressure_damage_heavy
event:/sub/cyclops/pressure_damage_light
event:/sub/cyclops/pressure_damage_medium
event:/sub/cyclops/proximity
event:/sub/cyclops/shield_on_loop
event:/sub/cyclops/siren
event:/sub/cyclops/siren_3d
event:/sub/cyclops/sonar
event:/sub/cyclops/start
event:/sub/cyclops/sub_ambieance
event:/sub/drone/change_camera
event:/sub/drone/connected
event:/sub/drone/connecting_loop
event:/sub/drone/idle
event:/sub/drone/motor_loop
event:/sub/exo/claw_hit_fish
event:/sub/exo/claw_hit_terain
event:/sub/exo/claw_pickup
event:/sub/exo/claw_punch
event:/sub/exo/dock_exo_cyclops
event:/sub/exo/dock_exo_moonpool
event:/sub/exo/drill_hit_loop
event:/sub/exo/drill_loop
event:/sub/exo/enter_exo
event:/sub/exo/hook_hit
event:/sub/exo/hook_loop
event:/sub/exo/hook_shoot
event:/sub/exo/interior_loop
event:/sub/exo/jets_empty
event:/sub/exo/jets_loop
event:/sub/exo/jump
event:/sub/exo/land
event:/sub/exo/land_precursor
event:/sub/exo/sit
event:/sub/exo/step
event:/sub/exo/step_precursor
event:/sub/exo/undock_exo_cyclops
event:/sub/exo/undock_exo_moonpool
event:/sub/exo/welcome
event:/sub/pod/climb_in_bottom
event:/sub/pod/climb_in_top
event:/sub/pod/climb_out_bottom
event:/sub/pod/climb_out_bottom_first_use
event:/sub/pod/climb_out_top
event:/sub/pod/climb_out_top_first_use
event:/sub/pod/fix_panel_powerup
event:/sub/pod/pod_background
event:/sub/pod/pod_background_damaged
event:/sub/pod/pod_fire
event:/sub/pod/pod_fire
event:/sub/pod/pod_sparks
event:/sub/pod/radio/radio_generic
event:/sub/pod/radio/radio_lifepod_17
event:/sub/pod/radio/radio_lifepod_bounceback
event:/sub/pod/radio/radio_new_msg_loop
event:/sub/pod/radio/radio_play
event:/sub/pod/radio/radio_power_on
event:/sub/pod/radio/radioKoosh26
event:/sub/pod/radio/radioMushroom24
event:/sub/pod/radio/radioRadiationSuit
event:/sub/pod/radio/radioSunbeam_not coming
event:/sub/pod/radio/radioSunbeamEvent01
event:/sub/pod/radio/radioSunbeamEvent02
event:/sub/pod/radio/radioSunbeamEvent03
event:/sub/pod/radio/radioSunbeamEvent04
event:/sub/pod/radio/radioSunbeamEvent05
event:/sub/pod/radio/radioSunbeamEvent05_PDA
event:/sub/rocket/all_systems_go
event:/sub/rocket/auxilery_on
event:/sub/rocket/call_lift_loop
event:/sub/rocket/call_lift_loop_2
event:/sub/rocket/computer_on
event:/sub/rocket/coms_on
event:/sub/rocket/end_cin_music
event:/sub/rocket/end_cin_music_short
event:/sub/rocket/end_cin_sfx
event:/sub/rocket/end_cin_sfx_short
event:/sub/rocket/epic_splash
event:/sub/rocket/hydrolics_on
event:/sub/rocket/ladders/innerPlatform_ladder_down
event:/sub/rocket/ladders/innerPlatform_ladder_up
event:/sub/rocket/ladders/innerRocketShip_ladder_down
event:/sub/rocket/ladders/innerRocketShip_ladder_up
event:/sub/rocket/ladders/outerPlatform_ladder_down
event:/sub/rocket/ladders/outerPlatform_ladder_up
event:/sub/rocket/lifesupport_on
event:/sub/rocket/lift_arrive
event:/sub/rocket/lift_beep
event:/sub/rocket/outside_bottom_loop
event:/sub/rocket/rocket
event:/sub/rocket/rocket_door_open
event:/sub/rocket/rocket_door_open_inside
event:/sub/rocket/rocket_finished
event:/sub/rocket/rocket_interior_loop
event:/sub/rocket/stage_build
event:/sub/rocket/start
event:/sub/rocket/stop
event:/sub/rocket/telemerty_on
event:/sub/rocket/time capsual_launch
event:/sub/rocket/time_pod_close
event:/sub/rocket/time_pod_open
event:/sub/rocket/time_pod_open_first_use
event:/sub/rocket/time_pod_open_first_use_VO
event:/sub/rocket/use_lift
event:/sub/rocket/use_lift_2
event:/sub/seamoth/AI_no_power
event:/sub/seamoth/crush_damage
event:/sub/seamoth/crush_depth_update
event:/sub/seamoth/crush_depth_warning
event:/sub/seamoth/depth_update
event:/sub/seamoth/depth_warning
event:/sub/seamoth/dock
event:/sub/seamoth/dock_seamoth_cyclops
event:/sub/seamoth/enter_seamoth
event:/sub/seamoth/first_use
event:/sub/seamoth/glass_break
event:/sub/seamoth/hull_breach_warning
event:/sub/seamoth/hull_fix
event:/sub/seamoth/impact_solid_hard
event:/sub/seamoth/impact_solid_medium
event:/sub/seamoth/impact_solid_soft
event:/sub/seamoth/interior_loop
event:/sub/seamoth/power_down
event:/sub/seamoth/pulse
event:/sub/seamoth/pulse_charge_loop
event:/sub/seamoth/seaglide_equip
event:/sub/seamoth/seaglide_light_off
event:/sub/seamoth/seaglide_light_on
event:/sub/seamoth/seaglide_loop
event:/sub/seamoth/seaglide_loop_rpm
event:/sub/seamoth/seaglide_rev_up
event:/sub/seamoth/seaglide_stop
event:/sub/seamoth/seaglide_upgrade_loop
event:/sub/seamoth/seajet_equip
event:/sub/seamoth/seamoth_light_off
event:/sub/seamoth/seamoth_light_on
event:/sub/seamoth/seamoth_loop
event:/sub/seamoth/seamoth_loop_rpm
event:/sub/seamoth/seamoth_rev_up
event:/sub/seamoth/seamoth_stop
event:/sub/seamoth/select
event:/sub/seamoth/sonar_loop
event:/sub/seamoth/sparks
event:/sub/seamoth/storage_close
event:/sub/seamoth/storage_open
event:/sub/seamoth/torpedo_armed
event:/sub/seamoth/torpedo_disarmed
event:/sub/seamoth/torpedo_explode_loop
event:/sub/seamoth/torpedo_fire
event:/sub/seamoth/undock
event:/sub/seamoth/undock_seamoth_cyclops
event:/sub/seamoth/welcome
event:/sub_module/fabricator/build
event:/sub_module/fabricator/build_long_end
event:/sub_module/fabricator/build_long_loop
event:/sub_module/fabricator/build_long_start
event:/sub_module/fabricator/close
event:/sub_module/fabricator/fabricator_click
event:/sub_module/fabricator/fabricator_new_item
event:/sub_module/fabricator/menu_up
event:/sub_module/fabricator/negative
event:/sub_module/fabricator/open
event:/sub_module/first_aid/spawn
event:/sub_module/workbench/build_long_loop
event:/sub_module/workbench/close
event:/sub_module/workbench/open
event:/sub_module/workbench/working
event:/tools/airbladder/airbladder_deflate
event:/tools/airbladder/airbladder_deflate_surface
event:/tools/airbladder/airbladder_hold_loop
event:/tools/airbladder/airbladder_inflate
event:/tools/airbladder/airbladder_pickup
event:/tools/battery_die
event:/tools/battery_insert
event:/tools/beacon/beacon_loop
event:/tools/beacon/draw
event:/tools/builder/complete
event:/tools/builder/deploy
event:/tools/builder/first_use
event:/tools/builder/loop
event:/tools/builder/place
event:/tools/builder/remove
event:/tools/constructor/bot_1_hover
event:/tools/constructor/bot_2_hover
event:/tools/constructor/bot_3_hover
event:/tools/constructor/bot_4_hover
event:/tools/constructor/bots_build
event:/tools/constructor/bots_confirm
event:/tools/constructor/bots_fly_infront
event:/tools/constructor/bots_out
event:/tools/constructor/constructor_climb
event:/tools/constructor/constructor_deploy_end
event:/tools/constructor/constructor_deploy_start
event:/tools/constructor/deploy
event:/tools/constructor/spawn
event:/tools/constructor/sub_splash
event:/tools/diamondblade/deploy
event:/tools/diamondblade/hit
event:/tools/diamondblade/swing
event:/tools/divereel/breadcrum
event:/tools/divereel/deploy
event:/tools/divereel/node_loop
event:/tools/divereel/reeling
event:/tools/divereel/reload
event:/tools/divereel/reset_path
event:/tools/divereel/set_anchor
event:/tools/divereel/unravel
event:/tools/dolls/jack
event:/tools/dolls/mark
event:/tools/extinguisher/deploy
event:/tools/extinguisher/first use
event:/tools/extinguisher/shoot
event:/tools/extinguisher/use_loop
event:/tools/flare/deploy
event:/tools/flare/first_use
event:/tools/flare/on_loop
event:/tools/flare/throw
event:/tools/flashlight/deploy
event:/tools/flashlight/fist_use
event:/tools/flashlight/turn_off
event:/tools/flashlight/turn_on
event:/tools/floating_locker/close
event:/tools/floating_locker/deploy
event:/tools/floating_locker/open
event:/tools/gravcannon/deploy
event:/tools/gravcannon/fail
event:/tools/gravcannon/fire
event:/tools/gravcannon/first_use
event:/tools/gravcannon/grab_loop
event:/tools/gravcannon/ready
event:/tools/gravcannon/reload
event:/tools/gravcannon/repulse
event:/tools/gravsphere/draw
event:/tools/gravsphere/explode
event:/tools/gravsphere/loop
event:/tools/gravsphere/loop_actual
event:/tools/knife/deploy
event:/tools/knife/first_use
event:/tools/knife/heat_hit
event:/tools/knife/heat_idle
event:/tools/knife/hit
event:/tools/knife/swing
event:/tools/knife/swing_surface
event:/tools/lasercutter/cut_end
event:/tools/lasercutter/cut_loop
event:/tools/lasercutter/deploy
event:/tools/lasercutter/door_cut
event:/tools/lasercutter/first_use
event:/tools/lights/pick_up
event:/tools/lights/place
event:/tools/pda/close
event:/tools/pda/drop_item
event:/tools/pda/equip_gear
event:/tools/pda/first_use
event:/tools/pda/new_biome
event:/tools/pda/open
event:/tools/pda/select
event:/tools/pda/unequip_gear
event:/tools/pipes/bubbles_loop
event:/tools/pipes/motor_loop
event:/tools/scan
event:/tools/scanner/draw
event:/tools/scanner/first_use
event:/tools/scanner/new_blueprint
event:/tools/scanner/new_encyclopediea
event:/tools/scanner/new_PDA_data
event:/tools/scanner/scan_complete
event:/tools/scanner/scan_loop
event:/tools/stasis_gun/charge
event:/tools/stasis_gun/deploy
event:/tools/stasis_gun/fire
event:/tools/stasis_gun/first_use
event:/tools/stasis_gun/holding_loop
event:/tools/stasis_gun/sphere_activate
event:/tools/stasis_gun/sphere_deactivate
event:/tools/stasis_gun/sphere_enter
event:/tools/swimcharge
event:/tools/teraformer/cut_loop
event:/tools/teraformer/cut_ready
event:/tools/teraformer/draw
event:/tools/teraformer/place_loop
event:/tools/transfuser/deploy
event:/tools/transfuser/fail
event:/tools/transfuser/inject_serum
event:/tools/transfuser/take_sample
event:/tools/use_loot
event:/tools/welder/deploy
event:/tools/welder/first_use
event:/tools/welder/weld_end
event:/tools/welder/weld_loop
            */
            }
    }
}
