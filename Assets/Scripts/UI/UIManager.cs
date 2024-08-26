using System;
using System.Collections.Generic;
using UI.Panel;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        [SerializeField] private Canvas UICanvas;
        [SerializeField] private UIPanel[] panels;

        protected Dictionary<string, UIPanel> panelMap;
        
        private void Start()
        {
            
        }
    }
}
