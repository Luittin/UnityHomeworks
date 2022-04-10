using UnityEngine;

public class UIGameMenuController : MonoBehaviour
{
    [SerializeField] 
    private GameManager _gameManager;
    
    [SerializeField] 
    private GameObject _touchInput;
    [SerializeField] 
    private GameObject _ballSight;

    [SerializeField] 
    private UpperMenuController _upperMenuController;
    [SerializeField] 
    private PauseMenuController _pauseMenuController;

    public void ShowTouch()
    {
        _touchInput.SetActive(true);
        _ballSight.SetActive(false);
    }

    public void ShowSight()
    {
        _touchInput.SetActive(false);
        _ballSight.SetActive(true);
    }
    
    
    
    public void OpenPauseMenu()
    {
        _pauseMenuController.ShowMenu();
    }
    
    
}
