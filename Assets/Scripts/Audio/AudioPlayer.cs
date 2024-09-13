using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource oneShotSource;
        
        public void PlayClip(AudioClip clip)
        {
            oneShotSource.PlayOneShot(clip);
        }

        public void PlayBGM(AudioClip clip)
        {
            AudioBGMManager.instance.PlayBGM(clip);
        }
    }
}
