using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace ArchitectsLibrary.CreatorKit.Patches
{
    [HarmonyPatch(typeof(MainMenuMusic))]
    public class MainMenuMusicPatches
    {
        [HarmonyPatch(nameof(MainMenuMusic.Start))]
        [HarmonyPostfix]
        public static void MainMenuMusic_Start_Postfix()
        {

        }
    }
}
