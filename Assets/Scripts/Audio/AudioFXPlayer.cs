using UnityEngine;

namespace Audio
{
    public class AudioFXPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource oneShotSource;

        public void PlayClip(AudioClip clip)
        {
            oneShotSource.PlayOneShot(clip);
        }

        public void PlayBGM(AudioClip bgmClip)
        {
            AudioBGMManager.instance.PlayBGM(bgmClip);
        }
    }
}
