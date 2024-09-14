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
            gameObject.SetActive(true);
            meteorDroppingEvent.Invoke();
        }
     
        
        protected override void EnteredTrigger(Collider2D other)
        {
            if (other.CompareTag("Floor"))
            {
                if (other.gameObject.name == "PlatformFloor")
                {
                    return;
                }
            } else if(!other.gameObject.GetComponent<HurtComponent>())
            {
                return;
            }

            meteorCollideEvent.Invoke();
            _dropperOwner.DestroyedMeteor(this);
            Destroy(gameObject);
        }
    }
}