using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossCommander : MonoBehaviour
    {
        [SerializeField] private BossAttack atacker;
        [SerializeField] private BossMeteorDropper meteorDropper;
        public UnityEvent onStartCommandingEvent;

        public void StartCommanding()
        {
            onStartCommandingEvent.Invoke();
        }
    }
}