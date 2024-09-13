using UnityEngine;

namespace Audio
{
    public class AudioBGMManager : MonoBehaviour
    {
        public static AudioBGMManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        [SerializeField]
        private AudioSource bgmSource;

        public void PlayOneShotBGM(AudioClip clip)
        {
            bgmSource.PlayOneShot(clip);
        }

        public void PlayBGM(AudioClip bgmClip)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }

        public void StopAnyBGM()
        {
            bgmSource.Stop();
        }
    }
}