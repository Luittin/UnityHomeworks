using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectedMenuItem : MonoBehaviour
{
    [SerializeField]
    protected Button _button;

    protected int _number;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonDown);
    }

    public virtual void SetData(int number)
    {
        _number = number;
    }

    public virtual void OnButtonDown()
    {
        LevelSetting.Instantiate().LevelNumber = _number;
    }
}
