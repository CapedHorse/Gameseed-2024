using Audio;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Cutscene
{
    public class CutsceneUIManager : MonoBehaviour
    {
        public static CutsceneUIManager instance;

        [SerializeField] private PlayerInput cutscenePlayerInput;
        [SerializeField] private Transform[] introCutscenePanels;
        [SerializeField] private Transform outroCutscenePanel;
        [SerializeField] private GameObject nextControlGuide;
        [SerializeField] private InputActionReference nextPanelInputRef;

        private int currentPanelId;
        private Transform _currentCutscenePanel;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        private void Start()
        {
            SetCutsceneInputEnabled(false);
        }

        private void StartCutscene(Transform cutscenePanel)
        {
            AudioBGMManager.instance.StopAnyBGM();
            cutscenePanel.gameObject.SetActive(true);
            nextControlGuide.SetActive(true);
            for (int i = 0; i < cutscenePanel.childCount; i++)
            {
                cutscenePanel.GetChild(i).gameObject.SetActive(false);
            }

            cutscenePanel.GetChild(0).gameObject.SetActive(true);
            cutscenePanel.GetChild(0).gameObject.GetComponent<CutscenePanel>().Show();
            
            _currentCutscenePanel = cutscenePanel;
            nextPanelInputRef.action.started += NextPanelInput;
        }

        private void NextPanelInput(InputAction.CallbackContext obj)
        {
            NextPanel(_currentCutscenePanel);
        }

        public void SetCutsceneInputEnabled(bool enabled)
        {
            if(enabled)
                cutscenePlayerInput.ActivateInput();
            else
                cutscenePlayerInput.DeactivateInput();
        }

        private void NextPanel(Transform cutscenePanel)
        {
            Debug.Log("Current panel id: "+ currentPanelId);
            currentPanelId++;
            if (currentPanelId >= cutscenePanel.childCount)
            {
                nextPanelInputRef.action.started -= NextPanelInput;
                _currentCutscenePanel = null;
                if (cutscenePanel == outroCutscenePanel)
                {
                    GameManager.instance.CompleteOutroCutscene();
                }
                else
                {
                    GameManager.instance.CompleteIntroCutscene();
                }
            }
            else
            {
                cutscenePanel.GetChild(currentPanelId).gameObject.SetActive(true);
                cutscenePanel.GetChild(currentPanelId).gameObject.GetComponent<CutscenePanel>().Show();
            }
        }

        private void EndCutscene(Transform cutscenePanel)
        {
            currentPanelId = 0;
            cutscenePanel.gameObject.SetActive(false);
            nextControlGuide.SetActive(false);
            SetCutsceneInputEnabled(false);
        }

        public void IntroCutscene(int cutsceneId, bool isStart)
        {
            if (isStart)
            {
                StartCutscene(introCutscenePanels[cutsceneId]);
            }
            else
            {
                EndCutscene(introCutscenePanels[cutsceneId]);
            }
        }

        public void OutroCutscene(bool isStart)
        {
            if (isStart)
            {
                StartCutscene(outroCutscenePanel);
            }
            else
            {
                EndCutscene(outroCutscenePanel);
            }
        }
    }
}