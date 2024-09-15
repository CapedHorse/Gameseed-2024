using Core;
using UI.Panel;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    public class MainMenuUIManager : UIManager
    {
        [SerializeField] private PlayerInput mainMenuUIInput;
        
        [Header("Main Menu")]
        [SerializeField] private InputActionReference navigateMenuActionRef; 
        [SerializeField] private InputActionReference quitGameActionRef;

        //cache
        private MainMenuPanel _mainMenuPanel;
        public MainMenuPanel MainMenuPanel => _mainMenuPanel;
        

        public void ToggleInput(bool on)
        {
            if(on)
                mainMenuUIInput.ActivateInput();
            else
                mainMenuUIInput.DeactivateInput();
                
        }

        #region Main Menu Navigation

        public override void InitiatePanel()
        {
            base.InitiatePanel();
            ToggleInput(false);
            _mainMenuPanel = (MainMenuPanel) panelMap["MainMenu"];
            _mainMenuPanel.OnPanelBeginShow.AddListener(MenuPanelStartShown);
            _mainMenuPanel.OnPanelFinishShow.AddListener(MenuPanelFinishShown);
            _mainMenuPanel.ShowPanel();
        }

        private void MenuPanelStartShown()
        {
            _mainMenuPanel.OnPanelBeginShow.RemoveListener(MenuPanelStartShown);
        }

        private void MenuPanelFinishShown()
        {
            _mainMenuPanel.OnPanelFinishShow.RemoveListener(MenuPanelFinishShown);
            mainMenuUIInput.SwitchCurrentActionMap("MainMenu");
            BindInputMainMenu();
            ToggleInput(true);
        }

        private void BindInputMainMenu()
        {
            navigateMenuActionRef.action.started += OnMenuNavigated;
            quitGameActionRef.action.started += OnQuitPressed;
        }
        
        private void OnMenuNavigated(InputAction.CallbackContext obj)
        {
            float value = obj.ReadValue<float>();

            if (value > 0)
            {
                _mainMenuPanel.thisButtonParent.NavigateButton(true);
            }
            else
            {
                _mainMenuPanel.thisButtonParent.NavigateButton(false);
            }
        }

        private void OnQuitPressed(InputAction.CallbackContext obj)
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        #endregion

        #region Public Inspector Functions
        
        public void PlayGame()
        {
            ToggleInput(false);
            FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
            GameManager.instance.PlayGame();
        }

        public void OpenSettings()
        {
            ToggleInput(false);
            GameManager.instance.SettingsPanel.ShowPanel();
            GameManager.instance.SettingsPanel.OnPanelFinishHide.AddListener(ClosedSettings);
        }

        public void ClosedSettings()
        {
            ToggleInput(true);
            GameManager.instance.SettingsPanel.OnPanelFinishHide.RemoveListener(ClosedSettings);
        }

        #endregion

    }
}