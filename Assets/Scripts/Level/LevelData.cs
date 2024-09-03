using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [System.Serializable]
    public class LevelData
    {
        public string levelCutsceneName;
        public string levelTutorialSceneName;
        public List<GameSessionData> gameSessionList;
    }
}