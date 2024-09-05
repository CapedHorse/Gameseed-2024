using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Cutscene
{
    public class CutsceneUIManager : MonoBehaviour
    {
        public static CutsceneUIManager instance;

        [SerializeField] private PlayerInput cutscenePlayerInput;
        [SerializeField] private Transform introCutscenePanel;
        [SerializeField] private Transform outroCutscenePanel;
        [SerializeField] private InputActionReference nextPanelInputRef;

        private int currentPanelId;
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
            cutscenePanel.gameObject.SetActive(true);
            
            for (int i = 0; i < introCutscenePanel.childCount; i++)
            {
                cutscenePanel.GetChild(i).gameObject.SetActive(false);
            }

            cutscenePanel.GetChild(0).gameObject.SetActive(true);
            
            nextPanelInputRef.action.started += (InputAction.CallbackContext ctx) =>
            {
                NextPanel(cutscenePanel);
            };
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
            currentPanelId++;
            if (currentPanelId >= cutscenePanel.childCount)
            {
                GameManager.instance.CompleteIntroCutscene();
            }
            else
            {
                cutscenePanel.GetChild(currentPanelId).gameObject.SetActive(true);
            }
        }

        private void EndCutscene(Transform cutscenePanel)
        {
            currentPanelId = 0;
            cutscenePanel.gameObject.SetActive(false);
            SetCutsceneInputEnabled(false);
        }

        public void IntroCutscene(bool isStart)
        {
            if (isStart)
            {
                StartCutscene(introCutscenePanel);
            }
            else
            {
                EndCutscene(introCutscenePanel);
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