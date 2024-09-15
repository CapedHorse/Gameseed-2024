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
        private bool _timeIsFrozen;

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

        private void OnGameplayPauseToggled(InputAction.CallbackContext obj)
        {
            TogglePause();
        }

        public void TogglePause()
        {
            if (Time.timeScale == 0)
            {
                if (!_timeIsFrozen)
                    Time.timeScale = 1;
                
                pausePanel.HidePanel();
            }
            else
            {
                Time.timeScale = 0;
                pausePanel.ShowPanel();
            }
        }

        public void PlayGame()
        {
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
                _currentLevelId = 0;
                AudioBGMManager.instance.StopAnyBGM();
                UnfreezeTime();
                UIManager.instance.InitiatePanel();
            });
        }

        public void StartLevel(int levelId, bool fromCutscene = false)
        {
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
    }
}
