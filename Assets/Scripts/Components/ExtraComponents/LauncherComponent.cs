using UnityEngine;
using UnityEngine.Events;

namespace Components.ExtraComponents
{
    public class LauncherComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Vector2 launchPower;

        public UnityEvent onLaunchedEvent;
        
        public void Launch()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = 1;
            rb.gravityScale = 0;
            rb.AddForce(launchPower);
            onLaunchedEvent.Invoke();
        }
    }
}