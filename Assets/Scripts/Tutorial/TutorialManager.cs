using Audio;
using Core;
using UI;
using UI.Overlay;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager instance;

        [SerializeField] private GameObject endLevelGuideUI;
        [SerializeField] private PlayerInput tutorialPlayerInput;
        [SerializeField] private InputActionReference endTutorialReference;
        [SerializeField] private AudioPlayer audioPlayer;
        [SerializeField] private AudioClip tutorialReloadedClip, endTutorialClip;
        [FormerlySerializedAs("inGameFading")] [SerializeField] private InGameFadingTransition inGameFadingTransition;

        private TutorialSession _tutorialSession;
        private string _tutorialSessionName;
        private int _thisLevelId;

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
            endLevelGuideUI.SetActive(false);
            tutorialPlayerInput.DeactivateInput();
            endTutorialReference.action.started += EndTutorial;
        }

        public void LoadTutorialSession(int levelId)
        {
            _thisLevelId = levelId;
            _tutorialSessionName = GameManager.instance.gameSettings.levelList[_thisLevelId].tutorialSceneName;
            GameManager.instance.FreezeTime();
            SceneManager.sceneLoaded += TutorialSceneLoaded;
            AudioBGMManager.instance.PlayBGM(GameManager.instance.gameSettings.levelList[_thisLevelId].tutorialBGM);
            SceneManager.LoadScene(_tutorialSessionName, LoadSceneMode.Additive);

        }

        private void TutorialSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= TutorialSceneLoaded;
            _tutorialSession = FindObjectOfType<TutorialSession>();
            FadingManager.instance.FadeOut(false, CanStartTutor, 0.5f);
        }
        
        private void ReloadSession()
        {
            // Destroy(_tutorialSession.gameObject);
            // _tutorialSession = Instantiate(GameManager.instance.gameSettings.levelList[_thisLevelId].tutorialLevel, transform);
            SceneManager.sceneUnloaded += TutorialSceneUnloaded;
            SceneManager.UnloadSceneAsync(_tutorialSessionName);
        }

        private void TutorialSceneUnloaded(Scene arg0)
        {
            SceneManager.sceneUnloaded -= TutorialSceneUnloaded;
            SceneManager.sceneLoaded += TutorialSceneReloaded;
            SceneManager.LoadScene(_tutorialSessionName, LoadSceneMode.Additive);
        }

        private void TutorialSceneReloaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= TutorialSceneReloaded;
            _tutorialSession = FindObjectOfType<TutorialSession>();
            CanStartTutor();
        }


        private void EndTutorial(InputAction.CallbackContext obj)
        {
            endTutorialReference.action.started -= EndTutorial;
            audioPlayer.PlayClip(endTutorialClip);
            GameManager.instance.TutorialEnded(_thisLevelId);
        }

        public void CanStartTutor()
        {
            GameManager.instance.UnfreezeTime();
            _tutorialSession.StartTutor();
        }
        
        public void CanEndTutor()
        {
            GameManager.instance.FreezeTime();
            audioPlayer.PlayClip(tutorialReloadedClip);
            inGameFadingTransition.Fading(() =>
            {
                endLevelGuideUI.SetActive(true);
                ReloadSession();
            }, () =>
            {
                tutorialPlayerInput.SwitchCurrentActionMap("TutorialUIControl");
                tutorialPlayerInput.ActivateInput();    
            });
        }

    }
}
