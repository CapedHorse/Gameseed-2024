using UnityEngine;

namespace Audio
{
    public class AudioFXOneShotPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource oneShotSource;

        public void PlayClip(AudioClip clip)
        {
            oneShotSource.PlayOneShot(clip);
        }
    }
}
