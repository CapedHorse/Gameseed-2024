using System;
using UI.Panel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace UI
{
    public class MainMenuUIManager : UIManager
    {
        [SerializeField] private PlayerInput mainMenuUIInput;
        
        [Header("Main Menu")]
        [SerializeField] private InputActionReference navigateMenuActionRef; 
        [SerializeField] private InputActionReference pressMenuActionRef, quitGameActionRef;
        
        
        
        private void Start()
        {
            InitiateMenuPanel();
        }

        private void InitiateMenuPanel()
        {
            panelMap["MainMenu"].OnPanelBeginShow.AddListener(MenuPanelStartShown);
            panelMap["MainMenu"].OnPanelFinishShow.AddListener(MenuPanelFinishShown);
            panelMap["MainMenu"].ShowPanel();
        }

        private void MenuPanelStartShown()
        {
            panelMap["MainMenu"].OnPanelBeginShow.RemoveListener(MenuPanelStartShown);
            mainMenuUIInput.SwitchCurrentActionMap("None");
        }

        private void MenuPanelFinishShown()
        {
            panelMap["MainMenu"].OnPanelFinishShow.RemoveListener(MenuPanelFinishShown);
            
            mainMenuUIInput.SwitchCurrentActionMap("Main");
            BindInputMainMenu();
        }
        
        private void BindInputMainMenu()
        {
            navigateMenuActionRef.action.performed += OnMenuNavigated;
            pressMenuActionRef.action.started += OnMenuPressed;
            quitGameActionRef.action.started += OnQuitPressed;
        }

        private void OnMenuNavigated(InputAction.CallbackContext obj)
        {
            MainMenuPanel mainMenuPanel = (MainMenuPanel) panelMap["MainMenu"];
            float value = obj.ReadValue<float>();

            if (value > 0)
            {
                mainMenuPanel.NavigateButton(true);
            }
            else
            {
                mainMenuPanel.NavigateButton(false);
            }
        }

        private void OnMenuPressed(InputAction.CallbackContext obj)
        {
            MainMenuPanel mainMenuPanel = (MainMenuPanel) panelMap["MainMenu"];
            mainMenuPanel.ProceedMenu();
        }

        private void ShowLevelSelection()
        {
            //Show level selectionpanel
        }

        private void OnQuitPressed(InputAction.CallbackContext obj)
        {
            MainMenuPanel mainMenuPanel = (MainMenuPanel) panelMap["MainMenu"];
            mainMenuPanel.ProceedMenu();
        }

        private void QuitPopUp()
        {
            //Show quit confirmation popup
        }
    }
}