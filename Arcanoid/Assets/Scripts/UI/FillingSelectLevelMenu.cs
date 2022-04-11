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
        int chapterNumber = LevelSetting.Instantiate().ChapterNumber;
        LevelObject[] levels = LoaderAssets<LevelObject>.GetAssets($"Chapters/Chapter{chapterNumber}");
        ChapterObject chapter = LoaderAssets<ChapterObject>.GetAsset($"Assets/Resources/ChapterObjekt/Chapter{chapterNumber}.asset");
        foreach (LevelObject level in levels)
        {
                SelectLevel selectLevel = Instantiate(_levelPrefab, _content).GetComponent<SelectLevel>();
                selectLevel.SetData(level._levelNumber);
                
                if (level._background >= 0)
                {
                    Texture texture = chapter._backgrounds[level._background];
                    selectLevel.GetComponent<Image>().sprite = Sprite.Create((Texture2D) texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }
            
            if (LevelSetting.Instantiate().GameData.LevelsDone[chapterNumber - 1].ComplitedLevels.Count < level._levelNumber)
            {
                Button select = selectLevel.GetComponent<Button>();
                ColorBlock colorBlock = select.colors;
                colorBlock.normalColor = new Color(0.0f, 0.0f, 0.0f);
                select.colors = colorBlock;
                select.interactable = false;                             
            }
            else
            {
                selectLevel.SetStars(LevelSetting.Instantiate().GameData.LevelsDone[chapterNumber - 1].ComplitedLevels[level._levelNumber - 1]);
            }
        }
    }
}
