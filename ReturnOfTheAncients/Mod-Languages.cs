namespace RotA
{
    using SMLHelper.V2.Handlers;
    
    public partial class Mod
    {
        public const string modEncyPath_root = "RotA";
        public const string modEncyPath_terminalInfo = "RotA/RotATerminalInformation";
        public const string modEncyPath_tech = "RotA/RotAPrecursorTech";
        public const string modEncyPath_relics = "RotA/RotAPrecursorRelics";
        public const string modEncyPath_gargantuan = "RotA/RotAGargantuanLeviathan";
        public const string modEncyPath_analysis = "RotA/RotAPrecursorAnalysis";
        
        const string ency_tertiaryOutpostTerminalGrassy = "TertiaryOutpostTerminal1";
        const string ency_tertiaryOutpostTerminalSparseReef = "TertiaryOutpostTerminal2";
        const string ency_tertiaryOutpostTerminalLostRiver = "TertiaryOutpostTerminal3";
        const string ency_supplyCacheTerminal = "SupplyCacheTerminal";
        const string ency_researchBaseTerminal = "ResearchBaseTerminal";
        const string ency_kooshBaseTerminal = "KooshBaseTerminal";
        const string ency_archElectricityTerminal = "ArchitectElectricity";
        const string ency_voidBaseTerminal = "VoidBaseTerminal";
        const string ency_ruinedGuardian = "RuinedGuardian";
        const string ency_distressSignal = "GuardianTerminal";
        const string ency_tailfin = "GuardianTailfin";
        const string ency_secondaryBaseModel = "SecondaryBaseModel";
        const string ency_voidBaseModel = "VoidBaseModel";
        const string ency_precingot = "PrecursorIngot";
        const string ency_cachePings = "CachePings";
        const string ency_precrifle = "PrecursorRifle";
        const string ency_precblade = "PrecursorBlade";
        const string ency_precbuilder = "PrecursorBuilder";
        const string ency_alienSpam = "PrecursorSpam";
        const string ency_eggRoom = "PrecursorEggRoomy";
        const string ency_aquariumSkeleton = "BabyGargSkeleton";
        const string ency_blackHole = "";
        const string ency_warpCannonTerminal = "WarpCannonTerminal";
        const string ency_omegaCubeFabricator = "OmegaCubeFabricator";
        const string ency_buildablesonicdeterrent = "BuildableSonicDeterrent";

        public const string warpCannonSwitchFireModeCurrentlyWarpKey = "WarpCannonSwitchFireModeWarp";
        public const string warpCannonSwitchFireModeCurrentlyCreatureKey = "WarpCannonSwitchFireModeCreature";
        public const string warpCannonSwitchFireModeCurrentlyManipulateFirePrimaryKey = "WarpCannonSwitchFireModeManipulatePrimary";
        public const string warpCannonSwitchFireModeCurrentlyManipulateFireSecondaryKey = "WarpCannonSwitchFireModeManipulateSecond";
        public const string warpCannonNotEnoughPowerError = "WarpCannonNotEnoughPowerError";

        public const string ionKnifeUseTextFormat = "IonKnifeUseFormat";
        public const string ionKnifeUseTextNoCubeLoadedFormat = "IonKnifeUseNoCubeLoadedFormat";
        public const string ionKnifePrecursorIonCrystal = "IonKnifePrecursorIonCrystal";
        public const string ionKnifeElectricube = "IonKnifeElectricube";
        public const string ionKnifeRedIonCube = "IonKnifeRedIonCube";
        public const string ionKnifeOmegaCube = "IonKnifeOmegaCube";

        public const string omegaTerminalHoverText = "OmegaTerminalHoverText";
        public const string omegaTerminalInteract = "OmegaTerminalInteract";
        public const string omegaTerminalRegenerateCube = "OmegaCubeRegenerateCube";

        #region Languages

        static void PatchLanguage()
        {
            
        }
        
        #endregion

        #region Encys

        static void PatchEncys()
        {
            //cut content
            PatchEncy(ency_tertiaryOutpostTerminalGrassy, modEncyPath_terminalInfo, "Popup_Blue", "TO_G_Ency");

            PatchEncy(ency_tertiaryOutpostTerminalSparseReef, modEncyPath_terminalInfo, "Popup_Blue", "TO_S_Ency");

            PatchEncy(ency_tertiaryOutpostTerminalLostRiver, modEncyPath_terminalInfo, "Popup_Blue", "TO_LR_Ency");

            PatchEncy(ency_supplyCacheTerminal, modEncyPath_terminalInfo, "Popup_Green", "SupplyCache_Ency");

            PatchEncy(ency_researchBaseTerminal, modEncyPath_terminalInfo, "Popup_green", "ResearchBase_Ency");

            PatchEncy(ency_kooshBaseTerminal, modEncyPath_terminalInfo, "Popup_green", "ResearchBase_Ency");

            PatchEncy(ency_ruinedGuardian, modEncyPath_analysis, "Guardian_Popup", "Guardian_Ency");

            PatchEncy(ency_distressSignal, modEncyPath_tech, "Popup_Blue", "BlueGlyph_Ency");

            PatchEncy(ency_archElectricityTerminal, modEncyPath_tech, "IonicPulse_Popup", "OrangeGlyph_Ency");

            PatchEncy(ency_voidBaseTerminal, modEncyPath_terminalInfo, "Popup_green", "VoidBase_Ency");

            PatchEncy(ency_tailfin, modEncyPath_analysis);

            PatchEncy(ency_secondaryBaseModel, modEncyPath_analysis);

            PatchEncy(ency_voidBaseModel, modEncyPath_analysis);

            PatchEncy(ency_precingot, modEncyPath_relics, "PrecIngot_Popup", "PrecIngot_Ency");

            PatchEncy(ency_cachePings, modEncyPath_terminalInfo, "Popup_Blue", "BlueGlyph_Ency");

            PatchEncy(ency_precrifle, modEncyPath_relics, "PrecRifle_Popup", "PrecRifle_Ency");

            PatchEncy(ency_precblade, modEncyPath_relics, "OmegaBlade_Popup", "OmegaBlade_Ency");

            PatchEncy(ency_precbuilder, modEncyPath_relics, "PrecursorBuilder_Popup", "PrecursorBuilder_Ency");

            PatchEncy(ency_alienSpam, modEncyPath_terminalInfo, "Popup_green", "GreenGlyph_Ency");

            PatchEncy(ency_eggRoom, modEncyPath_terminalInfo, "Popup_green", "GreenGlyph_Ency");

            PatchEncy(ency_aquariumSkeleton, modEncyPath_analysis);

            PatchEncy(ency_blackHole, modEncyPath_analysis);

            PatchEncy(ency_warpCannonTerminal, modEncyPath_tech, "WarpCannon_Popup", "WarpCannon_Ency");

            PatchEncy(ency_omegaCubeFabricator, modEncyPath_tech);

            PatchEncy(ency_buildablesonicdeterrent, modEncyPath_tech, "SonicDeterrent_Popup");
        }
        #endregion
    }
}