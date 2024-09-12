using System;
using Components.ExtraComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class GrabableComponent : ComponentBase
    {
        protected bool _grabbed;

        public UnityEvent onGrabbedEvent, onReleasedEvent, onDraggedDownEvent, onDraggedUpEvent;
        private ClawComponent _grabber;
        private bool _canBeGrabbed = true;
        public bool CanBeGrabbed => _canBeGrabbed;

        private Vector2 _initialPos;
        private float _lastYPos;

        private void Start()
        {
            _initialPos = transform.position;
        }

        private void Update()
        {
            if (_grabbed)
            {
                if (_lastYPos > transform.position.y)
                {
                    onDraggedDownEvent.Invoke();
                } else if (_lastYPos < transform.position.y)
                {
                    onDraggedUpEvent.Invoke();
                }
                
                _lastYPos = transform.position.y;
                
            }
        }

        public void Grab(ClawComponent grabber)
        {
            if (grabber != null)
            {
                _grabbed = true;
                _grabber = grabber;
                transform.SetParent(grabber.transform);
                onGrabbedEvent.Invoke();
            }
        }

        public void Released(ClawComponent releaser)
        {
            if (releaser != null)
            {
                _grabbed = false;
                _grabber = null;
                transform.SetParent(null);
                onReleasedEvent.Invoke();
            }
        }

        public void BreakFree()
        {
            if (_grabber != null)
            {
                _grabber.ForceRetract();
            }
        }

        public void SetCanGrabbed(bool can)
        {
            _canBeGrabbed = can;
        }

        public void ResetPosition()
        {
            transform.position = _initialPos;
        }
    }
}
