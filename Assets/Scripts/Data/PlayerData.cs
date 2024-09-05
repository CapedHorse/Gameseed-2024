
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    
    //Level related
    public int lastLevelID;
    public List<LevelData> levelDatas;
    
    //Score related
    public float totalPlayTime;
    public float avgCompletitionEachGame;
    
    //others data

    public bool hasIntro = false;
    public bool hasOutro = false;
    
    public PlayerData()
    {
        levelDatas = new List<LevelData>();
    }

    public PlayerData(string _playerName)
    {
        playerName = _playerName;
    }
}

[System.Serializable]
public class LevelData
{
    public string levelName = "";
    public bool unlocked = false;
    public bool hasPlayed = false;
    public int lastGameId = 0;

    public LevelData()
    {
        
    }
}


