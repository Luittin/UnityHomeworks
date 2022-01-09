using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private TextMeshProUGUI _bulletText;
    [SerializeField]
    private Image _iconGun; 
    [SerializeField]
    private Canvas _gameMenu;
    [SerializeField]
    private Canvas _deathMenu;

    public void UpdateScore(int newScore)
    {
        _scoreText.text = $"Score:{newScore}";
    }

    public void UpdateHealth(int newHealth)
    {
        _healthText.text = $"{newHealth}";
    }

    public void OpenDeathMenu()
    {
        Time.timeScale = 0;
        _gameMenu.gameObject.SetActive(false);
        _deathMenu.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void RefreshGunMenu(Sprite iconGun, int currentBullet, int maxBullet)
    {
        RefreshGunMenu(currentBullet, maxBullet);
        _iconGun.sprite = iconGun;
    }

    public void RefreshGunMenu(int currentBullet, int maxBullet)
    {
        _bulletText.text = $"{ currentBullet}/{maxBullet}";
    }

    public void OnRestartButtonPress()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
