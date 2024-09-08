using System.Collections.Generic;
using Tutorial;

namespace Level
{
    [System.Serializable]
    public class LevelSettings
    {
        public string levelName;
        public string tutorialSceneName = "Tutorial_1";
        public List<string> gameSessionNames;
    }
}