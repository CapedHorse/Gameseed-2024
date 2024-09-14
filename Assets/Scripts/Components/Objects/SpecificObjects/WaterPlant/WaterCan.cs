using System;
using System.Collections;
using Components.Player_Control;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.WaterPlant
{
    public class WaterCan : ComponentBase
    {
        [SerializeField] private float rotateValue = 25f;
        [SerializeField] private GameObject[] waters;
        [SerializeField] private float waterDownInterval = 0.25f;
        
        public UnityEvent onWaterCanHit, onWateredDownEvent, onWateredDownComplete;
       
        private int _waterCount;
        private bool _wateredAlready;

        protected override void EnteredCollision(Collision2D other)
        {
            PlatformingControl platformer = other.gameObject.GetComponent<PlatformingControl>();
            if (platformer)
            {
        
                    if (_wateredAlready)
                        return;
                    
                    Vector3 currentEuler = transform.eulerAngles;
                    currentEuler.z -= rotateValue;
                    transform.DORotate(currentEuler, 0.25f);

                    onWaterCanHit.Invoke();
                
            }
        }

        public void StarWatering()
        {
            _wateredAlready = true;
            StartCoroutine(GraduallyWatering());
        }

        private IEnumerator GraduallyWatering()
        {
            foreach (var water in waters)
            {
                water.transform.DOScale(0, 0);
                water.SetActive(true);
                water.transform.DOScale(1, 0.5f);
                onWateredDownEvent.Invoke();
                yield return new WaitForSeconds(waterDownInterval);
            }
            transform.DORotate(Vector3.zero, 1f);
        }

        public void CheckWater()
        {
            waters[_waterCount].SetActive(false);
            
            _waterCount++;

            if (_waterCount >= waters.Length)
            {
                onWateredDownComplete.Invoke();
            }
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);

            foreach (var water in waters)
            {
                DOTween.Kill(water.transform);
            }
        }
    }
}