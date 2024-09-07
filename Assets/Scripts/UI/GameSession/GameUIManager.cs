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
        
        [Header("HUD")]
        [SerializeField] private Slider timerSlider;
        [SerializeField] private Transform timerIcon;
        [SerializeField] private TextMeshProUGUI gameNameText;
        
        [Header("Transition")]
        [SerializeField] private HealthPointUI[] hpUI;
        [Tooltip("0 is begin, 1 is success, 2 is failed")] [SerializeField] private GameObject[] gameStateTextImage;
        [SerializeField] private TextMeshProUGUI scoreCompletitionText;
        [SerializeField] private TextMeshProUGUI gameCountdownText;
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

        public void Transition(GameStateType gameStateType, UnityAction transitionAction, UnityAction afterTransitionAction)
        {
            ResetGameStateUI();
            gameStateTextImage[(int)gameStateType].SetActive(true);

            int currentPlayerHealth = GameSessionManager.instance.PlayerHeath;
            if (currentPlayerHealth < hpUI.Length)
            {
                hpUI[currentPlayerHealth].DecreaseHealth();
            }
            
            inGameTransitionTransform.DOMoveY(1080, 0);
            inGameTransitionTransform.gameObject.SetActive(true);
        }
    }
}