using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Components.Objects
{
    public class TargetComponent : ComponentBase
    {
        [SerializeField] private string targetID;
        public UnityEvent onTargetEnteredEvent;
        public string TargetID => targetID;

        protected override void EnteredTrigger(Collider2D other)
        {
            TargeterComponent targeter = other.gameObject.GetComponent<TargeterComponent>();
            if (targeter != null)
            {
                if (targeter.TargeterID == targetID)
                {
                    onTargetEnteredEvent.Invoke();
                }
            }
        }
    }
}