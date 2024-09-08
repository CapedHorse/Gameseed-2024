using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.BoxSpillingSeeds
{
    public class SpilledSeed : ComponentBase
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer thisSpriteRenderer;
        [SerializeField] private SpriteRenderer thisPlantSpriteRenderer;
        [SerializeField] private Sprite grownPlantSprite;
        [SerializeField] private Vector2 launchPower;
        private bool _launched;

        public UnityEvent seedPlantedEvent;
        public void LaunchSeed()
        {
            if (_launched)
                return;
            _launched = true;
            rb.gravityScale = 1;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(launchPower);
            thisSpriteRenderer.sortingOrder = -1;
        }

        protected override void EnteredCollision(Collision2D other)
        {
            if (other.collider.CompareTag("Floor"))
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                rb.gravityScale = 0f;
                transform.position = other.GetContact(0).point;
                thisSpriteRenderer.gameObject.SetActive(false);
                thisPlantSpriteRenderer.gameObject.SetActive(true);
                seedPlantedEvent.Invoke();
            }
        }
    }
}