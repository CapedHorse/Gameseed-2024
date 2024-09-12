using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.ExtraComponents
{
    public class NumberCounter : MonoBehaviour
    {
        [SerializeField] private int supposedNumber;
        [SerializeField] private int initialNumber;
        public UnityEvent onNumberCountDoneEvent, onNumberCountZeroEvent;

        private int _currentCount;

        private void Start()
        {
            _currentCount = initialNumber;
        }

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

            if (_currentCount <= 0)
            {
                onNumberCountZeroEvent.Invoke();
            }
        }
    }
}