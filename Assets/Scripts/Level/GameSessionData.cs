using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    [System.Serializable]
    public class GameSessionData
    {
        public string gameSessionSceneName = "";
        public EGameSessionType gameType = EGameSessionType.Gameplay;
        public float gameSessionTime = 10f;
    }

    public enum EGameSessionType
    {
        Gameplay, Tutorial
    }
}