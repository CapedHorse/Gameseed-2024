using System;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossMeteorDropper : MonoBehaviour
    {
        [SerializeField] private Transform meteorDropTarget;
        [SerializeField] private BossMeteor meteorPrefab;
        [SerializeField] private Transform meteorsParent;
        [FormerlySerializedAs("warningSign")] [SerializeField] private AttackWarningSign attackWarningSign;
        [SerializeField] private int meteorDroppingTimes = 3;
        [SerializeField] private float droppingInterval = 2f;
        [SerializeField] private float warningToDropTime = 1f;

        public UnityEvent runOutOfMeteorsEvent;
        private bool _droppingMeteor;
        private float _currentDroppingWaitTime;
        private bool _isDroppingMeteor;
        private int _currentDroppedMeteorCount;


        //for x time, drop meteor, then count to meteor dropping times, if max, then start attacking the player

        public void StartDropping(bool can)
        {
            if (can)
            {
                _droppingMeteor = true;
            }
            else
            {
                _droppingMeteor = false;
            }
        }

        private void Update()
        {
            if (!_droppingMeteor)
                return;

            _currentDroppingWaitTime += Time.deltaTime;

            if (_currentDroppingWaitTime >= droppingInterval)
            {
                DropMeteor();
            }
        }

        private void DropMeteor()
        {
            StartDropping(false);
            
            BossMeteor newMeteor = Instantiate(meteorPrefab, meteorsParent);
            newMeteor.transform.position = new Vector2(meteorDropTarget.position.x, meteorsParent.position.y);
            attackWarningSign.Warn(meteorDropTarget, warningToDropTime, () =>
            {
                newMeteor.DropMeteor(this);
            });
            
        }

        public void DestroyedMeteor(BossMeteor bossMeteor)
        {
            _currentDroppedMeteorCount++;
            if (_currentDroppedMeteorCount >= meteorDroppingTimes)
            {
                _currentDroppedMeteorCount = 0;
                StartDropping(false);
                runOutOfMeteorsEvent.Invoke();
            }
            else
            {
                _currentDroppingWaitTime = 0;
                StartDropping(true);
            }
        }
    }
}