using UnityEngine;

namespace Components.ExtraComponents
{
    public class ClawBlocker : MonoBehaviour
    {
        [SerializeField] private bool canMove;
        
        [SerializeField] private float moveSpeed = 10;
        [SerializeField] private float targetPos;
        private float initialXPos;
        private float targetXPos;

        private bool _fromRight;
        private bool _moveToLeft;

        private void Start()
        {
            if (!canMove)
                return;
            
            initialXPos = transform.localPosition.x;
            targetXPos = targetPos;

            if (targetXPos <= initialXPos)
            {
                _moveToLeft = true;
            }
        }

        private void FixedUpdate()
        {
            if (!canMove)
                return;

            
            if (_moveToLeft)
            {
                transform.localPosition =
                    new Vector2(transform.localPosition.x - Time.fixedDeltaTime * moveSpeed , transform.localPosition.y);
                
                if ((targetXPos < initialXPos && transform.localPosition.x <= targetXPos) || (targetXPos >= initialXPos && transform.localPosition.x <= initialXPos))
                {
                    _moveToLeft = false;
                    transform.localScale = new Vector2(-1, 1);
                }
            }
            else
            {
                transform.localPosition =
                    new Vector2(transform.localPosition.x + Time.fixedDeltaTime * moveSpeed , transform.localPosition.y);
                if ((targetXPos >= initialXPos && transform.localPosition.x >= targetXPos) || (targetXPos < initialXPos && transform.localPosition.x >= initialXPos))
                {
                    _moveToLeft = true;
                    transform.localScale = new Vector2(1, 1);
                }
            }
        }
    }
}