using System;
using System.Collections.Generic;
using Level;
using UnityEditor;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int playerHealthEachLevel = 3;
        public float timeEachPlay = 15f;
        public float countDownTime = 3f;
        public float delayBeforeRealPlay = 1.5f;
        public List<LevelSettings> levelList;
    }
}