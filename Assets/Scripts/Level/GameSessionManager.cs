using System;
using System.Collections.Generic;
using Core;
using UI.GameSession;
using UnityEngine;

namespace Level
{
    public class GameSessionManager : MonoBehaviour
    {
        public static GameSessionManager instance;

        private GameUIManager _gameUIManager;
        private GameSession _currentGameSession;
        private int _currentGameLevelId;
        private int _currentGameSessionId = 0;
        private bool _startGame;
        public bool StartedGame => _startGame;

        private int _playerHealth;
        public int PlayerHeath => _playerHealth;
        
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
            _startGame = false;
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


        public void LoadGameSession(GameSession gameSessionPrefab)
        {
            _currentGameSession = Instantiate(gameSessionPrefab, transform);
            GameManager.instance.FreezeTime();
        }

        public void InitializeGameSession(int levelId)
        {
            _currentGameLevelId = levelId;
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            _playerHealth = gameSettings.playerHealthEachLevel;
            LoadGameSession(gameSettingsLevel.gameSessionPrefabList[_currentGameSessionId]);
        }

        public void EndedGameSession(GameStateType type)
        {
            
        }
    }

    public enum GameStateType
    {
        Begin = 0,
        Success,
        Failed
    }
}
