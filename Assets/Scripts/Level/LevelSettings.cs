using System.Collections.Generic;
using Tutorial;
using UnityEngine;

namespace Level
{
    [System.Serializable]
    public class LevelSettings
    {
        public string levelName;
        public TutorialSession tutorialLevel;
        public List<GameSession> gameSessionPrefabList;
    }
}