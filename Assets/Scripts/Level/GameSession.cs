using Components.Player_Control;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class GameSession : MonoBehaviour
    {
        public UnityEvent onTimerRunsOut, onGameStarted, onGameSuccess, onGameFailed;
        
        virtual public void StartGame()
        {
            onGameStarted.Invoke();
        }

        public void StopGame()
        {
            
        }

        virtual public void TimerRunsOut()
        {
            onTimerRunsOut.Invoke();
        }
        
        virtual public void GameSessionSuccess()
        {
            onGameSuccess.Invoke();   
            GameSessionManager.instance.EndedGameSession(GameStateType.Success);
        }

        virtual public void GameSessionRetry()
        {
            onGameFailed.Invoke();
            GameSessionManager.instance.EndedGameSession(GameStateType.Retry);
        }
    }
}