using UnityEngine;

public class FillingSelectLevelMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    private UnityEngine.GameObject _levelPrefab;

    private void Awake()
    {
        FillMenu();
    }

    private void FillMenu()
    {
        LevelObject[] levels = LoaderAssets<LevelObject>.GetAssets($"Chapters/Chapter{LevelSetting.Instantiate().ChapterNumber}");
        foreach (LevelObject level in levels)
        {
                SelectLevel selectLevel = Instantiate(_levelPrefab, _content).GetComponent<SelectLevel>();
                selectLevel.SetData(level._levelNumber);
        }
    }
}
