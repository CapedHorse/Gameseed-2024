using System;
using System.Collections.Generic;
using UI.GameSession;
using UnityEngine;

namespace Level
{
    public class GameSessionManager : MonoBehaviour
    {
        public static GameSessionManager instance;

        private GameUIManager _gameUIManager;

        private int _currentGameSessionId = 0;
        private bool _startGame;
        private float _currentPlayTime;
        private float _targetPlayTime;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            
            Destroy(gameObject);
        }

        private void Start()
        {
            _gameUIManager = GameUIManager.instance;
        }

        public virtual void InitializeGame(GameSessionData gameSesData)
        {
            
        }

        private void Update()
        {
            if (!_startGame)
                return;

            _currentPlayTime += Time.deltaTime;

            if (_currentPlayTime >= _targetPlayTime)
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


        public void LoadGameSession(List<GameSession> gameSessionPrefabList)
        {
            Instantiate(gameSessionPrefabList[_currentGameSessionId], transform);
        }
    }
}
