using Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI.Cutscene
{
    public class CutscenePanel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [FormerlySerializedAs("cutsceneAudio")] [SerializeField] AudioClip cutsceneClip;
        [SerializeField] private bool shaking;
        public UnityEvent onCutsceneShown;
    
        public void Show()
        {
            if(shaking)
                transform.DOShakeRotation(0.25f, Vector3.one * 10, 150);
            
            animator.SetTrigger("Show");
            
            AudioBGMManager.instance.PlayOneShotBGM(cutsceneClip);
            
            onCutsceneShown.Invoke();
        }

    }
}
