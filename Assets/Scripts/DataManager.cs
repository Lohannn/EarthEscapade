using UnityEngine;

public class DataManager
{
    public static int playerScore;
    public static int playerHighScore;
    public static int playerLife;

    public void ResetData()
    {
        playerScore = 0;
        playerLife = 3;
    }
}
