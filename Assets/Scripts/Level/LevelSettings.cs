using System.Collections.Generic;
using Tutorial;
using UnityEngine;

namespace Level
{
    [System.Serializable]
    public class LevelSettings
    {
        public string levelName;
        public bool haveTutorial;
        public string tutorialSceneName = "Tutorial_1";
        public bool haveHeart = true;
        public List<GameSessionSettings> gameSessions;
    }

    [System.Serializable]
    public class GameSessionSettings
    {
        public string gameSessionName;
        public bool haveTime = true;
        public float playTimer = 10f;
    }
}