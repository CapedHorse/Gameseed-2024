using Components.Objects;
using UnityEngine;

namespace Components.ExtraComponents
{
    public class PushingComponent : ComponentBase
    {
        [SerializeField] private float pushingForce = 5f;

        public float PushingForce => pushingForce;
        
    }
}