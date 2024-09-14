using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossMeteorDropper : MonoBehaviour
    {
        [SerializeField] private Transform meteorDropTarget;
        [SerializeField] private GameObject meteorPrefab;
        [SerializeField] private GameObject warningSign;
        [SerializeField] private int meteorDroppingTimes = 3;
        [SerializeField] private float droppingInterval = 2f;
        [SerializeField] private float warningToDropTime = 1f;

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
                _currentDroppedMeteorCount = 0;
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
            _currentDroppingWaitTime = 0;
            
        }

        public void DestroyedMeteor(BossMeteor bossMeteor)
        {
            
        }
    }
}