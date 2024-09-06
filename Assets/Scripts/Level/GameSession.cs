using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerControl playerControl;
        public UnityEvent onGameStarted, onGameEnded;
        virtual public void StartGame()
        {
            onGameStarted.Invoke();
        }

        virtual public void EndGame()
        {
            onGameEnded.Invoke();
        }
        
        virtual public void GameSessionSuccess()
        {
            GameSessionManager.instance.EndedGameSession(GameEndType.Success);
        }

        virtual public void GameSessionFailed()
        {
            GameSessionManager.instance.EndedGameSession(GameEndType.Failed);
        }
    }
}