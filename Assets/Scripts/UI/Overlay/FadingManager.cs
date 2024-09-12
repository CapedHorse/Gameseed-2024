using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI.Overlay
{
    public class FadingManager : MonoBehaviour
    {
        public static FadingManager instance;

        [SerializeField] private bool useFading = true;
        [FormerlySerializedAs("fadingFG")] [SerializeField] private CanvasGroup fadingCG;
        [SerializeField] private GameObject generalFadingPanel;
        [SerializeField] private Transform fadingMaskTransform;
        [SerializeField] private float fadingSpeed = 0.25f;
        [SerializeField] private float cutoutSpeed = 0.5f;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        public void FadeIn(bool isFading, UnityAction fadeInAction = null)
        {
            if (isFading)
            {
                fadingCG.DOFade(0, 0).SetUpdate(true);
                fadingCG.gameObject.SetActive(true);
                fadingCG.DOFade(1, fadingSpeed).SetUpdate(true).onComplete = () => fadeInAction?.Invoke();
            }
            else
            {
                generalFadingPanel.SetActive(true);
                fadingMaskTransform.gameObject.SetActive(true);
                fadingMaskTransform.DOScale(2.5f, cutoutSpeed).SetUpdate(true).onComplete = () => fadeInAction?.Invoke();    
            }
        }

        public void FadeOut(bool isFading, UnityAction fadeOutAction = null, float fadeOutSpeed = 0)
        {
            if (isFading)
            {
                fadingCG.DOFade(0, fadeOutSpeed == 0 ? fadingSpeed : fadeOutSpeed).SetUpdate(true).onComplete = () =>
                {
                    fadingCG.gameObject.SetActive(false);
                    fadeOutAction?.Invoke();
                };
            }
            else
            {
                fadingMaskTransform.DOScale(0,  fadeOutSpeed == 0 ? cutoutSpeed : fadeOutSpeed).SetUpdate(true).onComplete = () =>
                {
                    generalFadingPanel.SetActive(false);
                    fadingMaskTransform.gameObject.SetActive(false);
                    fadeOutAction?.Invoke();
                };    
            }
        }
    }
}
