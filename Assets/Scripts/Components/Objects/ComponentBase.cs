using System;
using UnityEngine;

namespace Components.Objects
{
    public class ComponentBase : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            EnteredCollision(other);
        }

        protected virtual void EnteredCollision(Collision2D other)
        {
            
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            ExitCollision(other);
        }

        protected virtual void ExitCollision(Collision2D other)
        {
            
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            StayedCollision(other);
        }

        protected virtual void StayedCollision(Collision2D other)
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnteredTrigger(other);
        }

        protected virtual void EnteredTrigger(Collider2D other)
        {
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ExitTrigger(other);
        }

        protected virtual void ExitTrigger(Collider2D other)
        {
            
        }
    }
}
