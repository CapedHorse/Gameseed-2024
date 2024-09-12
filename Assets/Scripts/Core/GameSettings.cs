using System.Collections.Generic;
using Level;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int playerHealthEachLevel = 3;
        public int countDownTime = 3;
        public float delayBeforeRealPlay = 1.5f;
        public float delayWhenShowingState = 2f;
        public List<LevelSettings> levelList;
    }
}