using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] 
    private Image[] _stars;
    
    [SerializeField]
    private string[] _endGameTexts;
    
    [SerializeField] 
    private TextMeshProUGUI _endText;

    private void Awake()
    {
        EnableStarsImage();
    }

    private void ReloadMenu()
    {
        EnableStarsImage();
    }
    
    private void EnableStarsImage()
    {
        foreach (Image star in _stars)
        {
            star.enabled = false;
        }
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        ShowEndGameText(1);
    }

    public void ShowMenu(int numberText, int countStars)
    {
        gameObject.SetActive(true);
        ShowEndGameText(numberText);
        ShowStars(countStars);
    }

    public void HideMenu()
    {
        ReloadMenu();
        gameObject.SetActive(false);
    }
    
    private void ShowEndGameText(int numberTextEnd)
    {
        _endText.text = _endGameTexts[numberTextEnd];
    }
    
    private void ShowStars(int countShowStars)
    {
        for (int i = 0; i < countShowStars; i++)
        {
            _stars[i].enabled = true;
        }
    }
        
    public void OnRetryGame(){
    
    }
    
    public void OnReloadGame(){
        ReloadMenu();
    }    
}
