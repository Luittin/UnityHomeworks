using UnityEngine;
using UnityEngine.UI;

public class FillingSelectLevelMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    private GameObject _levelPrefab;

    private void Awake()
    {
        FillMenu();
    }

    private void FillMenu()
    {
        LevelObject[] levels = LoaderAssets<LevelObject>.GetAssets($"Chapters/Chapter{LevelSetting.Instantiate().ChapterNumber}");
        ChapterObject chapter = LoaderAssets<ChapterObject>.GetAsset($"Assets/Resources/ChapterObjekt/Chapter{LevelSetting.Instantiate().ChapterNumber}.asset");
        foreach (LevelObject level in levels)
        {
                SelectLevel selectLevel = Instantiate(_levelPrefab, _content).GetComponent<SelectLevel>();
                selectLevel.SetData(level._levelNumber);
                Debug.Log(level._background);
                if (level._background >= 0)
                {
                    Texture texture = chapter._backgrounds[level._background];
                    selectLevel.GetComponent<Image>().sprite = Sprite.Create((Texture2D) texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }
        }
    }
}
