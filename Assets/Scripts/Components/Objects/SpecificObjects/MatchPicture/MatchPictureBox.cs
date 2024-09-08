using Components.Player_Control;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.MatchPicture
{
    public class MatchPictureBox : ComponentBase
    {
        private bool _entered;
        public bool IsEntered => _entered;
        public GameObject correctSign, wrongSign;
        public UnityEvent onSelected, onDeselected;
        protected override void EnteredTrigger(Collider2D other)
        {
            if (other.GetComponent<PlayerControl>())
            {
                _entered = true;
                onSelected.Invoke();
                Debug.Log("Entered answer box!");
            }
        }

        protected override void ExitTrigger(Collider2D other)
        {
            if (other.GetComponent<PlayerControl>())
            {
                _entered = false;
                onDeselected.Invoke();
                Debug.Log("Exited answer box!");
            }
        }

        public void CheckedAnswer(bool correct)
        {
            if (correct)
            {
                correctSign.SetActive(true);
            }
            else
            {
                wrongSign.SetActive(true);
            }
        }
    }
}