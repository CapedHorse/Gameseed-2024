using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField] private Button levelSelectButton;
        [SerializeField] private GameObject[] completedGameTokens;
        [SerializeField] private Image characterImage;
        [SerializeField] private Sprite lockedCharacterSprite;
        [SerializeField] private Sprite unlockedCharacterSprite;
        [SerializeField] private GameObject lockedSign;
        [SerializeField] private Image characterNameImage;
        [SerializeField] private Sprite lockedCharNameSprite;
        [SerializeField] private Sprite unlockedCharNameSprite;

        public void SetLocked()
        {
            levelSelectButton.interactable = false;
        }

        public void SetUnlocked()
        {
            levelSelectButton.interactable = true;
        }

        public void AddListenerToButton(UnityAction action)
        {
            levelSelectButton.onClick.RemoveAllListeners();
            levelSelectButton.onClick.AddListener(action);
        }
    }
}