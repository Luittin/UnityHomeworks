using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _healthText;

    [SerializeField]
    private Canvas _gameMenu;
    [SerializeField]
    private Canvas _dethMenu;

    public void UpdateScore(int newScore)
    {
        _scoreText.text = $"Score:{newScore}";
    }

    public void UpdateHealth(int newHealth)
    {
        _healthText.text = $"{newHealth}";
    }

    public void OpenDethMenu()
    {
        Time.timeScale = 0;
        _gameMenu.gameObject.SetActive(false);
        _dethMenu.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnRestartButtonPress()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
