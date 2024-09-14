using System;
using DG.Tweening;
using UnityEngine;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossAllSideAttacker : MonoBehaviour
    {
        [SerializeField] private GameObject attackPoint;
        [SerializeField] private float speed = 1f;
        [Tooltip("0 for X, 1 for Y")][SerializeField] private int targetAxis;
        [SerializeField] private float targetXPos;
        [SerializeField] private float targetYPos;

        private Vector2 _initialPos;

        public void Attack(Transform target, bool canBeAttacked)
        {
            _initialPos = transform.localPosition;
            
            if (targetAxis == 0)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, target.localPosition.y);
                transform.DOLocalMoveX(targetXPos, speed);
            }
            else
            {
                transform.localPosition = new Vector2(target.localPosition.x, transform.localPosition.y);
                transform.DOLocalMoveY(targetYPos, speed);
            }

            attackPoint.SetActive(canBeAttacked);
        }

        public void Retreat()
        {
            attackPoint.SetActive(false);
            transform.DOLocalMove(_initialPos, speed);
        }
    }
}