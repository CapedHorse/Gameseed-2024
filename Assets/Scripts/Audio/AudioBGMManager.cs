using System;
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

        [SerializeField] private AudioClip mainMenuClip, levelSelectionClip;

        public void PlayMainMenuMusic()
        {
            bgmSource.clip = mainMenuClip;
            bgmSource.Play();
        }

        public void PlayLevelSelectClip()
        {
            bgmSource.clip = levelSelectionClip;
            bgmSource.Play();
        }
        
        public void PlayTutorialClip(int lvlId)
        {
            
        }

        public void PlayTransitionClip(int lvlId)
        {
            
        }

        public void PlayGameMusicClip(int lvlId, int gameSessionId)
        {
            
        }

        public void PlayBGM(AudioClip bgmClip)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }
    }
}