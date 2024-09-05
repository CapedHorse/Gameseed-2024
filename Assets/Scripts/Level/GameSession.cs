using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerControl playerControl;
        
        private int currentHealth;

        public UnityEvent onGameSessionSuccess, onGameSessionFailed;

        virtual public void GameSessionSuccess()
        {
            
        }

        virtual public void GameSessionFailed()
        {
            currentHealth--;

            //Notify the UI
            if (currentHealth <= 0)
            {
                //Told the game manager to fail the game
            }
        }
    }
}