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
        public float timeEachPlay = 15f;
        public int countDownTime = 3;
        public float delayBeforeRealPlay = 1.5f;
        [FormerlySerializedAs("delayWhenShowingDelay")] public float delayWhenShowingState = 2f;
        public List<LevelSettings> levelList;
    }
}