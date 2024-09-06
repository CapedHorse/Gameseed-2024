using UnityEngine;
using UnityEngine.Events;

namespace Components.ExtraComponents
{
    public class HurtComponent : MonoBehaviour
    {
        public UnityEvent onHurtEvent;

        public void GotHurt()
        {
            onHurtEvent.Invoke();
        }
    }
}