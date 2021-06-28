namespace RotA
{
    using SMLHelper.V2.Handlers;
    
    public partial class Mod
    {
        public const string modEncyPath_gargantuan = "GargMod/GargModGargantuanLeviathan";
        public const string modEncyPath_analysis = "GargMod/GargModPrecursorAnalysis";
        
        const string modEncyPath_root = "GargMod";
        const string modEncyPath_terminalInfo = "GargMod/GargModInformation";
        const string modEncyPath_tech = "GargMod/GargModPrecursorTech";
        const string modEncyPath_relics = "GargMod/GargModPrecursorRelics";
        const string ency_tertiaryOutpostTerminalGrassy = "TertiaryOutpostTerminal1Ency";
        const string ency_tertiaryOutpostTerminalSparseReef = "TertiaryOutpostTerminal2Ency";
        const string ency_tertiaryOutpostTerminalLostRiver = "TertiaryOutpostTerminal3Ency";
        const string ency_supplyCacheTerminal = "SupplyCacheTerminalEncy";
        const string ency_researchBaseTerminal = "ResearchBaseTerminalEncy";
        const string ency_kooshBaseTerminal = "KooshBaseTerminalEncy";
        const string ency_archElectricityTerminal = "ArchitectElectricityEncy";
        const string ency_voidBaseTerminal = "VoidBaseTerminalEncy";
        const string ency_ruinedGuardian = "RuinedGuardianEncy";
        const string ency_distressSignal = "GuardianTerminalEncy";
        const string ency_tailfin = "GuardianTailfinEncy";
        const string ency_secondaryBaseModel = "SecondaryBaseModelEncy";
        const string ency_voidBaseModel = "VoidBaseModelEncy";
        const string ency_precingot = "PrecursorIngotEncy";
        const string ency_cachePings = "CachePingsEncy";
        const string ency_precrifle = "PrecursorRifleEncy";
        const string ency_precblade = "PrecursorBladeEncy";
        const string ency_precbuilder = "PrecursorBuilderEncy";
        const string ency_alienSpam = "PrecursorSpamEncy";
        const string ency_eggRoom = "PrecursorEggRoomEncy";
        const string ency_aquariumSkeleton = "BabyGargSkeletonEncy";
        const string ency_blackHole = "ResearchBaseSingularityEncy";
        const string ency_warpCannonTerminal = "WarpCannonTerminalEncy";
        const string ency_omegaCubeFabricator = "OmegaCubeFabricatorEncy";
        const string alienSpamEncyText = "This data terminal consists primarily of text in several unknown languages. Partially translated text is displayed below:\n\nTransfer of \u2580\u2596\u2517\u259b\u2584\u2596 failed. Sector Zero study of \u259c\u259a\u2523 \u259c\u259a\u2517\u2523\u2517\u252b\u2513\u250f\u2513 terminated for \u259b\u2584\u2596\u2505\u2517\u2596.\n\n\u2523\u2517\u250f\u259b\u2584\u2596\u259c\u250f\u2523 \u259a \u2596\u259e\u2523\u2517\u2596\u2517\u2523.\n\nVaccine progress: Awaiting termination.\n\nEmperor Apparatus status: Functioning.\n\n\u2523\u2517\u2596\u2503\u2580\u259a\u2597\u250f\u250f\u2513. \u2596\u251b\u2580\u2517\u259e\u2503\u250f\u2584 distress \u2580\u2596\u2517\u259b\u2596\u259c\u259a\u2523 data \u2505\u2596\u2517\u2501\u2596 \u2596\u2513\u252b\u259e\u2523 \u259a \u259b\u2584\u2505\u2517\u2596 \u259a \u2596\u259e\u2523\u2517\u2596\u2517\u2523 \u259a\u251b\u2598\u259e\u2501\u2596\u2505 \u259e\u2523\u2517\u2596\u2517\u2523.\n\n'Architects of the \u259a\u251b\u2598\u259e' status: missing. \u2501\u2596\u2505.\n\n\u2580\u2596\u2517\u259b\u259a\u2523 \u259c\u259a\u2517 \u259c\u259a.\n\nSpecimen of the Ancients terminated. Error: termination failed.";

        #region Languages
        
        static void PatchLanguage()
        {
            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Titans", "Titans");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_root), "Return of the Ancients");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_analysis), "Analysis");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_terminalInfo), "Information");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_tech), "Technology");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_relics), "Relics");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_gargantuan), "Gargantuan Leviathan");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyWarpKey, "Teleport to base: {1}\nCurrent mode: Personal teleportation. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyCreatureKey, "Warp in creatures: {1}\nCurrent mode: Creature summon. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyManipulateFirePrimaryKey, "Create exit portal: {1}\nCurrent mode: Environment manipulation. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyManipulateFireSecondaryKey, "Create entrance portal: {1}\nCurrent mode: Environment manipulation. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonNotEnoughPowerError, "Insufficient power.");

            LanguageHandler.SetLanguageLine(omegaTerminalHoverText, "Omega terminal");
            LanguageHandler.SetLanguageLine(omegaTerminalInteract, "Insert data");
            LanguageHandler.SetLanguageLine(omegaTerminalRegenerateCube, "Generate omega cube");
        }
        
        #endregion

        #region Encys

        static void PatchEncys()
        {
            PatchEncy(ency_tertiaryOutpostTerminalGrassy, modEncyPath_terminalInfo, "Tertiary Outpost C Analysis", "An alien outpost, likely used as a charging system for powered devices.\n\n1. Data Terminal:\nAn alien data terminal with a blue holographic symbol. Contains co-ordinates pointing to two secondary outposts.\n\n2. Charging device:\nA claw-shaped device that can output large amounts of energy, from an unknown source. You just read cut content, by the way :)", "Popup_Blue", "TO_G_Ency");

            PatchEncy(ency_tertiaryOutpostTerminalSparseReef, modEncyPath_terminalInfo, "Tertiary Outpost A Analysis", "An alien outpost, likely used as a charging system for powered devices.\n\n1. Data Terminal:\nAn alien data terminal with a blue holographic symbol. Contains co-ordinates pointing to two secondary outposts.\n\n2. Charging device:\nA claw-shaped device that can output large amounts of energy, from an unknown source.\n\n3. Alien robot:\nThese devices are a common occurence in all alien technology. However, they are likely present in this base only to repair alien machinery while it is charging in the claw device.", "Popup_Blue", "TO_S_Ency");

            PatchEncy(ency_tertiaryOutpostTerminalLostRiver, modEncyPath_terminalInfo, "Tertiary Outpost B Analysis", "An alien outpost, likely used as a charging system for powered devices.\n\n1. Data Terminal:\nAn alien data terminal with a blue holographic symbol. Contains co-ordinates pointing to two secondary outposts.\n\n2. Charging device:\nA claw-shaped device that can output large amounts of energy, from an unknown source.\n\n3. Orange Tablet:\nA rare alien artifact. It was likely put here for future use, but was never reclaimed.", "Popup_Blue", "TO_LR_Ency");

            PatchEncy(ency_supplyCacheTerminal, modEncyPath_terminalInfo, "Alien Supply Cache", "This large structure appears to be designed to hold valuabe resources for potential future use.\n\nAnalysis:\n- Large pillar-shaped storage units line either side of the interior. The materials inside are condensed as far as physically possible in order to maintain a minuscule volume.\n- Several exploitable mineral deposits are found loosely scattered in the base. A potential reason for this is an overflow of dedicated storage.\n- Several small alien structural alloy ingots are on display in the base. Their purpose appears to be aesthetic. Retrieval methods are still unknown.\n- The arch-like structure situated in the back of the cache, if not decorational, was likely used for quick transportation of supplies.", "Popup_Green", "SupplyCache_Ency");

            PatchEncy(ency_researchBaseTerminal, modEncyPath_terminalInfo, "Destructive Technology Research Base", "This outpost acted as a hub for the testing of extremely destructive technology. Examples of this technology include a powerful ionic pulse defense mechanism, a kind of sentry unit, and a uniquely designed weapon.\n\nAnalysis:\n- Lacking extensive decorations and structures, this base appears to be solely dedicated to research of destructive technology.\n- Mentions of a project under the name \"GUARDIAN\" are present, but any files that may have pertained to this project are either missing, corrupt, or encrypted.\n- The development and usage of this technology appears to have contributed to the destruction of the local ecosystem, which was once flourishing with life.\n\nThe technology in this base may be exploited for personal use. Use with caution.", "Popup_green", "ResearchBase_Ency");

            PatchEncy(ency_kooshBaseTerminal, modEncyPath_terminalInfo, "Data Storage Facility", " ???", "Popup_green", "ResearchBase_Ency");

            PatchEncy(ency_ruinedGuardian, modEncyPath_analysis, "Mysterious Wreckage", "The shattered remains of a vast alien machine.\n\n1. Purpose:\nThe exact purpose of this device remains vague, but the hydrodynamic build, reinforced structure and various defence mechanisms suggest a mobile sentry. It was presumably tasked with guarding a location of significant importance from nearby roaming leviathan class lifeforms.\n\n2. Damage:\nAnalysis of the wreck reveals extensive damage in various places, which resulted in a near total system failure. The damage is consistent with being crushed, despite the extraordinary integrity of the construction material. The current state of the remains indicate the incident occurred recently and within the vicinity, despite no obvious culprit being found nearby. Whatever its purpose, it has obviously failed.\n\nAssessment: Further Research Required. Caution is advised.", "Guardian_Popup", "Guardian_Ency");

            PatchEncy(ency_distressSignal, modEncyPath_tech, "Alien Distress Signal", "This Data Terminal has given your PDA access to an encrypted tracking network. The only activity on the network is a distress signal from over a kilometer away. Proceed with caution.", "Popup_Blue", "BlueGlyph_Ency");

            PatchEncy(ency_archElectricityTerminal, modEncyPath_tech, "Ionic Pulse Nanotechnology", "(Cut content). This Data Terminal contains the blueprints for an advanced nanotechnology used to generate a powerful plasma-based charge with a distinctive green glow. The applications of this medium include transferring high amounts of energy and incapacitating large fauna.\n\nYour PDA has generated several new upgrade blueprints which exploit this discovery.\n\nSynthesized blueprints:\n- Seamoth Perimeter Defense MK2\n- Prawn Suit Ion Defense Module\n- Creature Decoy MK2\n- Prawn Suit Ion Dash Module", "IonicPulse_Popup", "OrangeGlyph_Ency");

            PatchEncy(ency_voidBaseTerminal, modEncyPath_terminalInfo, "Emperor Communications Apparatus", "This data terminal contains schematics and statistics relating to the facility. Analysis is shown below.\n\nDue to the outbreak of the kharaa bacterium, the aliens were desperate to develop a vaccine. The only known cure at the time, found in the last known 'Emperor', is too diluted to provide any definite use.\n\nDue to belief that more of these Emperors may exist, far away from the crater, this apparatus was constructed. While initially appearing similar to any other alien structure on the planet, with the primary focus being research, schematics show an odd ability to expand downwards over half a kilometer, exposing a significant number of complex machines.\n\nThis machinery was designed to communicate with and attract any stray Emperors. Obviously, this plan has failed. However, it did attract another unusual juvenile specimen.\n\nThe base was eventually repurposed solely for vaccine development studies.", "Popup_green", "VoidBase_Ency");

            PatchEncy(ency_tailfin, modEncyPath_analysis, "Alien Machine Tail Segment", "The tail of some sort of segmented machine. A lack of obvious damage suggests it was designed for intentional uncoupling when in danger.");

            PatchEncy(ency_secondaryBaseModel, modEncyPath_analysis, "Cache Structure", "A large structure with a mysterious design, used as long-term storage of data and resources. The entrance is forcefield-protected and airlocked, most likely to protect the valuables inside.");

            PatchEncy(ency_voidBaseModel, modEncyPath_analysis, "Suspended Platform", "A massive structure, over 300 meters in height. It is impossible to determine any applications of a base this large in such a dangerous area.\n\nThe unique design with the majority of the interior being inaccessible suggests a non-conventional use. More information may be located inside the structure.");

            PatchEncy(ency_precingot, modEncyPath_relics, "Alien Structural Alloy", "An unnamed alloy with unprecedented integrity. Appears to be non-malleable with any known technology. Luminescent detailing also suggests complex inner circuitry.\n\nNo practical applications can be simulated for this object.", "PrecIngot_Popup", "PrecIngot_Ency");

            PatchEncy(ency_cachePings, modEncyPath_terminalInfo, "Caches Location Data", "This Data Terminal contains a map with the co-ordinates of many locations, with data related to each. These co-ordinates have been uploaded to your PDA.\n\nLocational data:\n- Sanctuary Alpha: Found in the depths of a barren biome. Requires a purple tablet.\n- Sanctuary Beta: Found deep underwater in a dark cave system. Requires a purple tablet.\n- Sanctuary Cappa: Found near a mysterious crater. Requires a purple tablet.\n- Laboratory: Found in a frigid cave system. Requires an orange tablet.\n\nCo-ordinates that are unusually pointing to the equator have not been uploaded. Traveling that far of a distance would be close to impossible.", "Popup_Blue", "BlueGlyph_Ency");

            PatchEncy(ency_precrifle, modEncyPath_relics, "Alien Rifle Variant", "This weapon strongly resembles a similar alien device found on the planet. The coloration however appears more close to the distinct architectural style of the aliens. Being powered by ion energy, it must have been extremely powerful.", "PrecRifle_Popup", "PrecRifle_Ency");

            PatchEncy(ency_precblade, modEncyPath_relics, "Alien Knife", "An alien knife with obvious applications. A lack of luminosity, which is almost always found in alien technology, suggests it is no longer powered.");

            PatchEncy(ency_precbuilder, modEncyPath_relics, "Alien Construction Tool", "An ancient construction tool that appears uncannily similar to the Alterra Habitat Builder.\n\nThis device was likely used to design and create large structures with ease, including all of the alien structures found on the planet. The fact that it has been left in stasis on site suggests this was the last structure it has ever built.", "PrecursorBuilder_Popup", "PrecursorBuilder_Ency");

            PatchEncy(ency_alienSpam, modEncyPath_terminalInfo, "Alien Document", alienSpamEncyText, "Popup_green", "GreenGlyph_Ency");

            PatchEncy(ency_eggRoom, modEncyPath_terminalInfo, "Research Laboratory Logs", "This document is portrayed in a way that is universally understood. File too large to upload to PDA.\n\nTranscript:\n- Unknown creature is approaching facility from below.\n- Creature appears to act extremely aggressive.\n- Creature is attacking Communications Apparatus.\n- Creature has been captured for vaccine research.\n- Specimen analysis: Juvenile leviathan-class creature. Has very few similarities in DNA with any other species observed on the planet.\n- 1 DNA match found: Egg Specimen 18.\n- Specimen interestingly has no signs of infection. Preparing specimen for Kharaa testing procedures.\n- [PERFORMING PLANET-WIDE QUARANTINE. VACCINE DEVELOPMENT TERMINATED. SPECIMEN PLACED IN LONG-TERM STORAGE TANK]", "Popup_green", "GreenGlyph_Ency");

            PatchEncy(ency_aquariumSkeleton, modEncyPath_analysis, "Gargantuan Skeleton", "The skeletal remains of a juvenile leviathan specimen, encased in a sealed environment. Carbon dating shows it has died approximately one thousand years ago. Relative intactness of the bones suggests it has died of starvation.");

            PatchEncy(ency_blackHole, modEncyPath_analysis, "Contained Singularity", "A highly unstable object with immeasurably high mass contained via gravity manipulation. If released it could absorb the entire solar system in a relatively short amount of time. It was likely designed to be used as a weapon, a quarantine failsafe option, or at the very least a way to intimidate other species. If that is true, it has certainly succeeded.\n\nAssessment: Do not touch.");

            PatchEncy(ency_warpCannonTerminal, modEncyPath_tech, "Handheld Warping Device Schematics", "This terminal contains schematics for alien warp technology, which utilizes the unnatural properties of Electricubes. As a result of this finding, your PDA has synthesized blueprints for a Handheld Warping Device.\n\nWarping Device Operation:\nEnergy is quickly routed through the system from the battery up into the barrel, where it can be released in a controlled pulse. The magnitude and effects of this pulse can be adjusted by precise mechanical rotations to the four calibration discs.\n\nThese pulses generate a warping field that transports atoms from a point to another at extremely fast speeds.\n\nPractical uses include personal teleportation and manipulation of the state of objects in the environment.", "WarpCannon_Popup", "WarpCannon_Ency");

            PatchEncy(ency_omegaCubeFabricator, modEncyPath_tech, "Omega Cube Fabrication Device", "A powerful device which is obviously alien in design. Appears to be a fabrication device of some sort.\n\nThe data terminal connected to the fabricator has an incomplete set of data. It is emitting a broadcast, which reads in English: \"Project 'Omega' formula is 95% complete. Data sets missing: '1'. Data set type: 'biometric data'. Species name: 'Gargantuan Leviathan'. Data must be obtained from an adult specimen.\"\n\nYou must scan an adult Gargantuan Leviathan to complete this formula. Good luck.");
        }
        #endregion
    }
}