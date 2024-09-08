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
        [SerializeField] private UIPanelWrapper[] panels;

        protected Dictionary<string, UIPanel> panelMap;

        protected virtual void Start()
        {
            panelMap = new Dictionary<string, UIPanel>();
            
            foreach (var panel in panels)
            {
                panelMap.Add(panel.panelName, panel.panel);
            }
        }
    }
}
