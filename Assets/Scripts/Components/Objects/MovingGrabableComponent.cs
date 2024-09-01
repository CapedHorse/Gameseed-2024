using System;
using UnityEngine;

namespace Components.Objects
{
    public class MovingGrabableComponent : GrabableComponent
    {
        [SerializeField] private float moveSpeed = 10;
        private float initialXPos;
        private float targetXPos;

        private void OnEnable()
        {
            initialXPos = transform.localPosition.x;
            targetXPos = initialXPos * -1;
        }

        private void FixedUpdate()
        {
            if (_grabbed)
                return;
            
            if (transform.localPosition.x != targetXPos)
            {
                transform.localPosition =
                    new Vector2(transform.localPosition.x + Time.fixedDeltaTime * moveSpeed * Mathf.Sign(targetXPos), transform.localPosition.y);    
            }
            else if(transform.localPosition.x == targetXPos)
            {
                transform.localPosition = new Vector2(initialXPos, transform.localPosition.y);
            }
            
            
            
            
        }
    }
}