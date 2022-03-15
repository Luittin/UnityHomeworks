using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectLevel : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    private int _levelNumber;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonDown);
    }

    public void SetData(int level)
    {
        _levelNumber = level;
    }

    public void OnButtonDown()
    {
        LevelSetting.Instantiate().LevelNumber = _levelNumber;
    }
}
