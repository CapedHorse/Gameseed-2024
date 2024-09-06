using System.Collections.Generic;
using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.BoxSpillingSeeds
{
    public class SpillingSeedBox : ComponentBase
    {
        [SerializeField] List<SpilledSeed> spilledSeeds;

        public UnityEvent onSeedSpilledEvent;
        protected override void EnteredCollision(Collision2D other)
        {
            PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
            if (playerControl)
            {
                spilledSeeds[spilledSeeds.Count - 1].LaunchSeed();
                spilledSeeds.RemoveAt(spilledSeeds.Count - 1);
                onSeedSpilledEvent.Invoke();
            }
        }
    }
}