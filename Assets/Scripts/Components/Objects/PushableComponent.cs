using System;
using Components.ExtraComponents;
using UnityEngine;

namespace Components.Objects
{
    public class PushableComponent : ComponentBase
    {
        [SerializeField] private Rigidbody2D rb;
        
        protected override void ExitCollision(Collision2D other)
        {
            PushingComponent pusher = other.gameObject.GetComponent<PushingComponent>();

            if (pusher)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
