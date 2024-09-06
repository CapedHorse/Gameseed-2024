using UnityEngine;

namespace Components.Objects.SpecificObjects.BoxSpillingSeeds
{
    public class SpilledSeed : ComponentBase
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField] private Vector2 launchPower;
        
        public void LaunchSeed()
        {
            rb.AddForce(launchPower);
        }

        protected override void EnteredCollision(Collision2D other)
        {
            if (other.collider.CompareTag("Floor"))
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}