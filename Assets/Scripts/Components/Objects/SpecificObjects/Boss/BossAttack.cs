
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
        [SerializeField] private bool customizedVulnerability;

        public UnityEvent attackingEvent, hitEvent, retreatingEvent, doneRetreatingEvent, doneFleeRetreatingEvent;
        
        private Vector2 _initialPos;
        private bool _attacking;
        private bool _retreat;
        private bool _vulnerable;
        private float _currentlyVulnerableTime;
        private bool _isFlee;

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

            if (_vulnerable)
            {
                _currentlyVulnerableTime += Time.deltaTime;
                if (_currentlyVulnerableTime >= vulnerableTime)
                {
                    Retreat();
                }
            }
        }

        public void SetVulnerable(bool on)
        {
            if (on)
            {
                _attacking = false;
                _currentlyVulnerableTime = 0;
                _vulnerable = true;
                bossBeakImpact.SetActive(true);
                attackPoint.transform.localPosition = new Vector2(
                    Random.Range(minAttackPointPos.x, maxAttackPointPos.x),
                    Random.Range(minAttackPointPos.y, maxAttackPointPos.y));
                attackPoint.SetActive(true);
            }
            else
            {
                _vulnerable = false;
                bossBeakImpact.SetActive(false);
                attackPoint.SetActive(false);
            }
        }

        public void Attack()
        {
            _attacking = true;
            attackingEvent.Invoke();
        }

        public void SetFlee()
        {
            _isFlee = true;
        }
        
        public void Retreat()
        {
            SetVulnerable(false);
            retreatingEvent.Invoke();
            transform.DOMove(_initialPos, 1).SetUpdate(true).onComplete = () =>
            {
                if(_isFlee)
                    doneFleeRetreatingEvent.Invoke();
                else
                    doneRetreatingEvent.Invoke();
            };
        }

        protected override void EnteredTrigger(Collider2D other)
        {
            if (other.CompareTag("Floor"))
            {
                if (_vulnerable)
                    return;
                
                hitEvent.Invoke();
                
                if(customizedVulnerability)
                    return;
                
                SetVulnerable(true);
            }
        }
    }
}
