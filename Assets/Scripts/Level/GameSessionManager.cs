using System;
using UnityEngine;

namespace Level
{
    public class GameSessionManager : MonoBehaviour
    {
        public static GameSessionManager instance;
        private bool startGame;
        private float currentPlayTime;
        private float targetPlayTime;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        public virtual void InitializeGame(GameSessionData gameSesData)
        {
            
        }

        private void Update()
        {
            if (!startGame)
                return;

            currentPlayTime += Time.deltaTime;

            if (currentPlayTime >= targetPlayTime)
            {
                //Will make the game session fail
            }
        }

        public virtual void FailedGameSession()
        {
            
        }

        public virtual void FinishGameSession()
        {
            
        }
        
        
    }
}
