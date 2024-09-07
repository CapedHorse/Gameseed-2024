using System;
using Core;
using DG.Tweening;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.GameSession
{
    public class GameUIManager : MonoBehaviour
    {
        public static GameUIManager instance;
        
        [SerializeField] private Transform inGameTransitionTransform;
        [SerializeField] private float transitionDuration = 0.25f;
        
        [Header("HUD")]
        [SerializeField] private TextMeshProUGUI currentCompletedGameText;
        [SerializeField] private TextMeshProUGUI maxCompletedGameText;
        [SerializeField] private Slider timerSlider;
        [SerializeField] private Image timerFill;
        [SerializeField] private Transform timerIcon;
        [SerializeField] private TextMeshProUGUI gameNameText;
        
        [Header("Transition")]
        [SerializeField] private HealthPointUI[] hpUI;
        [Tooltip("0 is begin, 1 is success, 2 is failed")] [SerializeField] private GameObject[] gameStateTextImage;
        // [SerializeField] private TextMeshProUGUI scoreCompletitionText;
        [SerializeField] private TextMeshProUGUI gameCountdownText;
        
        [SerializeField] private string startingStr = "Starting In..";
        [SerializeField] private string nextGameStr = "Next Start In..";
        [SerializeField] private string retryingStr = "Retrying In..";
        
        [SerializeField] private TextMeshProUGUI gameCountdownValueText;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        private void ResetGameStateUI()
        {
            foreach (var gameStateText in gameStateTextImage)
            {
                if (gameStateText.activeSelf)
                {
                    gameStateText.SetActive(false);
                }
            }
        }

        public void Transition(GameStateType gameStateType, UnityAction transitionAction = null, UnityAction afterTransitionAction = null)
        {
            SetupGameStateUI(gameStateType);

            SetupGameProgress();

            SetupPlayerHealth();
            
            SetupCountdown(gameStateType);

            SetupTimer();


            inGameTransitionTransform.DOMoveY(1080, 0);
            inGameTransitionTransform.gameObject.SetActive(true);
            inGameTransitionTransform.DOMoveY(0, transitionDuration).onComplete = () =>
            {
                transitionAction?.Invoke();
                
            };
        }

        private void SetupGameStateUI(GameStateType gameStateType)
        {
            //gamestate
            ResetGameStateUI();
            gameStateTextImage[(int)gameStateType].SetActive(true);
        }
        
        private void SetupGameProgress()
        {
            GameSessionManager gameSessionManager = GameSessionManager.instance;
            currentCompletedGameText.text = (gameSessionManager.CompletedGameSession + 1).ToString();
            maxCompletedGameText.text = GameManager.instance.gameSettings
                .levelList[gameSessionManager.CurrentLevelId].gameSessionPrefabList.ToString();

        }

        private void SetupPlayerHealth()
        {
            //health
            int currentPlayerHealth = GameSessionManager.instance.PlayerHeath;
            if (currentPlayerHealth < hpUI.Length)
            {
                hpUI[currentPlayerHealth].DecreaseHealth();
            }
        }

        private void SetupCountdown(GameStateType gameStateType)
        {
            //countdown
            switch (gameStateType)
            {
                case GameStateType.Begin:
                    gameCountdownText.text = startingStr;
                    break;
                case GameStateType.Success:
                    gameCountdownText.text = nextGameStr;
                    break;
                case GameStateType.Failed:
                    gameCountdownText.text = retryingStr;
                    break;
                case GameStateType.Completed:
                    gameCountdownText.gameObject.SetActive(false);
                    break;
                default:
                    gameCountdownText.text = "...";
                    break;
            }

            gameCountdownValueText.text = GameManager.instance.gameSettings.countDownTime.ToString();
        }

        private void SetupTimer()
        {
            timerSlider.maxValue = GameManager.instance.gameSettings.timeEachPlay;
            timerSlider.value = timerSlider.maxValue;
            timerIcon.localScale = Vector2.one;
            timerFill.color = Color.white;
        }

    }
}