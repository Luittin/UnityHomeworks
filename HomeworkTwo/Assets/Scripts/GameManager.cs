using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager = null;
    
    public static GameManager Instance { get => _gameManager; }
    public int Score { get => _score; set { _score = value; _uiManager.UpdateScore(Score); } }

    [SerializeField]
    private UIManager _uiManager;

    private int _score = 0;

    private void Awake()
    {
        if(_gameManager == null)
        {
            _gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Score = 0;
    }

    public void EnemyKilled(int addScore)
    {
        Score += addScore;
    }

    public void ChangeHealth(int health)
    {
        _uiManager.UpdateHealth(health);

        if(health <= 0)
        {
            _uiManager.OpenDethMenu();
        }
    }

    public void RefrashGunMenu(Sprite iconGun, int currentBullet, int maxBullet)
    {
        _uiManager.RefrashGunMenu(iconGun, currentBullet, maxBullet);
    }

    public void RefrashGunMenu(int currentBullet, int maxBullet)
    {
        _uiManager.RefrashGunMenu(currentBullet, maxBullet);
    }
}
