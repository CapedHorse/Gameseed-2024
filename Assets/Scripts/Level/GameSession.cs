using Components.Player_Control_Components;
using Core;
using UI.GameSession;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerControl playerControl;
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

        virtual public void GameSessionFailed()
        {
            onGameFailed.Invoke();
            GameSessionManager.instance.EndedGameSession(GameStateType.Retry);
        }
    }
}