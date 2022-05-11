using UnityEngine;

public class SelectChapter : SelectedMenuItem
{
    [SerializeField]
    protected UIMainMenuController _menuController;

    [SerializeField]
    protected State _nextState;

    public void SetData(int number, UIMainMenuController menuController, State nextState)
    {
        _number = number;
        _numberSelectText.text = _number.ToString();
        _menuController = menuController;
        _nextState = nextState;
    }

    public override void OnButtonDown()
    {
        GameManager.Instantiate().LevelSetting.ChapterNumber = _number;
        Debug.Log(GameManager.Instantiate().LevelSetting.ChapterNumber);
        _menuController.ChangeState(_nextState);
    }
}
