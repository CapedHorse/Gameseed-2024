using UnityEngine;
using UnityEngine.Events;

namespace Components.ExtraComponents
{
    public class PositionerComponent : MonoBehaviour
    {
        public UnityEvent onPositionedEvent;

        public void SnapReposition(Transform inTransform)
        {
            transform.position = inTransform.position;
            onPositionedEvent.Invoke();
        }
    }
}