using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace UI.Panel
{
    public class MainMenuPanel : UIPanel
    {
        [SerializeField] private ButtonParent[] buttonParents;
        

        //caches
        
        private int currentButtonParentsId = -1;

        public override void ShowPanel()
        {
            base.ShowPanel();
            //Have animation transition first
            ShowNextButtonParent();
            FinishShowPanel();
        }

        protected override void FinishShowPanel()
        {
            base.FinishShowPanel();
        }
        
        public void ShowNextButtonParent()
        {
            //Hide previous if not the first button parent to show at start
            if (currentButtonParentsId != -1)
            {
                GetCurrentButtonParent().HideButtonParent();
            }
            
            currentButtonParentsId++;

            if (currentButtonParentsId >= buttonParents.Length)
            {
                currentButtonParentsId = 0;
            }
            
            GetCurrentButtonParent().ShowButtonParent();
        }

        public void ShowPrevButtonParent()
        {
            GetCurrentButtonParent().HideButtonParent();

            currentButtonParentsId--;

            if (currentButtonParentsId < 0)
            {
                currentButtonParentsId = buttonParents.Length - 1;
            }
            
            GetCurrentButtonParent().ShowButtonParent();
        }

        public ButtonParent GetCurrentButtonParent()
        {
            return buttonParents[currentButtonParentsId];
        }

        
    }
}