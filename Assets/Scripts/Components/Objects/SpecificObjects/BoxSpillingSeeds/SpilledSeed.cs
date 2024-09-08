using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace Components.Objects.SpecificObjects.BoxSpillingSeeds
{
    public class SpilledSeed : ComponentBase
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer thisSpriteRenderer;
        [SerializeField] private Sprite grownPlantSprite;
        [SerializeField] private Vector2 launchPower;

        public UnityEvent seedPlantedf;
        public void LaunchSeed()
        {
            rb.gravityScale = 1;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(launchPower);
            thisSpriteRenderer.sortingOrder = -1;
        }

        private void FixedUpdate()
        {
            
        }

        protected override void EnteredCollision(Collision2D other)
        {
            if (other.collider.CompareTag("Floor"))
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                rb.gravityScale = 0f;
                transform.position = other.GetContact(0).point;
                thisSpriteRenderer.sprite = grownPlantSprite;
            }
        }
    }
}