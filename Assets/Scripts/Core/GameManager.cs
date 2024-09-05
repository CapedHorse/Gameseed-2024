using System;
using Level;
using Tutorial;
using UI.Cutscene;
using UI.Overlay;
using UI.Panel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private FadingManager fadingManager;
        private CutsceneUIManager cutsceneManager;
        [SerializeField] private PlayerInput gameManagerInput;
        [SerializeField] private LevelSelectionPanel levelSelectionPanel;
        
        public GameSettings gameSettings;
        public PlayerData playerData;
        
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
            
            gameManagerInput.DeactivateInput();
            
            playerData = new PlayerData();
            foreach (var level in gameSettings.levelList)
            {
                LevelData newLevel = new LevelData();
                newLevel.levelName = level.levelName;
                newLevel.hasPlayed = false;
                playerData.levelDatas.Add(newLevel);
            }
        }

        public void PlayGame()
        {
            fadingManager.FadeIn(() =>
            {
                if (playerData.hasIntro)
                {
                    levelSelectionPanel.OnPanelFinishShow.AddListener(() =>
                    {
                        fadingManager.FadeOut(() =>
                        {
                            levelSelectionPanel.ToggleLevelInput(true);
                        });
                    });
                    levelSelectionPanel.ShowPanel();
                }
                else
                {
                    cutsceneManager.IntroCutscene(true);
                    fadingManager.FadeOut(() =>
                    {
                        cutsceneManager.SetCutsceneInputEnabled(true);
                    });
                }
            });
        }
        
        public void CompleteIntroCutscene()
        {
            Debug.Log("Cutscene Finished");
            playerData.hasIntro = true;
            playerData.levelDatas[0].unlocked = true;
            fadingManager.Fading(() =>
            {
                cutsceneManager.IntroCutscene(false);
                levelSelectionPanel.ShowPanel();
            }, () =>
            {
                levelSelectionPanel.ToggleLevelInput(true);
            });
        }

        public void OpenLevel(int levelId)
        {
            
            fadingManager.FadeIn(() =>
            {
                levelSelectionPanel.HidePanel();
                levelSelectionPanel.ToggleLevelInput(false);
                
                if (playerData.levelDatas[levelId].hasPlayed)
                {
                    OpenGameSession(levelId);
                }
                else
                {
                    OpenTutorial(levelId);
                }
            });
        }

        void OpenTutorial(int levelId)
        {
            SceneManager.sceneLoaded += (Scene arg0, LoadSceneMode arg1) =>
            {
                TutorialManager.instance.LoadTutorialSession(gameSettings.levelList[levelId].tutorialLevel);
                fadingManager.FadeOut();
            };
            
            SceneManager.LoadScene("Tutorial");
        }

        void OpenGameSession(int levelId)
        {
            SceneManager.sceneLoaded += (Scene arg0, LoadSceneMode arg1) =>
            {
                GameSessionManager.instance.LoadGameSession(gameSettings.levelList[levelId].gameSessionPrefabList);
                fadingManager.FadeOut();
            };
            
            SceneManager.LoadScene("GameSession");
        }
    }
}
