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
        [FormerlySerializedAs("inGameFading")] [SerializeField] private InGameFadingTransition inGameFadingTransition;

        private TutorialSession _tutorialSession; 
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
            _tutorialSession = Instantiate(tutorialLevel, transform);
        }

        public void CanEndTutor()
        {
            inGameFadingTransition.Fading(() =>
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
            _tutorialSession = Instantiate(GameManager.instance.gameSettings.levelList[_thisLevelId].tutorialLevel, transform);
            CanStartTutor();
        }
        
        
        private void EndTutorial(InputAction.CallbackContext obj)
        {
            GameManager.instance.TutorialEnded(_thisLevelId);
        }

        public void CanStartTutor()
        {
            _tutorialSession.StartTutor();
        }
    }
}
