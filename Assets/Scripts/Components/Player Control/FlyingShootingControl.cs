using System;
using Components.ExtraComponents;
using Lean.Pool;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Components.Player_Control_Components
{
    public class FlyingShootingControl : FlyingControl
    {
        [FormerlySerializedAs("bulletPrefab")] [SerializeField] private BulletComponent bulletComponentPrefab;
        [SerializeField] private Transform bulletHose;

        [SerializeField] private float bulletCooldown = 1;
        
        private float _currentBulletCooldown;
        private bool _hasShotBullet;

        private void Update()
        {
            if (_hasShotBullet)
            {
                _currentBulletCooldown += Time.deltaTime;

                if (_currentBulletCooldown >= bulletCooldown)
                {
                    _currentBulletCooldown = 0;
                    _hasShotBullet = false;
                }
            }
        }

        protected override void MainActionInputStarted(InputAction.CallbackContext obj)
        {
            if (_hasShotBullet)
                return;

            BulletComponent spawnedBulletComponent = LeanPool.Spawn(bulletComponentPrefab, bulletHose.position, Quaternion.identity);
            spawnedBulletComponent.Launch(bulletHose.up);
            _hasShotBullet = true;
        }
    }
}