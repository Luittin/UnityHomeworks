using UnityEngine;
using UnityEngine.UI;

public class FillingSelectLevelMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    private GameObject _levelPrefab;
    [SerializeField]
    private string PATH_LEVELS = "Chapters/Chapter{0}";
    [SerializeField]
    private string PATH_CHAPTER = "Assets/Resources/ChapterObjekt/Chapter{0}.asset";

    private LevelObject[] _levels;
    private ChapterObject _chapter;

    private int _chapterNumber;
    private GameData _gameData;

    private void Awake()
    {
        FillMenu();
    }

    private void FillMenu()
    {
        _chapterNumber = GameManager.Instantiate().LevelSetting.ChapterNumber;
        _gameData = GameManager.Instantiate().GameData;
        
        _levels = LoaderAssets<LevelObject>.GetAssets(string.Format(PATH_LEVELS, _chapterNumber));
        _chapter = LoaderAssets<ChapterObject>.GetAsset(string.Format(PATH_CHAPTER, _chapterNumber));
        
        foreach (LevelObject level in _levels)
        {
            SelectLevel selectLevel = CreateMenuItem.CreateSelectItem(_levelPrefab, _content).GetComponent<SelectLevel>();
            selectLevel.SetData(level._levelNumber);
            
            if (level._background >= 0)
            {
                CreateMenuItem.SetBackgroundSelectItem(_chapter._backgrounds[level._background], selectLevel.GetComponent<Image>());
            }
            
            if (_gameData.LevelsDone[_chapterNumber - 1].ComplitedLevels.Count < level._levelNumber)
            {
                CreateMenuItem.SetOptionToSelect(selectLevel.GetComponent<Button>());                          
            }
            else
            {
                int countStars = _gameData.LevelsDone[_chapterNumber - 1].ComplitedLevels[level._levelNumber - 1];
                CreateMenuItem.SetStarsSelectItem(selectLevel, countStars);
            }
        }
    }
}
