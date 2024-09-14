using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossCommander : MonoBehaviour
    {
        public UnityEvent onStartMainCommandEvent, onStartSecondaryCommandEvent;

        public void StartMainCommand()
        {
            onStartMainCommandEvent.Invoke();
        }

        public void StartSecondaryCommand()
        {
            onStartSecondaryCommandEvent.Invoke();
        }
        
    }
}