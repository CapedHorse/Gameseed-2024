using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXOnePlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource oneShotSource;

    public void PlayClip(AudioClip clip)
    {
        oneShotSource.PlayOneShot(clip);
    }
}
