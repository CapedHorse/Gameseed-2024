using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public float playerHealthEachLevel;
        public List<LevelData> levelList;
        public string lastCutsceneSceneName;
    }
}