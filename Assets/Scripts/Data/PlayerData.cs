
[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int lastLevelID;
    public int lastGameInLevelID;
    public float totalPlayTime;
    public float avgCompletitionEachGame;
    //others data

    public PlayerData()
    {
        
    }

    public PlayerData(string _playerName)
    {
        playerName = _playerName;
    }
}


