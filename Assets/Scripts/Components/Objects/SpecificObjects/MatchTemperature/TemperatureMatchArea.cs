using System;
using Components.Objects.SpecificObjects.Pistachio;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.MatchTemperature
{
    public class TemperatureMatchArea : ComponentBase
    {
        [SerializeField] private Transform temperatureMatcher;
        
        private bool _temperatureMatched = false;

        public UnityEvent onFineTempEvent, onColdEvent, onHotEvent,
            onTemperatureMatchedEvent, onTemperatureFailedToMatchEvent;

        private bool _invokedOnCold, _invokedOnHot;
        
        
        private void Update()
        {
            if (!_temperatureMatched)
            {
                if (temperatureMatcher.position.y < transform.position.y)
                {
                    if (!_invokedOnCold)
                    {
                        _invokedOnCold = true;
                        _invokedOnHot = false;
                        onColdEvent.Invoke();
                    }
                }
                else
                {
                    if (!_invokedOnHot)
                    {
                        _invokedOnHot = true;
                        _invokedOnCold = false;
                        onHotEvent.Invoke();
                    }
                }
            }    
        }

        public void CheckTemperatureMatch()
        {
            if (_temperatureMatched)
            {
                onTemperatureMatchedEvent.Invoke();
            }
            else
            {
                onTemperatureFailedToMatchEvent.Invoke();
            }
        }

        protected override void EnteredTrigger(Collider2D other)
        {
            if (other.gameObject.GetComponent<BulletPushable>())
            {
                _temperatureMatched = true;
                onFineTempEvent.Invoke();
            }
        }

        protected override void ExitTrigger(Collider2D other)
        {
            if (other.gameObject.GetComponent<BulletPushable>())
            {
                _temperatureMatched = false;
            }
        }
    }
}