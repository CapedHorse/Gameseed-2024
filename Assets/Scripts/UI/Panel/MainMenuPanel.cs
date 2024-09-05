using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace UI.Panel
{
    public class MainMenuPanel : UIPanel
    {
        [SerializeField] private Button playGameBtn, settingsBtn, quitBtn;

        public override void ShowPanel()
        {
            base.ShowPanel();
            //Have animation transition first
            FinishShowPanel();
        }

        protected override void FinishShowPanel()
        {
            base.FinishShowPanel();
        }
        
        
        
    }
}