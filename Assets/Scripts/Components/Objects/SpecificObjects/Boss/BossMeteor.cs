using Components.ExtraComponents;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossMeteor : ComponentBase
    {
        public UnityEvent meteorDroppingEvent, meteorCollideEvent;
        
        
        private BossMeteorDropper _dropperOwner;

        public void DropMeteor(BossMeteorDropper meteorDropper)
        {
            _dropperOwner = meteorDropper;
            meteorDroppingEvent.Invoke();
        }
     
        
        protected override void EnteredTrigger(Collider2D other)
        {
            if (other.CompareTag("Floor") || other.gameObject.GetComponent<HurtComponent>())
            {
                meteorCollideEvent.Invoke();
                _dropperOwner.DestroyedMeteor(this);
                Destroy(gameObject);
            }
        }
    }
}