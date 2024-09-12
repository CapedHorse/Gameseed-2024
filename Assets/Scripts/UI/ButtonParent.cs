using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ButtonParent : MonoBehaviour
    {
        
        [SerializeField] private Button[] mainMenuButtons;
        private int currentButtonId;

        public UnityEvent<Button> onSelectedButton;

        public Button GetCurrentSelectedButton()
        {
            return mainMenuButtons[currentButtonId];
        }
        
        public void NavigateButton(bool toUp)
        {
            currentButtonId = Mathf.Clamp(toUp? currentButtonId - 1: currentButtonId + 1, 0, mainMenuButtons.Length - 1);

            if (toUp)
            {
                PrevButtonId();

                if (!mainMenuButtons[currentButtonId].interactable)
                {
                    PrevButtonId();
                }
            }
            else
            {
                NextButtonId();

                if (!mainMenuButtons[currentButtonId].interactable)
                {
                    NextButtonId();
                }
            }

            SelectButton();

        }

        private void NextButtonId()
        {
            currentButtonId++;
            if (currentButtonId >= mainMenuButtons.Length)
            {
                currentButtonId = 0;
            }
        }

        private void PrevButtonId()
        {
            currentButtonId--;
            if (currentButtonId < 0)
            {
                currentButtonId = mainMenuButtons.Length - 1;
            }
        }

        private void SelectButton()
        {
            
            mainMenuButtons[currentButtonId].Select();
            onSelectedButton.Invoke(GetCurrentSelectedButton());
        }

    }
}
