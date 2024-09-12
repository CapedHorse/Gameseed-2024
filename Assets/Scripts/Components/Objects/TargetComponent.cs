using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class TargetComponent : ComponentBase
    {
        [SerializeField] private string targetID;
        [SerializeField] private bool canBeEnteredMultipleTimes;
        public UnityEvent onTargetEnteredEvent;
        public string TargetID => targetID;
        private bool _hasEntered;
        protected override void EnteredTrigger(Collider2D other)
        {
            if (_hasEntered && !canBeEnteredMultipleTimes)
                return;
            
            TargeterComponent targeter = other.gameObject.GetComponent<TargeterComponent>();
            if (targeter != null)
            {
                if (targeter.TargeterID == targetID)
                {
                    onTargetEnteredEvent.Invoke();
                    _hasEntered = true;
                }
            }
        }
    }
}