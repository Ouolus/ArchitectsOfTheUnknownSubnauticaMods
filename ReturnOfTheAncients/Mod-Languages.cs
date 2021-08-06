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
            LanguageHandler.SetLanguageLine($"EncyPath_{modEncyPath_root}", "Return of the Ancients");
            LanguageHandler.SetLanguageLine($"EncyPath_{modEncyPath_analysis}", "Analysis");
            LanguageHandler.SetLanguageLine($"EncyPath_{modEncyPath_terminalInfo}", "Information");
            LanguageHandler.SetLanguageLine($"EncyPath_{modEncyPath_tech}", "Technology");
            LanguageHandler.SetLanguageLine($"EncyPath_{modEncyPath_relics}", "Relics");
            LanguageHandler.SetLanguageLine($"EncyPath_{modEncyPath_gargantuan}", "Gargantuan Leviathan");
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
        }
        #endregion
    }
}