using System;
using Components.Player_Control_Components;
using UnityEngine;

namespace Components.Objects
{
    public class PlatformComponent : ComponentBase
    {
        [SerializeField] private GameObject floorPlatform;
        [SerializeField] private float distancePlayerToPlatform = 1f;
        private bool _hasActivated;


        private void Update()
        {
            if (PlayerControl.instance.transform.position.y >= transform.position.y + distancePlayerToPlatform)
            {
                if (!_hasActivated)
                {
                    _hasActivated = true;
                    floorPlatform.SetActive(true);
                }
            }
            else
            {
                if (_hasActivated)
                {
                    _hasActivated = false;
                    floorPlatform.SetActive(false);
                }
            }
        }
    }
}