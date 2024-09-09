using Components.ExtraComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class PushableComponent : ComponentBase
    {
        [SerializeField] private Rigidbody2D rb;
        public UnityEvent onPushedEvent, onCollidedEvent;
        public bool isForPistachio;
        private bool _pushedEventInvoked;
        
        //Physics can be improved, by checking contact point position, check the distance, which axis is the closest to collider center 
        
        protected override void EnteredCollision(Collision2D other)
        {
            PushingComponent pusher = other.gameObject.GetComponent<PushingComponent>();

            if (pusher)
            {
                if (_pushedEventInvoked)
                    return;
                
                if(isForPistachio)
                    rb.bodyType = RigidbodyType2D.Dynamic;
                onPushedEvent.Invoke();
                _pushedEventInvoked = true;
                
                Debug.Log("Pushed Event Invoked");
            }
            else
            {
                if (isForPistachio)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    rb.velocity = Vector2.zero;    
                }
                
            }

            if (other.collider.CompareTag("Floor"))
            {
                onCollidedEvent.Invoke();
            }

        }

        protected override void StayedCollision(Collision2D other)
        {
            if (isForPistachio)
            {
                if (!other.gameObject.GetComponent<PushingComponent>())
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    // rb.velocity = Vector2.zero;
                }    
            }
            
        }
        

        protected override void ExitCollision(Collision2D other)
        {
            PushingComponent pusher = other.gameObject.GetComponent<PushingComponent>();

            if (pusher)
            {
                if(isForPistachio)
                    rb.bodyType = RigidbodyType2D.Static;
                
                rb.velocity = Vector2.zero;
                _pushedEventInvoked = false;
                Debug.Log("Pushed Event can be Invoked Again");
            }
            
            
        }
    }
}
