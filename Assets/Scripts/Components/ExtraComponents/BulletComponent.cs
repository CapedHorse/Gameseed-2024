using System;
using UnityEngine;

namespace Components.ExtraComponents
{
    public class BulletComponent : DestroyerComponent
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float bulletSpeed = 20f;
        private void OnEnable()
        {
            rb.velocity = Vector2.zero;
        }
        
        public void Launch(Vector2 direction)
        {
            rb.velocity = direction * bulletSpeed;
        }


        protected override void EnteredCollision(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}