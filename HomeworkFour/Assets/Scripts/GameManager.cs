using UnityEngine;

public class GameManager
{
    private static GameManager instance = null;

    private int _countCoins = 0;

    public int CountCoins { set { _countCoins = value; } }

    public static GameManager Instance() 
    {
        if (instance == null)
            return instance = new GameManager();
        return instance;
    }

    public void OnAddCoins(int coins)
    {
        _countCoins += coins;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }
}
