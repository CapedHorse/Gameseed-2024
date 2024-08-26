using UnityEngine;
using UnityEngine.Events;

namespace UI.Panel
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panelParent;

        public UnityEvent OnPanelBeginShow, OnPanelFinishShow;
        public UnityEvent OnPanelBeginHide, OnPanelFinishHide;

        private void Start()
        {
            panelParent.SetActive(false);
        }

        public virtual void ShowPanel()
        {
            OnPanelBeginShow.Invoke();    
            panelParent.SetActive(true);
            //Will have transition animation in the child
        }

        protected virtual void FinishShowPanel()
        {
            OnPanelFinishShow.Invoke();
        }
        
        public virtual void HidePanel()
        {
            OnPanelBeginHide.Invoke();
            //Will have transition animation in the child
        }

        protected virtual void FinishHidePanel()
        {
            panelParent.SetActive(false);
            OnPanelFinishHide.Invoke();
        }
    }
}