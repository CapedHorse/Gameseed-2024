using UnityEngine;
using UnityEngine.Events;

namespace UI.Panel
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panelParent;
        
        public UnityEvent OnPanelBeginShow, OnPanelFinishShow;
        public UnityEvent OnPanelBeginHide, OnPanelFinishHide;

        private bool _isOpened;
        public bool IsOpened => _isOpened;

        public virtual void ShowPanel()
        {
            OnPanelBeginShow.Invoke();    
            panelParent.SetActive(true);
            //Will have transition animation in the child
        }

        public virtual void FinishShowPanel()
        {
            OnPanelFinishShow.Invoke();
            _isOpened = true;
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
            _isOpened = false;
        }

       
    }
}