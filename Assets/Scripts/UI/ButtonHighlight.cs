using System;
using UnityEngine;

namespace UI
{
    public class ButtonHighlight : MonoBehaviour
    {
        [SerializeField] private Transform buttonHighlightImageTransform;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void MoveSelection(Transform newParent)
        {
            transform.SetParent(newParent);
            gameObject.SetActive(true);
            
            //Will tween Scale everytime
        }
    }
}