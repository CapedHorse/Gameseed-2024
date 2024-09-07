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

        [SerializeField] private GameObject generalFadingPanel;
        [SerializeField] private CanvasGroup generalFadingCG;
        [FormerlySerializedAs("FadingSpeed")] [SerializeField] private float fadingSpeed = 0.25f;
        [SerializeField] private GameObject completePanel;
        [SerializeField] private GameObject failedPanel;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        public void FadeIn(UnityAction fadeInAction = null)
        {
            generalFadingCG.DOFade(0, 0).SetUpdate(true);
            generalFadingPanel.SetActive(true);
            generalFadingCG.DOFade(1, fadingSpeed).SetUpdate(true).onComplete = () => fadeInAction?.Invoke();
            
        }

        public void FadeOut(UnityAction fadeOutAction = null, float fadeOutSpeed = 0)
        {
            generalFadingCG.DOFade(0,  fadeOutSpeed == 0 ? fadingSpeed : fadeOutSpeed).SetUpdate(true).onComplete = () =>
            {
                generalFadingPanel.SetActive(false);
                fadeOutAction?.Invoke();
            };
        }
        
        public void Fading(UnityAction fadeInAction, UnityAction fadeOutAction)
        {
            FadeIn(fadeInAction);
            FadeOut(fadeOutAction);
        }

        Task AsyncFadeIn()
        {
            Task fadingTask = new Task(() => FadeIn());
            fadingTask.Start();
            return fadingTask;
        }

        Task AsyncFadeOut()
        {
            Task fadingTask = new Task(() => FadeOut());
            fadingTask.Start();
            return fadingTask;
        }
    }
}
