using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class InGameFadingTransition : MonoBehaviour
    {
        [SerializeField] private CanvasGroup inGameFadingCG;
        [SerializeField] private float fadingDuration;


        public void Fading(UnityAction fadingAction, UnityAction afterFadeAction)
        {
            inGameFadingCG.DOFade(1, fadingDuration).onComplete = () =>
            {
                fadingAction?.Invoke();
                inGameFadingCG.DOFade(0, fadingDuration).onComplete = () =>
                {
                    afterFadeAction?.Invoke();
                };
            };
        }
    }
}