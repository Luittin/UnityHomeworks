using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectedMenuItem : MonoBehaviour
{
    [SerializeField]
    protected Button _button;

    [SerializeField]
    protected TextMeshProUGUI _numberSelectText;

    protected int _number;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonDown);
    }

    public virtual void SetData(int number)
    {
        _number = number;
        _numberSelectText.text = _number.ToString();
    }

    public virtual void OnButtonDown()
    {
        LevelSetting.Instantiate().LevelNumber = _number;
    }
}
