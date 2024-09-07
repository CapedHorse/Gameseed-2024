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
        private string _currentGameSessionLevelId;
        private int _currentGameLevelId;
        public int CurrentLevelId => _currentGameLevelId;
        
        private int _completedGameSession = 0;
        public int CompletedGameSession => _completedGameSession;
        
        private bool _startGame;
        public bool StartedGame => _startGame;

        private int _playerHealth;
        public int PlayerHeath => _playerHealth;
        
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

        public virtual void FailedGameSession()
        {
            
        }

        public virtual void FinishGameSession()
        {
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            _completedGameSession++;
            if (_completedGameSession >= gameSettingsLevel.gameSessionPrefabList.Count)
            {
                //Do tell that you finished the level
            }
            else
            {
                Destroy(_currentGameSession);
                _gameUIManager.Transition(GameStateType.Success, () =>
                {
                    LoadGameSession(gameSettingsLevel.gameSessionPrefabList[_completedGameSession]);
                });    
            }
            
        }


        public void LoadGameSession(GameSession gameSessionPrefab)
        {
            _currentGameSession = Instantiate(gameSessionPrefab, transform);
        }

        public void InitializeGameSession(int levelId)
        {
            _currentGameLevelId = levelId;
            GameSettings gameSettings = GameManager.instance.gameSettings;
            LevelSettings gameSettingsLevel = gameSettings.levelList[_currentGameLevelId];
            _playerHealth = gameSettings.playerHealthEachLevel;
            GameManager.instance.FreezeTime();
            // _gameUIManager.Transition();
            LoadGameSession(gameSettingsLevel.gameSessionPrefabList[_completedGameSession]);
        }

        public void EndedGameSession(GameStateType type)
        {
            
        }
    }

    public enum GameStateType
    {
        Begin = 0,
        Success,
        Failed,
        Completed
    }
}
