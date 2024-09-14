using System;
using System.Collections.Generic;
using Components.ExtraComponents;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Components.Player_Control
{
    public class FlyingShootingControl : FlyingControl
    {
        [FormerlySerializedAs("bulletPrefab")] [SerializeField] private BulletComponent bulletComponentPrefab;
        [SerializeField] private Transform bulletHose;
        [SerializeField] private float bulletCooldown = 1;

        public UnityEvent onShootingEvent;
        private float _currentBulletCooldown;
        private bool _hasShotBullet;
        private List<BulletComponent> bullets = new List<BulletComponent>();

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
            if (!playerInput.inputIsActive)
                return;
            
            if (_hasShotBullet)
                return;

            BulletComponent spawnedBulletComponent = LeanPool.Spawn(bulletComponentPrefab, bulletHose.position, Quaternion.identity);
            bullets.Add(spawnedBulletComponent);
            spawnedBulletComponent.transform.rotation = bulletHose.rotation;
            spawnedBulletComponent.SetOwner(this);
            spawnedBulletComponent.Launch(bulletHose.up);
            _hasShotBullet = true;
            onShootingEvent.Invoke();
        }

        private void OnDestroy()
        {
            foreach (var bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }
        }

        public void UnregisterBullet(BulletComponent bullet)
        {
            bullets.Remove(bullet);
        }
    }
}