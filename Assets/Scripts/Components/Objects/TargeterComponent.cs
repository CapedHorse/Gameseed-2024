using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class TargeterComponent : ComponentBase
    {
        [SerializeField] private string targeterID;
        public UnityEvent onEnteredTarget;
        public string TargeterID => targeterID;

        public void EnteredTarget()
        {
            onEnteredTarget.Invoke();
        }
    }
}