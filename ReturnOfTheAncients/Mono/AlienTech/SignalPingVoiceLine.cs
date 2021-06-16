using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Story;

namespace RotA.Mono.AlienTech
{
    public class SignalPingVoiceLine : MonoBehaviour
    {
        public Data data;

        public struct Data
        {
            public bool hasVoiceLine;
            public string subtitleKey;
            public string storyGoalKey;
            public string subtitleDisplayText;
            public AudioClip audioClip;
            public float delay;

            public Data(bool hasVoiceLine, string subtitleKey, string storyGoalKey, string subtitleDisplayText, AudioClip audioClip, float delay)
            {
                this.hasVoiceLine = hasVoiceLine;
                this.subtitleKey = subtitleKey;
                this.storyGoalKey = storyGoalKey;
                this.subtitleDisplayText = subtitleDisplayText;
                this.audioClip = audioClip;
                this.delay = delay;
            }

            public Data(Data data)
            {
                this.hasVoiceLine = data.hasVoiceLine;
                this.subtitleKey = data.subtitleKey;
                this.storyGoalKey = data.storyGoalKey;
                this.subtitleDisplayText = data.subtitleDisplayText;
                this.audioClip = data.audioClip;
                this.delay = data.delay;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.Equals(Player.main.gameObject))
            {
                if (!data.hasVoiceLine)
                {
                    return;
                }
                if (StoryGoalManager.main.OnGoalComplete(data.subtitleKey))
                {
                    Invoke(nameof(PlayVoiceLine), data.delay);
                }
            }
        }

        private void PlayVoiceLine()
        {
            CustomPDALinesManager.PlayPDAVoiceLine(data.audioClip, data.subtitleKey, data.subtitleDisplayText);
        }
    }
}
