using System;
using Core;
using DG.Tweening;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

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
        [SerializeField] private Image timerBG;
        [SerializeField] private Image timerFill;
        [SerializeField] private Transform timerIcon;
        [SerializeField] private TextMeshProUGUI gameNameText;

        [Header("Transition")] 
        
        [SerializeField] private Image transitionBGImage;
        [SerializeField] private Sprite successBG, failedBG;
        [SerializeField] private HealthPointUI[] hpUI;
        [Tooltip("0 is begin, 1 is success, 2 is retrying, 3 is completed, 4 is failed")] [SerializeField] private GameObject[] gameStateTextImage;
        [SerializeField] private TextMeshProUGUI gameCountdownText;
        
        [SerializeField] private string startingStr = "Starting In..";
        [SerializeField] private string nextGameStr = "Next Start In..";
        [SerializeField] private string retryingStr = "Retrying In..";
        [SerializeField] private string failedStr = "Back to Chapters In..";

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

        public void TransitionIn(GameStateType gameStateType, UnityAction transitionAction = null)
        {
            SetupBG(gameStateType);
            SetupGameStateUI(gameStateType);
            SetupGameProgress();
            SetupCountdown(gameStateType);
            SetupTimer();
            
            inGameTransitionTransform.DOLocalMoveY(1080, 0).SetUpdate(true);
            inGameTransitionTransform.gameObject.SetActive(true);
            inGameTransitionTransform.DOLocalMoveY(0, transitionDuration).SetUpdate(true).onComplete = () =>
            {
                SetupPlayerHealth();
                transitionAction?.Invoke();
            };
        }

        public void TransitionOut(UnityAction transitionAction = null)
        {
            inGameTransitionTransform.DOLocalMoveY(1080, transitionDuration).SetUpdate(true).onComplete = () =>
            {
                inGameTransitionTransform.gameObject.SetActive(false);
                transitionAction?.Invoke();
            };
        }

        private void SetupBG(GameStateType gameStateType)
        {
            switch (gameStateType)
            {
                case GameStateType.Begin:
                    
                    break;
                case GameStateType.Success:
                    transitionBGImage.sprite = successBG;
                    break;
                case GameStateType.Retry:
                    transitionBGImage.sprite = failedBG;
                    break;
                case GameStateType.Failed:
                    transitionBGImage.sprite = failedBG;
                    break;
                case GameStateType.Completed:
                    transitionBGImage.sprite = successBG;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameStateType), gameStateType, null);
            }
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
            currentCompletedGameText.text = (gameSessionManager.CompletedGameSessionCount + 1).ToString();
            maxCompletedGameText.text = GameManager.instance.gameSettings
                .levelList[gameSessionManager.CurrentLevelId].gameSessionNames.Count.ToString();

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
            gameCountdownText.gameObject.SetActive(true);
            gameCountdownValueText.gameObject.SetActive(true);
            //countdown
            switch (gameStateType)
            {
                case GameStateType.Begin:
                    gameCountdownText.text = startingStr;
                    break;
                case GameStateType.Success:
                    gameCountdownText.text = nextGameStr;
                    break;
                case GameStateType.Retry:
                    gameCountdownText.text = retryingStr;
                    break;
                case GameStateType.Failed:
                    gameCountdownText.gameObject.SetActive(false);
                    gameCountdownValueText.gameObject.SetActive(false);
                    break;
                case GameStateType.Completed:
                    gameCountdownText.gameObject.SetActive(false);
                    gameCountdownValueText.gameObject.SetActive(false);
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

        public void CountdownUI(int countdownValue)
        {
            gameCountdownValueText.text = countdownValue.ToString();
            gameCountdownValueText.transform.DOPunchScale(Vector2.one * 0.25f, 0.25f, 1).SetUpdate(true);
            
        }
        
        public void HideGameName()
        {
            gameNameText.transform.DOScale(0, 0.25f).SetUpdate(true).onComplete = () =>
            {
                gameNameText.gameObject.SetActive(false);
            };
        }

        public void ShowGameName(string gameName)
        {
            gameNameText.text = gameName;
            gameNameText.transform.DOScale(0, 0);
            gameNameText.gameObject.SetActive(true);
            gameNameText.transform.DOScale(1.1f, 0.1f).SetUpdate(true).onComplete = () =>
            {
                gameNameText.transform.DOScale(1, 0.05f).SetUpdate(true);
            };
        }

        public void SetTimerValue(float currentPlayTime)
        {
            timerSlider.value = currentPlayTime;
        }

        public void KedutTimer()
        {
            timerBG.DOColor(Color.red, 0.25f).onComplete = () => timerBG.DOColor(Color.white, 0.25f);
            timerSlider.transform.DOPunchScale(Vector2.one * 0.01f, 0.5f, 1);
        }
    }
}