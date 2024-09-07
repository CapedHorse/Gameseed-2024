using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerControl playerControl;
        public UnityEvent onTimerRunsOut, onGameStarted, onGameEnded;
        
        
        private float _currentPlayTime;
        private float _targetPlayTime;

        
        private void Update()
        {
            if (!GameSessionManager.instance.StartedGame)
                return;

            _currentPlayTime += Time.deltaTime;

            if (_currentPlayTime >= _targetPlayTime)
            {
                TimerRunsOut();
            }
        }

        virtual public void StartGame()
        {
            onGameStarted.Invoke();
        }

        virtual public void EndGame()
        {
            onGameEnded.Invoke();
        }

        virtual public void TimerRunsOut()
        {
            onTimerRunsOut.Invoke();
        }
        
        virtual public void GameSessionSuccess()
        {
            GameSessionManager.instance.EndedGameSession(GameStateType.Success);
        }

        virtual public void GameSessionFailed()
        {
            GameSessionManager.instance.EndedGameSession(GameStateType.Failed);
        }
    }
}