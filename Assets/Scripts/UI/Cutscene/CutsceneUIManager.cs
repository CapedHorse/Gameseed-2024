using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Cutscene
{
    public class CutsceneUIManager : MonoBehaviour
    {
        public static CutsceneUIManager instance;

        [SerializeField] private Transform cutscenePanelParent;

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
            for (int i = 0; i < cutscenePanelParent.childCount; i++)
            {
                cutscenePanelParent.GetChild(i).gameObject.SetActive(false);
            }

            cutscenePanelParent.GetChild(0).gameObject.SetActive(true);
            
            nextPanelInputRef.action.started += NextPanel;
        }

        private void NextPanel(InputAction.CallbackContext obj)
        {
            currentPanelId++;
            if (currentPanelId >= cutscenePanelParent.childCount)
            {
                GameManager.instance.CompleteCutscene();
            }
            else
            {
                cutscenePanelParent.GetChild(currentPanelId).gameObject.SetActive(true);
            }
        }
    }
}