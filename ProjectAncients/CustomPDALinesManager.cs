using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;
using SMLHelper.V2.Handlers;

namespace ProjectAncients
{
    public static class CustomPDALinesManager
    {
        static readonly List<string> registeredSubtitleKeys = new List<string>();

        public static void PlayPDAVoiceLine(AudioClip audioClip, string subtitleKey, string subtitleDisplayText)
        {
            if (!registeredSubtitleKeys.Contains(subtitleKey))
            {
                LanguageHandler.SetLanguageLine(subtitleKey, subtitleDisplayText);
                registeredSubtitleKeys.Add(subtitleKey);
            }
            GameObject obj = new GameObject("PDA Line Instance");
            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = audioClip;
            source.volume = ECCHelpers.GetECCVolume();
            source.Play();
            GameObject.Destroy(obj, audioClip.length);
            Subtitles.main.Add(subtitleKey);
        }

        public static void PlayPDAVoiceLineFMOD(string eventPath, string subtitleKey, string subtitleDisplayText)
        {
            if (!registeredSubtitleKeys.Contains(subtitleKey))
            {
                LanguageHandler.SetLanguageLine(subtitleKey, subtitleDisplayText);
                registeredSubtitleKeys.Add(subtitleKey);
            }
            FMODAsset soundAsset = ScriptableObject.CreateInstance<FMODAsset>();
            soundAsset.path = eventPath;
            source.volume = ECCHelpers.GetECCVolume();
            source.Play();
            GameObject.Destroy(obj, audioClip.length);
            Subtitles.main.Add(subtitleKey);
        }
    }
}
