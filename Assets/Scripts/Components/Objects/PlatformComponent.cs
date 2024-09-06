using System;
using Components.Player_Control_Components;
using UnityEngine;

namespace Components.Objects
{
    public class PlatformComponent : ComponentBase
    {
        [SerializeField] private GameObject floorPlatform;
        [SerializeField] private Collider2D floorCol;
        [SerializeField] private LayerMask playerAboveLayer, playerBelowLayer;
        [SerializeField] private float distancePlayerToPlatform = 1f;
        [SerializeField] private Transform playerTransform;
        private bool _hasActivated;


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
                        // floorPlatform.SetActive(true);
                    }
                }
                else
                {
                    if (_hasActivated)
                    {
                        _hasActivated = false;
                        floorCol.excludeLayers = playerBelowLayer;
                        // floorPlatform.SetActive(false);
                    }
                }
            }
        }
    }
}