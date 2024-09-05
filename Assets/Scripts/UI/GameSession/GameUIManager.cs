using UnityEngine;
using UnityEngine.UI;

namespace UI.GameSession
{
    public class GameUIManager : MonoBehaviour
    {
        public static GameUIManager instance;

        [SerializeField] private GameObject[] health;
        [SerializeField] private Slider timerSlider;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }
    }
}