using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace UI.Panel
{
    public class MainMenuPanel : UIPanel
    {
        [SerializeField] private ButtonHighlight mainMenuButtonHighlight;
        [SerializeField] private Button[] mainMenuButtons;

        //caches
        private int currentButtonId;
        
        protected override void FinishShowPanel()
        {
            base.FinishShowPanel();
            
            SelectButton();
        }

        public void NavigateButton(bool toUp)
        {
            currentButtonId = Mathf.Clamp(toUp? currentButtonId - 1: currentButtonId + 1, 0, mainMenuButtons.Length - 1);

            if (toUp)
            {
                currentButtonId--;
                if (currentButtonId <= 0)
                {
                    currentButtonId = mainMenuButtons.Length -1;
                }
            }
            else
            {
                currentButtonId++;
                if (currentButtonId >= mainMenuButtons.Length)
                {
                    currentButtonId = 0;
                }
            }

            SelectButton();

        }

        private void SelectButton()
        {
            
            mainMenuButtons[currentButtonId].Select();
            mainMenuButtonHighlight.MoveSelection(mainMenuButtons[currentButtonId].transform);
        }

        public void ProceedMenu()
        {
            mainMenuButtons[currentButtonId].onClick.Invoke();
        }
    }
}