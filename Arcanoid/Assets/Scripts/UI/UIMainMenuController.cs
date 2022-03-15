using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField]
    private Canvas _mainMenu;
    [SerializeField]
    private Canvas _selectGameMenu;
    [SerializeField]
    private Canvas _selectLevelMenu;

    private Canvas _currentMenu;

    private void Awake()
    {
        _currentMenu = _mainMenu;
        _mainMenu.gameObject.SetActive(true);
    }

    public void OnSelectMenu(Canvas menu)
    {
        _currentMenu.gameObject.SetActive(false);
        _currentMenu = menu;
        _currentMenu.gameObject.SetActive(true);
    }
}
