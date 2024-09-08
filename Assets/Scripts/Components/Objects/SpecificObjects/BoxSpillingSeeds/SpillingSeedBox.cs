using System.Collections.Generic;
using Components.Player_Control;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.BoxSpillingSeeds
{
    public class SpillingSeedBox : ComponentBase
    {
        [SerializeField] List<SpilledSeed> spilledSeeds;
        private int seedCount;
        private int plantedSeedCount;
        public UnityEvent onSeedSpilledEvent, onSpilledDoneEvent;


        private void Start()
        {
            seedCount = spilledSeeds.Count;
        }

        protected override void EnteredCollision(Collision2D other)
        {
            PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
            if (playerControl)
            {
                if (spilledSeeds.Count <= 0)
                    return;
                
                spilledSeeds[0].LaunchSeed();
                spilledSeeds.RemoveAt(0);
                onSeedSpilledEvent.Invoke();
            }
        }

        public void CheckSeedLeft(SpilledSeed seed)
        {
            plantedSeedCount++;
            if (plantedSeedCount >= seedCount)
            {
                 onSpilledDoneEvent.Invoke();
            }
        }
    }
}