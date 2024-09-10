using UnityEngine;
using UnityEngine.Events;

namespace Components.ExtraComponents
{
    public class NumberCounter : MonoBehaviour
    {
        [SerializeField] private int supposedNumber;
        public UnityEvent onNumberCountDoneEvent;

        private int _currentCount;

        public void AddNumber()
        {
            _currentCount++;
            if (_currentCount >= supposedNumber)
            {
                onNumberCountDoneEvent.Invoke();
            }
        }

        public void SubstractNumber()
        {
            _currentCount--;
        }
    }
}