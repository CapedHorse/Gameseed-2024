using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager instance;
        
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


        public void LoadTutorialSession(TutorialSession tutorialLevel)
        {
            Instantiate(tutorialLevel, transform);
        }
    }
}
