using Audio;
using DG.Tweening;
using Level;
using Tutorial;
using UI;
using UI.Cutscene;
using UI.Overlay;
using UI.Panel;
using UI.Pause_Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private FadingManager fadingManager;
        private CutsceneUIManager cutsceneManager;
        [SerializeField] private PlayerInput gameManagerInput;
        [SerializeField] private InputActionReference gameplayPauseInputRef;
        [SerializeField] private PausePanel pausePanel;
        [SerializeField] private SettingsPanel settingsPanel;

        public SettingsPanel SettingsPanel => settingsPanel;
        [SerializeField] bool isTest;
        
        
        public GameSettings gameSettings;
        public PlayerData playerData;
        private int _currentLevelId;
        private int _lastGameSessionId;
        private bool _timeIsFrozen;
        private bool _isNewGame = true;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            
            Destroy(gameObject);
        }

        private void Start()
        {
            fadingManager = FadingManager.instance;
            cutsceneManager = CutsceneUIManager.instance;
            ToggleGameManagerInput(false);
            playerData = new PlayerData();
            foreach (var level in gameSettings.levelList)
            {
                LevelData newLevel = new LevelData();
                newLevel.levelName = level.levelName;
                newLevel.hasPlayed = false;
                playerData.levelDatas.Add(newLevel);
            }
            
            if(!isTest)
                UIManager.instance.InitiatePanel();
            
        }

        public void ToggleGameManagerInput(bool on)
        {
            if (on)
            {
                gameManagerInput.ActivateInput();
                // gameplayPauseInputRef.action.started += OnGameplayPauseToggled;
            }
            else
            {
                gameManagerInput.DeactivateInput();
                // gameplayPauseInputRef.action.started -= OnGameplayPauseToggled;
            }
        }

        public void TogglePause()
        {
            if (Time.timeScale == 0)
            {
                if (!_timeIsFrozen)
                    Time.timeScale = 1;
                
                pausePanel.HidePanel();
                Cursor.visible = false;
            }
            else
            {
                Time.timeScale = 0;
                pausePanel.ShowPanel();
                Cursor.visible = true;
            }
        }

        public bool HasLastGameUnfinished()
        {
            int levelDatasCount = playerData.levelDatas.Count - 1;
            
            //Checking if last level is the boss level and completed
            if (_currentLevelId == levelDatasCount)
            {
                if (playerData.levelDatas[levelDatasCount].completedGames >=
                    gameSettings.levelList[levelDatasCount].gameSessions.Count)
                {
                    return false;
                }
            }
            for (int i = 0; i < playerData.levelDatas.Count; i++)
            {
                LevelData levelData = playerData.levelDatas[i];
                if (levelData.unlocked)
                {
                    if (levelData.completedGames < gameSettings.levelList[i].gameSessions.Count)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public void ContinueLastGame()
        {
            //Check last level, then play last game id of that level
            _lastGameSessionId = playerData.levelDatas[_currentLevelId].completedGames;
            _isNewGame = false;
            StartLevel(_currentLevelId);
        }

        public void PlayNewGame()
        {
            _isNewGame = true;
            _currentLevelId = 0;
            _lastGameSessionId = 0;
            playerData.levelDatas[_currentLevelId].hasPlayed = false;
            fadingManager.FadeIn(true, () =>
            {
                cutsceneManager.IntroCutscene(_currentLevelId, true);
                fadingManager.FadeOut(true, () =>
                {
                    cutsceneManager.SetCutsceneInputEnabled(true);
                });
            });
        }
        
        public void CompleteIntroCutscene()
        {
            playerData.hasIntro = true;
            playerData.levelDatas[_currentLevelId].unlocked = true;
            if (_currentLevelId > 0)
            {
                SceneManager.sceneUnloaded += CurrentGameSceneUnloaded;
                SceneManager.UnloadSceneAsync("GameSession");
            }
            else
            {
                StartLevel(_currentLevelId, true);
            }
        }

        private void CurrentGameSceneUnloaded(Scene arg0)
        {
            SceneManager.sceneUnloaded -= CurrentGameSceneUnloaded;
            StartLevel(_currentLevelId, true);
        }

        public void CompleteOutroCutscene()
        {
            fadingManager.FadeIn(false, () =>
            {
                cutsceneManager.OutroCutscene(false);
                SceneManager.sceneLoaded += MainMenuSceneLoaded;
                SceneManager.LoadScene("MainMenuScene");    
            });
        }

        public void BackToMainMenu()
        {
            fadingManager.FadeIn(false, () =>
            {
                SceneManager.sceneLoaded += MainMenuSceneLoaded;
                SceneManager.LoadScene("MainMenuScene");    
            });
            
        }
        private void MainMenuSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= MainMenuSceneLoaded;
            fadingManager.FadeOut(false, () =>
            {
                AudioBGMManager.instance.StopAnyBGM();
                UnfreezeTime();
                UIManager.instance.InitiatePanel();
            });
        }

        public void StartLevel(int levelId, bool fromCutscene = false)
        {
            Debug.Log("Starting New Level!");
            fadingManager.FadeIn(false, () =>
            {
                if (fromCutscene)
                {
                    cutsceneManager.IntroCutscene(levelId,false);
                }

                if (!gameSettings.levelList[levelId].haveTutorial)
                {
                    playerData.levelDatas[levelId].hasPlayed = true;
                    OpenGameSession(levelId);
                }
                else
                {
                    if (playerData.levelDatas[levelId].hasPlayed)
                    {
                        OpenGameSession(levelId);
                    }
                    else
                    {
                        OpenTutorial(levelId);
                    }    
                }
            });
        }

        void OpenTutorial(int levelId)
        {
            SceneManager.sceneLoaded += TutorialSceneLoaded;
            _currentLevelId = levelId;
            SceneManager.LoadScene("Tutorial");
        }

        private void TutorialSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "Tutorial")
            {
                SceneManager.sceneLoaded -= TutorialSceneLoaded;
                TutorialManager.instance.LoadTutorialSession(_currentLevelId);    
            }
            
        }

        void OpenGameSession(int levelId)
        {
            SceneManager.sceneLoaded += GameSceneLoaded;
            _currentLevelId = levelId;
            SceneManager.LoadScene("GameSession");
        }

        private void GameSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "GameSession")
            {
                SceneManager.sceneLoaded -= GameSceneLoaded;
                
                if (!_isNewGame)
                {
                    GameSessionManager.instance.SetContinuedGame(_lastGameSessionId);
                    _isNewGame = true;
                }
                
                GameSessionManager.instance.InitializeGameSession(_currentLevelId);    
            }
         
            
        }

        public void TutorialEnded(int thisLevelId)
        {
            playerData.levelDatas[thisLevelId].hasPlayed = true;
            StartLevel(thisLevelId);
        }

        public void FreezeTime()
        {
            _timeIsFrozen = true;
            Time.timeScale = 0f;
        }

        public void UnfreezeTime()
        {
            _timeIsFrozen = false;
            Time.timeScale = 1f;
        }

        public void CompletedLevel()
        {
            _currentLevelId++;
            _lastGameSessionId = 0;
            fadingManager.FadeIn(true, () =>
            {
                if (_currentLevelId >= gameSettings.levelList.Count)
                {
                    cutsceneManager.OutroCutscene(true);
                }
                else
                {
                    cutsceneManager.IntroCutscene(_currentLevelId, true);
                }

                fadingManager.FadeOut(true, () =>
                {
                    cutsceneManager.SetCutsceneInputEnabled(true);
                }, 0.75f);
            }, 0.75f);
            
        }

        public void FailedGameLevel()
        {
            //Should've show pop up first
            SceneManager.sceneUnloaded += FailedGameSceneUnloaded;
            SceneManager.UnloadSceneAsync("GameSession");
            
        }

        private void FailedGameSceneUnloaded(Scene arg0)
        {
            SceneManager.sceneUnloaded -= FailedGameSceneUnloaded;
            StartLevel(_currentLevelId);
        }

        public void CompletedGame()
        {
            _lastGameSessionId++;
            playerData.levelDatas[_currentLevelId].completedGames = _lastGameSessionId;
        }
    }
}
