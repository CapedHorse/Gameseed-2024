using System;
using Audio;
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
        [SerializeField] Color kedutColor;
        [SerializeField] private Image timerBG;
        [SerializeField] private Image timerFill;
        [SerializeField] private Transform timerIcon;
        [SerializeField] private TextMeshProUGUI gameNameText;

        [Header("Transition")] 
        
        [SerializeField] private Image transitionBGImage;
        [SerializeField] private Sprite successBG, failedBG;
        [SerializeField] private Transform healthTransform;
        [SerializeField] private HealthPointUI[] hpUI;
        [Tooltip("0 is begin, 1 is success, 2 is retrying, 3 is failed, 4 is completed")] [SerializeField] private GameObject[] gameStateTextImage;
        [SerializeField] private TextMeshProUGUI gameCountdownText;

        [SerializeField] GameObject[] charactersMarks;
        
        [SerializeField] private string startingStr = "Starting In..";
        [SerializeField] private string nextGameStr = "Next Start In..";
        [SerializeField] private string retryingStr = "Retrying In..";
        [SerializeField] private string failedStr = "Back to Chapters In..";

        [SerializeField] private TextMeshProUGUI gameCountdownValueText;

        [Header("Audio")] 
        [SerializeField] private AudioPlayer sfxPlayerGameUI;
        [SerializeField] public AudioClip transitionInClip,
            transitionOutClip,
            transitionBGM,
            goodGameClip,
            badGameClip,
            healthDecreasedClip,
            gameNameShownClip,
            countdownClip;

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
            SetupPlayerHaveHeart(GameSessionManager.instance.haveHeart);
            SetupCharacterMark();
            
            inGameTransitionTransform.DOScale(0, 0).SetUpdate(true);
            inGameTransitionTransform.gameObject.SetActive(true);
            sfxPlayerGameUI.PlayClip(transitionInClip);
            inGameTransitionTransform.DOScale(1, transitionDuration).SetUpdate(true).onComplete = () =>
            {
                SetupPlayerHealth();
                
                if (gameStateType == GameStateType.Completed)
                {
                    ShowCompletedPanelAndInput();
                } else if (gameStateType == GameStateType.Failed)
                {
                    ShowFailedPanelAndInput();
                }
                
                transitionAction?.Invoke(); 
                sfxPlayerGameUI.PlayBGM(transitionBGM);
            };
        }

        private void ShowCompletedPanelAndInput()
        {
            healthTransform.gameObject.SetActive(false);
            //get current level's completed cutscene, get the sprite, show them here
        }

        private void ShowFailedPanelAndInput()
        {
            //show option to continue or back
        }

        public void TransitionOut(UnityAction transitionAction = null)
        {
            sfxPlayerGameUI.PlayClip(transitionOutClip);
            inGameTransitionTransform.DOScale(0, transitionDuration).SetUpdate(true).onComplete = () =>
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

            if (gameStateType == GameStateType.Completed || gameStateType == GameStateType.Success)
            {
                sfxPlayerGameUI.PlayClip(goodGameClip);
            }
            else if(gameStateType == GameStateType.Failed || gameStateType == GameStateType.Retry )
            {
                sfxPlayerGameUI.PlayClip(badGameClip);
            }
        }
        
        private void SetupGameProgress()
        {
            GameSessionManager gameSessionManager = GameSessionManager.instance;
            currentCompletedGameText.text = (gameSessionManager.CompletedGameSessionCount + 1).ToString();
            maxCompletedGameText.text = GameManager.instance.gameSettings
                .levelList[gameSessionManager.CurrentLevelId].gameSessions.Count.ToString();

        }

        public void SetupPlayerHaveHeart(bool have)
        {
            if (have)
            {
                foreach (var hp in hpUI)
                {
                    hp.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (var hp in hpUI)
                {
                    hp.gameObject.SetActive(false);
                } 
                hpUI[0].gameObject.SetActive(true);
            }
        }
        private void SetupPlayerHealth()
        {
            //health
            int currentPlayerHealth = GameSessionManager.instance.PlayerHeath;
            
            if (currentPlayerHealth < hpUI.Length)
            {
                sfxPlayerGameUI.PlayClip(healthDecreasedClip);
                hpUI[currentPlayerHealth].DecreaseHealth();
            }
        }

        private void SetupCharacterMark()
        {
            foreach(var charMark in charactersMarks){
                charMark.SetActive(false);
            }

            charactersMarks[GameSessionManager.instance.CurrentLevelId].
            SetActive(true);
            
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

        public void SetupTimer(float time)
        {
            if (time == 0)
            {
                timerSlider.gameObject.SetActive(false);
            }
            else
            {
                timerSlider.gameObject.SetActive(true);
                timerSlider.maxValue = time;
                timerSlider.value = timerSlider.maxValue;
                timerIcon.localScale = Vector2.one;
                timerFill.color = Color.white;
            }
        }

        public void CountdownUI(int countdownValue)
        {
            sfxPlayerGameUI.PlayClip(countdownClip);
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
            sfxPlayerGameUI.PlayClip(gameNameShownClip);
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
            timerBG.DOColor(kedutColor, 0.25f).SetUpdate(true).onComplete = () => timerBG.DOColor(Color.white, 0.25f);
            timerSlider.transform.DOPunchScale(Vector2.one * 0.01f, 0.5f, 1).SetUpdate(true);
        }

        public void ResetKedutTimer()
        {
            DOTween.Kill(timerBG);
            DOTween.Kill(timerSlider.transform);
            
            timerBG.color = Color.white;
            timerSlider.transform.localScale = Vector2.one;
        }

        private void OnDestroy()
        {
            DOTween.Kill(gameCountdownValueText.transform);
            DOTween.Kill(inGameTransitionTransform);
            DOTween.Kill(gameNameText.transform);
            DOTween.Kill(timerBG);
            DOTween.Kill(timerSlider.transform);
        }
    }
}