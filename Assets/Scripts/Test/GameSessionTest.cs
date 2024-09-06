using System;
using System.Collections;
using Level;
using UnityEngine;
using UnityEngine.Events;

namespace Test
{
    public class GameSessionTest : MonoBehaviour
    {
        [SerializeField] private GameSession testedGameSession;
        [SerializeField] private float testingDelayTime;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(testingDelayTime);
            if (testedGameSession)
            {
                testedGameSession.StartGame();
            }   
        }
    }
}