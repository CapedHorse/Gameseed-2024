using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Overlay
{
    public class FadingManager : MonoBehaviour
    {
        public static FadingManager instance;

        [SerializeField] private GameObject generalFadingPanel;
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
            generalFadingPanel.SetActive(true);
            fadeInAction?.Invoke();
        }

        public void FadeOut(UnityAction fadeOutAction = null)
        {
            generalFadingPanel.SetActive(false);
            fadeOutAction?.Invoke();
        }

        /*
        public async void Fading(UnityAction fadeInAction, UnityAction fadeOutAction)
        {
            await AsyncFadeIn();
            Task awaitingFadeInAction = new Task(() => fadeInAction?.Invoke());
            awaitingFadeInAction.Start();
            await awaitingFadeInAction;
            await AsyncFadeOut();
            fadeOutAction?.Invoke();
        }*/
        
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
