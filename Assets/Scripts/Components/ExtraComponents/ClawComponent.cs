using Components.Objects;
using UnityEngine;

namespace Components.ExtraComponents
{
    public class ClawComponent : ComponentBase
    {
        [SerializeField] private Transform clawVine;
        [SerializeField] private float clawSpeed = 10;
        private float originClawY;
        private bool _clawLaunched;
        private bool _clawRetracted;

        private void FixedUpdate()
        {
            if (_clawLaunched)
            {
                if (_clawRetracted)
                {
                    transform.position = new Vector2(transform.position.x,
                        transform.position.y - Time.fixedDeltaTime * clawSpeed);
                    
                    clawVine.position = new Vector2(clawVine.position.x,
                        clawVine.position.y - (Time.fixedDeltaTime * clawSpeed) / 2);

                    clawVine.localScale = new Vector2(clawVine.localScale.x,
                        clawVine.localScale.y - Time.deltaTime * clawSpeed);
                    if (transform.position.y <= originClawY)
                    {
                        _clawRetracted = false;
                        _clawLaunched = false;
                    }
                }
                else
                {
                    transform.position = new Vector2(transform.position.x,
                        transform.position.y + Time.fixedDeltaTime * clawSpeed);
                    
                    clawVine.position = new Vector2(clawVine.position.x,
                        clawVine.position.y + (Time.fixedDeltaTime * clawSpeed) / 2);
                    
                    clawVine.localScale = new Vector2(clawVine.localScale.x,
                        clawVine.localScale.y + Time.deltaTime * clawSpeed);
                }
            }

            
        }

        public void LaunchClaw()
        {
            if (_clawLaunched)
                return;

            originClawY = transform.position.y;
            _clawLaunched = true;
            Debug.Log("Launching Claw");
            
        }

        protected override void EnteredCollision(Collision2D other)
        {
            if (other.collider.CompareTag("Ceil"))
            {
                _clawRetracted = true;
            }
        }

        protected override void EnteredTrigger(Collider2D other)
        {
            GrabableComponent grabableComponent = other.GetComponent<GrabableComponent>();
            if (grabableComponent != null)
            {
                _clawRetracted = true;
            }
        }
    }
}