using System.Collections;
using Audio;
using Core;
using DG.Tweening;
using UI.GameSession;
using UI.Overlay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class GameSessionManager : MonoBehaviour
    {
        public static GameSessionManager instance;

        [SerializeField]
        private GameUIManager gameUIManager;

        [SerializeField] private AudioPlayer gameSessionAudioPlayer;
        [SerializeField] private AudioClip goodGameStateClip, badGameStateClip;
        private GameSession _currentGameSession;
        private string _currentGameSessionName;
        
        public int CurrentLevelId => _currentGameLevelId;
        private int _currentGameLevelId;
        
        public int CompletedGameSessionCount => _completedGameSessionCount;
        private int _completedGameSessionCount = 0;
        
        public bool StartedGame => _startGame;
        private bool _startGame;

        public int PlayerHeath => _playerHealth;
        private int _playerHealth = 3;
        public bool haveHeart = true;

        public bool HaveTime => _haveTime;
        private bool _haveTime;
        public float CurrentPlayTime => _currentPlayTime;
        private float _currentPlayTime;
        
        private float _kedutTimerTime;
        

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

            if (!_haveTime)
                return;

            _currentPlayTime -= Time.deltaTime;
            gameUIManager.SetTimerValue(_currentPlayTime);
            if (_currentPlayTime <= 0)
            {
                gameUIManager.ResetKedutTimer();
                _currentGameSession.TimerRunsOut();
            } 
            else if (_currentPlayTime is > 0 and <= 5f)
            {
                _kedutTimerTime += Time.deltaTime;
                if (_kedutTimerTime >= 1f)
                {
                    _kedutTimerTime = 0;
                    gameUIManager.KedutTimer();
                }
            }
        }
        #endregion

        #region TestingSession

        public void InitializeTesting(string testedName)
        {
            _currentGameSessionName = testedName;
            _currentGameSession = FindObjectOfType<GameSession>();
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            haveHeart = gameSettingsLevel.haveHeart;
            _playerHealth = haveHeart ? gameSettings.playerHealthEachLevel : 1;
            _currentGameSessionName = SceneManager.GetSceneAt(1).name;
            
            
            SetupTime(gameSettingsLevel);
            gameUIManager.TransitionIn(GameStateType.Begin, () =>
            {
                FadingManager.instance.FadeOut(false, () =>
                {
                    StartCoroutine(PrepareStartGame());
                }, 0.5f);
            });
        }

        #endregion

        #region New Level Session

        public void InitializeGameSession(int levelId)
        {
            _currentGameLevelId = levelId;
            
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];

            haveHeart = gameSettingsLevel.haveHeart;
            _playerHealth = haveHeart ? gameSettings.playerHealthEachLevel : 1;
            _currentGameSessionName = gameSettingsLevel.gameSessions[_completedGameSessionCount].gameSessionName;
            
            GameManager.instance.FreezeTime();

            SceneManager.sceneLoaded += OnNewGameSceneLoaded;
            SceneManager.LoadScene(_currentGameSessionName, LoadSceneMode.Additive);
        }
        
        private void OnNewGameSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == _currentGameSessionName)
            {
                SceneManager.sceneLoaded -= OnNewGameSceneLoaded;
                _currentGameSession = FindObjectOfType<GameSession>();
                gameUIManager.TransitionIn(GameStateType.Begin, () =>
                {
                    FadingManager.instance.FadeOut(false, () =>
                    {
                        StartCoroutine(PrepareStartGame());
                    }, 0.5f);
                });
            }
            
        }

        #endregion

        #region Starting Preparation

        private IEnumerator PrepareStartGame()
        {
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
            GameSessionSettings sessionSettings = GameManager.instance.gameSettings
                .levelList[_currentGameLevelId].gameSessions[_completedGameSessionCount];
            gameSessionAudioPlayer.PlayBGM(sessionSettings.gameBGM);
            gameUIManager.ShowGameName(_currentGameSessionName);
            DOVirtual.DelayedCall(GameManager.instance.gameSettings.delayBeforeRealPlay+ 0.25f, () =>
            {
                gameUIManager.HideGameName();
                _currentGameSession.StartGame();
                _startGame = true;
                GameManager.instance.ToggleGameManagerInput(true);
                GameManager.instance.UnfreezeTime();
            });
            
        }

        #endregion

        #region Next Game Loading

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
            _currentGameSessionName = gameSettingsLevel.gameSessions[_completedGameSessionCount].gameSessionName;
            
            SceneManager.sceneLoaded += OnNextGameLoaded;
            SceneManager.LoadScene(_currentGameSessionName, LoadSceneMode.Additive);
        }

        private void OnNextGameLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnNextGameLoaded;
            _currentGameSession = FindObjectOfType<GameSession>();
            StartCoroutine(PrepareStartGame());
        }


        #endregion

        #region Game Retried

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
            _currentGameSession = FindObjectOfType<GameSession>();
            StartCoroutine(PrepareStartGame());
        }

        #endregion

        public void EndedGameSession(GameStateType type)
        {
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            if (type == GameStateType.Success || type == GameStateType.Completed)
            {
                gameSessionAudioPlayer.PlayClip(goodGameStateClip);
            } else if (type == GameStateType.Failed || type == GameStateType.Retry)
            {
                gameSessionAudioPlayer.PlayClip(badGameStateClip);
                
            }
            _startGame = false;
            GameManager.instance.FreezeTime();
            GameManager.instance.ToggleGameManagerInput(false);
            
            DOVirtual.DelayedCall(gameSettingsLevel.gameSessions[_completedGameSessionCount].delayWhenShowingState, () =>
            {
                switch (type)
                {
                    case GameStateType.Begin:
                        break;
                    case GameStateType.Success:
                    {
                        _completedGameSessionCount++;
                        if (_completedGameSessionCount >= gameSettingsLevel.gameSessions.Count)
                        {
                            GameManager.instance.CompletedLevel();
                        }
                        else
                        {
                            SetupTime(gameSettingsLevel);
                            gameUIManager.TransitionIn(GameStateType.Success, LoadNextGame);
                        }
                    }
                        break;
                    case GameStateType.Retry:
                    {
                        _playerHealth--;
                        if (_playerHealth <= 0)
                        {
                            //Should show buttons instead
                            gameUIManager.TransitionIn(GameStateType.Failed);
                        }
                        else
                        {
                            SetupTime(gameSettingsLevel);
                            gameUIManager.TransitionIn(GameStateType.Retry, ReloadCurrentGame);
                        }
                    }
                        break;
                    case GameStateType.Completed:
                        break;
                }
            });
        }

        public void RetryThisLevel()
        {
            GameManager.instance.FailedGameLevel();
        }
        public void BackToMainMenu()
        {
            GameManager.instance.BackToMainMenu();
        }

        private void SetupTime(LevelSettings gameSettingsLevel)
        {
            _haveTime = gameSettingsLevel.gameSessions[_completedGameSessionCount].haveTime;
            _currentPlayTime = _haveTime ? gameSettingsLevel.gameSessions[_completedGameSessionCount].playTimer : 0;
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
