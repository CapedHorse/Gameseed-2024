using System;
using UnityEngine;

namespace Level
{
    public class BorderManager : MonoBehaviour
    {
        public static BorderManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        public Transform 
            westBorderPoint,
            eastBorderPoint,
            northBorderPoint,
            southBorderPoint;
        
        
    }
}
