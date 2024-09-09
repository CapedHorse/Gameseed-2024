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
            // transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.y, 0, rb.velocity.x));
        }


        protected override void EnteredCollision(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}