using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonElement : MonoBehaviour
{
    [SerializeField]
    private UIMainMenuController _menuController;
    [SerializeField]
    private State _nextState;

    private event Action<State> ClickButton;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        ClickButton = _menuController.OnSelectMenu;
    }

    private void OnButtonClick()
    {
        ClickButton?.Invoke(_nextState);
    }
}
