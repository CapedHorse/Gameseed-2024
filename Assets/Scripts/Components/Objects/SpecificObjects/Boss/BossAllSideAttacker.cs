using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossAllSideAttacker : MonoBehaviour
    {
        [SerializeField] private GameObject attackPoint;
        [SerializeField] private float speed = 1f;
        [Tooltip("0 for X, 1 for Y")][SerializeField] private int targetAxis;
        [SerializeField] private float targetXPos;
        [SerializeField] private float targetYPos;
        [SerializeField] private float retreatWaitTime = 2f;
        [SerializeField] private bool manualRetreat;
        [SerializeField] private AttackWarningSign warningSignIfManualRetreat;
        [SerializeField] private float manualRetreatWarningTime = 0.75f;
        
        public UnityEvent onAttackingEvent, onRetreatingEvent, onDoneRetreatingEvent;

        private Vector2 _initialPos;
        private Vector2 _currentTarget;

        public void SetTarget(Vector2 targetPos)
        {
            _currentTarget = targetPos;
        }
        public void Attack(bool canBeAttacked)
        {
            if (targetAxis == 0)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, _currentTarget.y);
                _initialPos = transform.localPosition;
                transform.DOLocalMoveX(targetXPos, speed).onComplete = Retreat;
            }
            else
            {
                transform.localPosition = new Vector2(_currentTarget.x, transform.localPosition.y);
                _initialPos = transform.localPosition;
                transform.DOLocalMoveY(targetYPos, speed).onComplete = Retreat;
            }

            onAttackingEvent.Invoke();
            
            attackPoint.SetActive(canBeAttacked);
        }

        public void ForceRetreat()
        {
            StopCoroutine(WaitForRetreat());
            Retreating();
        }

        public void Retreat()
        {
            _currentTarget = Vector2.zero;
            
            StartCoroutine(WaitForRetreat());
        }

        private IEnumerator WaitForRetreat()
        {
            yield return new WaitForSeconds(retreatWaitTime);
            if(!manualRetreat)
                Retreating();
            else
                warningSignIfManualRetreat.Warn(transform, manualRetreatWarningTime, Retreating);
        }

        private void Retreating()
        {
            onRetreatingEvent.Invoke();
            Vector2 targetRetreat = _initialPos;
            targetRetreat.x = targetAxis == 0 ? targetRetreat.x : transform.localPosition.x;
            targetRetreat.y = targetAxis == 0 ? transform.localPosition.y : targetRetreat.y;

            transform.DOLocalMove(_initialPos, speed).onComplete = () =>
            {
                attackPoint.SetActive(false);
                onDoneRetreatingEvent.Invoke();
            };
        }
    }
}