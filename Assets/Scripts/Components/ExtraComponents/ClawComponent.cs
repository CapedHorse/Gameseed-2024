using System;
using Components.Objects;
using Level;
using UnityEngine;
using UnityEngine.Events;

namespace Components.ExtraComponents
{
    public class ClawComponent : ComponentBase
    {
        [SerializeField] private Transform clawVine;
        [SerializeField] private Transform clawEdge;
        [SerializeField] private Transform grabbedTransform;
        [SerializeField] private float clawSpeed = 10;
        private float _vineOriginalScaleY;
        private float _maxClawY;
        private bool _clawLaunched;
        private bool _clawRetracted;
        private bool _stopped;
        private GrabableComponent _grabbed;

        public UnityEvent onClawLaunchedEvent, onClawRetractedEvent;
        public UnityEvent onClawBlocked;

        private void Start()
        {
            _maxClawY = BorderManager.instance.northBorderPoint.position.y;
            _vineOriginalScaleY = clawVine.localScale.y;
            _clawRetracted = false;
            _clawLaunched = true;
            _stopped = true;
        }

        private void FixedUpdate()
        {
            if (!_stopped)
            {
                float scalingResult = Time.deltaTime * clawSpeed;
                if (_clawRetracted)
                {
                    clawEdge.localScale = new Vector2(clawEdge.localScale.x,  1/clawVine.localScale.y);
                    clawVine.localScale = new Vector2(clawVine.localScale.x,
                        Mathf.Clamp(clawVine.localScale.y - scalingResult, 1, 10f));
                    
                    if (clawVine.localScale .y <= _vineOriginalScaleY)
                    {
                        Launch();
                        onClawLaunchedEvent.Invoke();
                    }
                }
                else if(_clawLaunched)
                {
                    clawEdge.localScale = new Vector2(clawEdge.localScale.x,  1/clawVine.localScale.y);
                    clawVine.localScale = new Vector2(clawVine.localScale.x,
                        Mathf.Clamp(clawVine.localScale.y + scalingResult, 1, 10f));

                    if (clawEdge.position.y >= _maxClawY)
                    {
                        Retract();
                        onClawRetractedEvent.Invoke();
                    }
                }
            }
        }

        public void MoveClaw()
        {
            _stopped = false;

            if (_clawLaunched)
            {
                onClawLaunchedEvent.Invoke();
            }

            if (_clawRetracted)
            {
                onClawRetractedEvent.Invoke();
            }
        }

        public void StopClaw()
        {
            _stopped = true;
            ReleaseGrabbed();
            Launch();
        }

        public void ReleaseGrabbed()
        {
            if (_grabbed != null)
            {
                _grabbed.Released(this);
                _grabbed = null;
            }
        }

        protected override void EnteredTrigger(Collider2D other)
        {
            if (other.GetComponent<ClawBlocker>())
            {
                onClawBlocked.Invoke();
                ForceRetract();
                return;
            }
            GrabableComponent grabableComponent = other.GetComponent<GrabableComponent>();
            if (grabableComponent != null && _grabbed == null)
            {
                if (grabableComponent.CanBeGrabbed)
                {
                    grabableComponent.Grab(this, grabbedTransform);
                    _grabbed = grabableComponent;
                }
                Retract();
            }
        }

        void Launch()
        {
            _clawRetracted = false;
            _clawLaunched = true;
        }
        private void Retract()
        {
            _clawRetracted = true;
            _clawLaunched = false;
        }

        public void ForceRetract()
        {
            if (_grabbed != null)
            {
                ReleaseGrabbed();
            }
            Retract();
        }
    }
}