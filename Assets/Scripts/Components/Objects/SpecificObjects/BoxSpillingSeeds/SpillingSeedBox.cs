using System.Collections.Generic;
using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.BoxSpillingSeeds
{
    public class SpillingSeedBox : ComponentBase
    {
        [SerializeField] List<SpilledSeed> spilledSeeds;
        
        public UnityEvent onSeedSpilledEvent, onSpilledDoneEvent;
        protected override void EnteredCollision(Collision2D other)
        {
            PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
            if (playerControl)
            {
                spilledSeeds[0].LaunchSeed();
                spilledSeeds.RemoveAt(0);
                onSeedSpilledEvent.Invoke();


                if (spilledSeeds.Count <= 0)
                {
                    onSpilledDoneEvent.Invoke();
                }
            }
        }
    }
}