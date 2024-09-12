using DG.Tweening;
using Level;
using Tutorial;
using UI;
using UI.Cutscene;
using UI.Overlay;
using UI.Panel;
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
        // [SerializeField] private LevelSelectionPanel levelSelectionPanel;
        
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
            ToggleGameManagerInput(true);
            playerData = new PlayerData();
            foreach (var level in gameSettings.levelList)
            {
                LevelData newLevel = new LevelData();
                newLevel.levelName = level.levelName;
                newLevel.hasPlayed = false;
                playerData.levelDatas.Add(newLevel);
            }
            
            UIManager.instance.InitiatePanel();

            gameManagerInput.controlsChangedEvent.AddListener(ControlsChanged);
            gameManagerInput.onControlsChanged += ControlsChanged;
        }

        public void ControlsChanged(PlayerInput obj)
        {
            Debug.Log("Current control is "+ obj.currentControlScheme);
        }

        public void ToggleGameManagerInput(bool on)
        {
            if(on)
                gameManagerInput.ActivateInput();
            else
                gameManagerInput.DeactivateInput();
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
            
            /*if (playerData.hasIntro)
            {
                StartLevel(_currentLevelId);
            }
            else
            {
                
                
            }*/
            
        }
        
        public void CompleteIntroCutscene()
        {
            Debug.Log("Cutscene Finished");
            playerData.hasIntro = true;
            playerData.levelDatas[_currentLevelId].unlocked = true;
            if (_currentLevelId > 0)
            {
                SceneManager.sceneUnloaded += CurrentGameSceneUnloaded;
                SceneManager.UnloadSceneAsync("GameSession");
            }
            else
            {
                Debug.Log("Start level krn habis cutscene pertama");
                StartLevel(_currentLevelId, true);
            }
        }

        private void CurrentGameSceneUnloaded(Scene arg0)
        {
            SceneManager.sceneUnloaded -= CurrentGameSceneUnloaded;
            Debug.Log("Start level habis unload sebelumnya");
            StartLevel(_currentLevelId, true);
        }

        public void CompleteOutroCutscene()
        {
            Debug.Log("Outro Cutscene Finished");
            //Might Show Credits first
            BackToMainMenu();
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
                    Debug.Log("Opening GameSes cuz dont have tutorial");
                    OpenGameSession(levelId);
                }
                else
                {
                    if (playerData.levelDatas[levelId].hasPlayed)
                    {
                        //ini masuk ke sini 2 kali
                        Debug.Log("Opening GameSes cuz played have tutorial");
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
            Debug.Log("GameSession opening");
            SceneManager.sceneLoaded += GameSceneLoaded;
            _currentLevelId = levelId;
            SceneManager.LoadScene("GameSession");
        }

        private void GameSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "GameSession")
            {
                Debug.Log("GameSession scene loaded");
                SceneManager.sceneLoaded -= GameSceneLoaded;
                GameSessionManager.instance.InitializeGameSession(_currentLevelId);    
            }
            else
            {
                Debug.Log("Actually not GameSession");
            }
            
        }

        public void TutorialEnded(int thisLevelId)
        {
            playerData.levelDatas[thisLevelId].hasPlayed = true;
            Debug.Log("starting level cuz tutor ended");
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
                });
            });
            
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
