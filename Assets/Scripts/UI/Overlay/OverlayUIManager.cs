using UnityEngine;
using UnityEngine.Events;

namespace UI.Overlay
{
    public class OverlayUIManager : MonoBehaviour
    {
        public static OverlayUIManager instance;

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

        public void FadeIn(UnityAction fadeInAction)
        {
            
        }
    }
}
