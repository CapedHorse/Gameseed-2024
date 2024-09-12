using UnityEngine;

namespace Components.ExtraComponents
{
    public class LiftOrDropComponent : MonoBehaviour
    {
        [SerializeField] private float changingSpeed = 0.1f;

        public void LiftPosition()
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + changingSpeed);
        }
        public void DropPosition()
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - changingSpeed);
        }
    }
}
