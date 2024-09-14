using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource oneShotSource;
        
        public void PlayClip(AudioClip clip)
        {
            if(clip != null)
                oneShotSource.PlayOneShot(clip);
        }

        public void PlayBGM(AudioClip clip)
        {
            if(clip != null)
                AudioBGMManager.instance.PlayBGM(clip);
        }

        public void StopBGM()
        {
            AudioBGMManager.instance.StopAnyBGM();
        }
    }
}
