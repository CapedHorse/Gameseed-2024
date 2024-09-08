using System;
using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class PlatformComponent : ComponentBase
    {
        [SerializeField] private Collider2D floorCol;
        [SerializeField] private LayerMask playerAboveLayer, playerBelowLayer;
        [SerializeField] private float distancePlayerToPlatform = 1f;
        [SerializeField] private Transform playerTransform;
        private bool _hasActivated;

        public UnityEvent onPlatformStepped, onPlatformLeft;

        private void Update()
        {
            if (playerTransform)
            {
                if (playerTransform.position.y >= transform.position.y + distancePlayerToPlatform)
                {
                    if (!_hasActivated)
                    {
                        _hasActivated = true;
                        floorCol.excludeLayers = playerAboveLayer;
                    }
                }
                else
                {
                    if (_hasActivated)
                    {
                        _hasActivated = false;
                        floorCol.excludeLayers = playerBelowLayer;
                    }
                }
            }
        }
    }
}