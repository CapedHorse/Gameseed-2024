using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Components.Objects.SpecificObjects.Boss
{
    public class BossFighter : MonoBehaviour
    {
        [SerializeField] private Transform attackedTarget;
        [SerializeField] private AttackWarningSign[] bossAttackWarningSign;
        [SerializeField] private BossAllSideAttacker[] bossAttackers;
        [SerializeField] private float delayBeforeAttack = 0.75f;
        [SerializeField] private float attackInterval = 3f;
        [SerializeField] private int attackCountForVulnerable = 3;

        private bool _canFight;
        private float _currentAttackTime;
        private int _currentAttackCount;

        public void CanStartFight(bool can)
        {
            _canFight = can;
        }
        private void Update()
        {
            if (!_canFight)
                return;

            _currentAttackTime += Time.deltaTime;
            if (_currentAttackTime >= attackInterval)
            {
                LaunchAttack();
            }
        }

        private void LaunchAttack()
        {
            _currentAttackTime = 0;
            
            CanStartFight(false);
            
            _currentAttackCount++;
            
            bool isVulnerable = false;
            int randomAttackerId = Random.Range(0, bossAttackers.Length);
            
            if (_currentAttackCount >= attackCountForVulnerable)
            {
                _currentAttackCount = 0;
                isVulnerable = true;
            }
            
            bossAttackers[randomAttackerId].SetTarget(attackedTarget.localPosition);
            bossAttackWarningSign[randomAttackerId].Warn(attackedTarget, delayBeforeAttack, () =>
            {
                bossAttackers[randomAttackerId].Attack(isVulnerable);
            });
        }
    }
}