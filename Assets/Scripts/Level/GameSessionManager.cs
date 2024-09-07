using System;
using System.Collections;
using Core;
using DG.Tweening;
using UI.GameSession;
using UI.Overlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Level
{
    public class GameSessionManager : MonoBehaviour
    {
        public static GameSessionManager instance;

        [SerializeField]
        private GameUIManager gameUIManager;
        private GameSession _currentGameSession;
        private string _currentGameSessionLevelId;
        private string _currentGameSessionName;
        
        public int CurrentLevelId => _currentGameLevelId;
        private int _currentGameLevelId;
        
        public int CompletedGameSessionCount => _completedGameSessionCount;
        private int _completedGameSessionCount = 0;
        
        public bool StartedGame => _startGame;
        private bool _startGame;

        public int PlayerHeath => _playerHealth;
        private int _playerHealth;
        
        
        private float _currentPlayTime;
        private float _targetPlayTime;

        #region Unity Life Cycle
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
            _startGame = false;
        }
        
        private void Update()
        {
            if (!_startGame)
                return;

            _currentPlayTime -= Time.deltaTime;
            gameUIManager.SetTimerValue(_currentPlayTime);
            if (_currentPlayTime <= 0)
            {
                _currentGameSession.TimerRunsOut();
            }
        }
#endregion

        #region New Level Session

        public void InitializeGameSession(int levelId)
        {
            _currentGameLevelId = levelId;
            
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            
            _playerHealth = gameSettings.playerHealthEachLevel;
            _currentGameSessionName = gameSettingsLevel.gameSessionNames[_completedGameSessionCount];
            
            GameManager.instance.FreezeTime();

            SceneManager.sceneLoaded += OnNewGameSceneLoaded;
            SceneManager.LoadScene(_currentGameSessionName, LoadSceneMode.Additive);
        }
        
        private void OnNewGameSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnNewGameSceneLoaded;
            _currentGameSession = FindObjectOfType<GameSession>();
            gameUIManager.TransitionIn(GameStateType.Begin, () =>
            {
                FadingManager.instance.FadeOut(() =>
                {
                    StartCoroutine(PrepareStartGame());
                }, 0.5f);
            });
        }

        #endregion

        #region Starting Preparation

        private IEnumerator PrepareStartGame()
        {
            gameUIManager.ShowGameName(_currentGameSessionName);
            int countdownTime = GameManager.instance.gameSettings.countDownTime;
            for (int i = countdownTime; i > 0; i--)
            {
                gameUIManager.CountdownUI(i);
                yield return new WaitForSecondsRealtime(1f);
            }
            
            gameUIManager.TransitionOut(StartingGame);
        }

        private void StartingGame()
        {
            DOVirtual.DelayedCall(GameManager.instance.gameSettings.delayBeforeRealPlay, () =>
            {
                gameUIManager.HideGameName();
                _currentGameSession.StartGame();
                _currentPlayTime = GameManager.instance.gameSettings.timeEachPlay;
                _startGame = true;
                GameManager.instance.UnfreezeTime();
            });
        }

        #endregion

        #region Game Success

        void LoadNextGame()
        {
            SceneManager.sceneUnloaded += OnCurrentGameUnloaded;
            SceneManager.UnloadSceneAsync(_currentGameSessionName);
        }

        private void OnCurrentGameUnloaded(Scene arg0)
        {
            SceneManager.sceneUnloaded -= OnCurrentGameUnloaded;
            
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            _currentGameSessionName = gameSettingsLevel.gameSessionNames[_completedGameSessionCount];
            
            SceneManager.sceneLoaded += OnNextGameLoaded;
            SceneManager.LoadScene(_currentGameSessionName, LoadSceneMode.Additive);
        }

        private void OnNextGameLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnNextGameLoaded;
            StartCoroutine(PrepareStartGame());
        }


        #endregion

        #region Game Failed

        private void ReloadCurrentGame()
        {
            SceneManager.sceneUnloaded += OnFailedGameSceneUnloaded;
            SceneManager.UnloadSceneAsync(_currentGameSessionName);
        }

        private void OnFailedGameSceneUnloaded(Scene arg0)
        {
            SceneManager.sceneUnloaded -= OnFailedGameSceneUnloaded;
            SceneManager.sceneLoaded += OnCurrentFailedGameReloaded;
            SceneManager.LoadScene(_currentGameSessionName, LoadSceneMode.Additive);
        }

        private void OnCurrentFailedGameReloaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnCurrentFailedGameReloaded;
            StartCoroutine(PrepareStartGame());
        }

        #endregion
        public void EndedGameSession(GameStateType type)
        {
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            GameManager.instance.FreezeTime();
            _startGame = false;
            
            DOVirtual.DelayedCall(GameManager.instance.gameSettings.delayWhenShowingState, () =>
            {
                switch (type)
                {
                    case GameStateType.Begin:
                        break;
                    case GameStateType.Success:
                    {
                        _completedGameSessionCount++;
                        if (_completedGameSessionCount >= gameSettingsLevel.gameSessionNames.Count)
                        {
                            //Do tell that you finished the level
                        }
                        else
                        {
                            gameUIManager.TransitionIn(GameStateType.Success, LoadNextGame);
                        }
                        
                       
                    }
                        break;
                    case GameStateType.Retry:
                    {
                        _playerHealth--;
                        if (_playerHealth <= 0)
                        {
                        
                        }
                        else
                        {
                            gameUIManager.TransitionIn(GameStateType.Retry, ReloadCurrentGame);
                        }
                    }
                        break;
                    case GameStateType.Completed:
                        break;
                }
            });
            
           
        }
        
    }

    public enum GameStateType
    {
        Begin = 0,
        Success,
        Retry,
        Failed,
        Completed
    }
}
