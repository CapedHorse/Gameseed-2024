using UnityEngine;
using UnityEngine.Events;

namespace UI.Panel
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panelParent;

        [HideInInspector]
        public UnityEvent OnPanelBeginShow, OnPanelFinishShow;
        [HideInInspector]
        public UnityEvent OnPanelBeginHide, OnPanelFinishHide;
        
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