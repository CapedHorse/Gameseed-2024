using Components.Player_Control_Components;
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
            }
        }

        protected override void ExitTrigger(Collider2D other)
        {
            if (other.GetComponent<PlayerControl>())
            {
                _entered = false;
                onDeselected.Invoke();
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