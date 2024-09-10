using UnityEngine;

namespace Components.Objects
{
    public class MovingGrabableComponent : GrabableComponent
    {
        [SerializeField] private float moveSpeed = 10;
        private float initialXPos;
        private float targetXPos;

        private bool _fromRight;
        private bool _moveToLeft;

        private void OnEnable()
        {
            initialXPos = transform.localPosition.x;
            targetXPos = initialXPos * -1;

            if (initialXPos > 0)
            {
                _moveToLeft = true;
            }
        }

        private void FixedUpdate()
        {
            if (_grabbed)
                return;
            
            if (_moveToLeft)
            {
                transform.localPosition =
                    new Vector2(transform.localPosition.x - Time.fixedDeltaTime * moveSpeed , transform.localPosition.y);
                if (transform.localPosition.x <= targetXPos)
                {
                    _moveToLeft = false;
                    transform.localScale = new Vector2(-1, 1);
                }
            }
            else
            {
                transform.localPosition =
                    new Vector2(transform.localPosition.x + Time.fixedDeltaTime * moveSpeed , transform.localPosition.y);
                if (transform.localPosition.x >= initialXPos)
                {
                    _moveToLeft = true;
                    transform.localScale = new Vector2(1, 1);
                }
            }

        }
    }
}