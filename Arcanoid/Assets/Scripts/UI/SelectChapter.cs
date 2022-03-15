using UnityEngine;
using UnityEngine.UI;

public class SelectChapter : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    private int _chapterNumber;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonDown);
    }

    public void SetData(int chapter)
    {
        _chapterNumber = chapter;
    }

    public void OnButtonDown()
    {
        LevelSetting.Instantiate().ChapterNumber = _chapterNumber;
    }
}
