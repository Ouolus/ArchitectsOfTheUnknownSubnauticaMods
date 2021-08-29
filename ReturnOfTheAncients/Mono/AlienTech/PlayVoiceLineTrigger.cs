using Story;
using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class PlayVoiceLineTrigger : MonoBehaviour
    {
        public struct Data
        {
            public bool hasVoiceLine;
            public string subtitleKey;
            public string storyGoalKey;
            public AudioClip audioClip;
            public float delay;

            public Data(bool hasVoiceLine, string subtitleKey, string storyGoalKey, AudioClip audioClip, float delay)
            {
                this.hasVoiceLine = hasVoiceLine;
                this.subtitleKey = subtitleKey;
                this.storyGoalKey = storyGoalKey;
                this.audioClip = audioClip;
                this.delay = delay;
            }
        }

        public string subtitleKey;
        public string storyGoalKey;
        public AudioClip audioClip;
        public float delay;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.Equals(Player.main.gameObject))
            {
                if (StoryGoalManager.main.OnGoalComplete(storyGoalKey))
                {
                    Invoke(nameof(PlayVoiceLine), delay);
                }
            }
        }

        private void PlayVoiceLine()
        {
            CustomPDALinesManager.PlayPDAVoiceLine(audioClip, subtitleKey);
        }
    }
}
