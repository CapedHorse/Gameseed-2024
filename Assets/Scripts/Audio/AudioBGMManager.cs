using UnityEngine;
using UnityEngine.Audio;

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
        private AudioSource bgmSource, nearestSFXSource;
        
        [SerializeField] AudioMixerGroup bgmAudioMixer, sfxAudioMixer;
        public AudioMixerGroup BGMAudiMixer => bgmAudioMixer;
        public AudioMixerGroup SFXAudiMixer => sfxAudioMixer;

        public void PlayOneShotBGM(AudioClip clip)
        {
            if (clip == null)
                return;
            bgmSource.PlayOneShot(clip);
        }

        public void PlayBGM(AudioClip bgmClip)
        {
            if (bgmClip == null)
                return;
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }

        public void StopAnyBGM()
        {
            bgmSource.Stop();
            nearestSFXSource.Stop();
        }

        public void SetSFXVolume(float value)
        {
            sfxAudioMixer.audioMixer.SetFloat("SFX Volume", value);
        }

        public void SetBGMVolume(float value)
        {
            bgmAudioMixer.audioMixer.SetFloat("BGM Volume", value);
        }
    }
}