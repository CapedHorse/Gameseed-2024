using System.Collections.Generic;
using Components.Player_Control;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.BoxSpillingSeeds
{
    public class SpillingSeedBox : ComponentBase
    {
        [SerializeField] List<SpilledSeed> spilledSeeds;
        [SerializeField] private Transform boxSpriteTweened;
        private int seedCount;
        private int plantedSeedCount;
        public UnityEvent onSeedSpilledEvent, onSpilledDoneEvent;

        private Sequence _boxSpriteTweener;

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
                
                StopTweenBox();
                spilledSeeds[0].LaunchSeed();
                spilledSeeds.RemoveAt(0);
                onSeedSpilledEvent.Invoke();

                _boxSpriteTweener = boxSpriteTweened.DOJump(transform.position, 0.5f, 1, 0.25f);
            }
        }

        public void StopTweenBox()
        {
            if (_boxSpriteTweener != null)
            {
                _boxSpriteTweener.Kill(true);
                _boxSpriteTweener = null;
            }
        }

        public void CheckSeedLeft(SpilledSeed seed)
        {
            plantedSeedCount++;
            if (plantedSeedCount >= seedCount)
            {
                 onSpilledDoneEvent.Invoke();
                 StopTweenBox();
                 _boxSpriteTweener = boxSpriteTweened.DOJump(transform.position, 0.5f, 1, 0.25f);
            }
        }
    }
}