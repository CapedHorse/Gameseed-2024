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
        
        [Header("Game Mode Selection")]
        [SerializeField] private InputActionReference navigateGameModeActionRef; 
        [SerializeField] private InputActionReference pressGameModeActionRef, backToMenuActionRef;
        
        
        //cache
        private MainMenuPanel mainMenuPanel;

        protected override void Start()
        {
            base.Start();
            mainMenuPanel = (MainMenuPanel) panelMap["MainMenu"];
            InitiateMenuPanel();
        }

        #region Main Menu Navigation

        private void InitiateMenuPanel()
        {
            mainMenuPanel.OnPanelBeginShow.AddListener(MenuPanelStartShown);
            mainMenuPanel.OnPanelFinishShow.AddListener(MenuPanelFinishShown);
            mainMenuPanel.ShowPanel();
        }

        private void MenuPanelStartShown()
        {
            mainMenuPanel.OnPanelBeginShow.RemoveListener(MenuPanelStartShown);
            mainMenuUIInput.DeactivateInput();
        }

        private void MenuPanelFinishShown()
        {
            mainMenuPanel.OnPanelFinishShow.RemoveListener(MenuPanelFinishShown);
            
            BindInputMainMenu();
            BindInputGameModeSelection();
            mainMenuUIInput.SwitchCurrentActionMap("MainMenu");
            mainMenuUIInput.ActivateInput();
        }

        private void BindInputMainMenu()
        {
            navigateMenuActionRef.action.performed += OnMenuNavigated;
            pressMenuActionRef.action.started += OnMenuPressed;
            quitGameActionRef.action.started += OnQuitPressed;
        }
        
        
        private void BindInputGameModeSelection()
        {
            navigateGameModeActionRef.action.performed += OnMenuNavigated;
            pressGameModeActionRef.action.started += OnMenuPressed;
            backToMenuActionRef.action.started += OnCloseGameMode;
        }

        private void OnMenuNavigated(InputAction.CallbackContext obj)
        {
            MainMenuPanel mainMenuPanel = (MainMenuPanel) panelMap["MainMenu"];
            float value = obj.ReadValue<float>();

            if (value > 0)
            {
                mainMenuPanel.GetCurrentButtonParent().NavigateButton(true);
            }
            else
            {
                mainMenuPanel.GetCurrentButtonParent().NavigateButton(false);
            }
        }

        private void OnMenuPressed(InputAction.CallbackContext obj)
        {
            mainMenuPanel.GetCurrentButtonParent().ProceedMenu();
        }

        private void OnQuitPressed(InputAction.CallbackContext obj)
        {
            mainMenuPanel.GetCurrentButtonParent().ProceedMenu();
            
        }

        private void OnCloseGameMode(InputAction.CallbackContext obj)
        {
            CloseGameModeSelection();
        }

        #endregion

        #region Game Mode Selection

        
        private void OnGameModeSelectionShown()
        {
            mainMenuUIInput.SwitchCurrentActionMap("GameModeMenu");
            mainMenuUIInput.ActivateInput();
        }

        private void OnGameModeSelectionClosed()
        {
            mainMenuUIInput.SwitchCurrentActionMap("MainMenu");
            mainMenuUIInput.ActivateInput();
        }


        #endregion
        

        #region Public Inspector Functions
        
        public void OpenGameModeSelection()
        {
            mainMenuUIInput.DeactivateInput();
            mainMenuPanel.ShowNextButtonParent();
            OnGameModeSelectionShown();
        }

        public void CloseGameModeSelection()
        {
            mainMenuUIInput.DeactivateInput();
            mainMenuPanel.ShowPrevButtonParent();
            OnGameModeSelectionClosed();
        }

        public void ProceedStoryMode()
        {
            //Have delay transition, clear UI, then call game manager to start story mode
        }

        public void ShowLevelSelection()
        {
            //Show level selectionpanel
        }

        public void ProceedFreeMode(int levelId)
        {
            //Check if level id unlocked, call game manager to start level that
            
            //If not, give feedback to level selection if locked yea
        }
        
        public void QuitPopUp()
        {
            //Show quit confirmation popup
            Application.Quit();
        }

        #endregion

    }
}