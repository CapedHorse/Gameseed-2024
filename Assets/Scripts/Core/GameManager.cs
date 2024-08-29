using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            
            Destroy(gameObject);
        }

        public void StartStoryMode()
        {
            
        }

        public void StartFreeMode(int levelId)
        {
            
        }
    }
}
