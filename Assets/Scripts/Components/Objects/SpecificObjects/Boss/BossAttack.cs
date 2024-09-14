
using System;
using Components.ExtraComponents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossAttack : ComponentBase
    {
        [SerializeField] private Transform attackTarget;
        [SerializeField] private GameObject bossBeakImpact;
        [SerializeField] private GameObject attackPoint;
        [SerializeField] private Vector2 minAttackPointPos;
        [SerializeField] private Vector2 maxAttackPointPos;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float vulnerableTime;

        public UnityEvent attackingEvent, hitEvent, retractingEvent;
        
        private Vector2 _initialPos;
        private bool _attacking;
        private bool _retreat;
        private bool _waitingToBeAttacked;
        private float _currentWaitingTime;

        private void Start()
        {
            _initialPos = transform.position;
        }

        private void Update()
        {
            if (_attacking)
            {
                transform.position = Vector3.MoveTowards(transform.position, attackTarget.position, Time.deltaTime * speed);
            }

            if (_waitingToBeAttacked)
            {
                _currentWaitingTime += Time.deltaTime;
                if (_currentWaitingTime >= vulnerableTime)
                {
                    Retreat();
                }
            }

        }

        private void SetVulnerable(bool on)
        {
            if (on)
            {
                _attacking = false;
                _waitingToBeAttacked = true;
                bossBeakImpact.SetActive(true);
                attackPoint.transform.position = new Vector2(
                    Random.Range(minAttackPointPos.x, maxAttackPointPos.x),
                    Random.Range(minAttackPointPos.y, maxAttackPointPos.y));
                attackPoint.SetActive(true);
            }
            else
            {
                _waitingToBeAttacked = false;
                bossBeakImpact.SetActive(false);
                attackPoint.SetActive(false);
                   
            }
        }
        
        
        //Sekali drop dan collide sama floor, auto stop, waiting to be attacked
        //Dalam waktu tertentu, balik lagi ke atas

        public void Attack()
        {
            _attacking = true;
            attackingEvent.Invoke();
        }

        
        private void Retreat()
        {
            SetVulnerable(false);
            transform.DOMove(_initialPos, 4);
        }

        protected override void EnteredCollision(Collision2D other)
        {
            if (other.collider.CompareTag("Floor") || other.gameObject.GetComponent<HurtComponent>())
            {
                hitEvent.Invoke();
                SetVulnerable(true);
            }
        }
    }
}
