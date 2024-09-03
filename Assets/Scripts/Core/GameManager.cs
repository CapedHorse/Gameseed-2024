using System;
using Level;
using UI.Overlay;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private OverlayUIManager overlayManager;
        [SerializeField] private PlayerInput gameManagerInput;
        
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
            playerData = new PlayerData();
        }

        public void PlayStoryMode()
        {
            
        }

        public void PlayFreeMode(int levelId, int miniGame)
        {
            //After select level, open character select first
        }

        void OpenCutscene()
        {
            
        }

        public void CompleteCutscene()
        {
            Debug.Log("Cutscene Finished");
            //Will open a tutorial scene
        }

        void OpenTutorial()
        {
            
        }

        public void CompleteTutorial()
        {
            
        }

        void OpenGameSession()
        {
            
        }
        
        
        public void CompleteGameSession()
        {
            //will add last session to player data
            //Check if this game session is the last in the level, proceed to next level
        }
        
        public void RetryCurrentGameSession()
        {
            
        }

        public void RetryFirstGameSession()
        {
            
        }

        void OpenLevel()
        {
            
        }
        
        void CompleteLevel()
        {
            
        }

        void FinalCutscene()
        {
            
        }

        //Utility
        void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }


        void BackToMenu()
        {
            
        }

        void Pause(bool onPause)
        {
            
        }


    }
}
