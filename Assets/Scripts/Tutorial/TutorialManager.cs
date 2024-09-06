using System;
using Core;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager instance;

        [SerializeField] private GameObject endLevelGuideUI;
        [SerializeField] private PlayerInput tutorialPlayerInput;
        [SerializeField] private InputActionReference endTutorialReference;
        [SerializeField] private InGameFading inGameFading;

        private TutorialSession _tutorialSession;
        private TutorialSession _thisTutorialPrefab;
        private int _thisLevelId;

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
            endLevelGuideUI.SetActive(false);
            tutorialPlayerInput.DeactivateInput();
            endTutorialReference.action.started += EndTutorial;
        }

        public void LoadTutorialSession(TutorialSession tutorialLevel, int levelId)
        {
            _thisLevelId = levelId;
            _thisTutorialPrefab = tutorialLevel;
            _tutorialSession = Instantiate(tutorialLevel, transform);
        }

        public void CanEndTutor()
        {
            inGameFading.Fading(() =>
            {
                endLevelGuideUI.SetActive(true);
                ReloadSession();
            }, () =>
            {
                tutorialPlayerInput.SwitchCurrentActionMap("TutorialUIControl");
                tutorialPlayerInput.ActivateInput();    
            });
        }

        private void ReloadSession()
        {
            Destroy(_tutorialSession.gameObject);
            _tutorialSession = Instantiate(_thisTutorialPrefab, transform);
        }
        
        
        private void EndTutorial(InputAction.CallbackContext obj)
        {
            GameManager.instance.TutorialEnded(_thisLevelId);
        }

    }
}
