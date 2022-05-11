using UnityEngine;
using UnityEngine.UI;

public class FillSelectChapterMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    private GameObject _chapterPrefab;
    
    [SerializeField]
    private UIMainMenuController _menuController;

    [SerializeField]
    private State _nextState;
    
    [SerializeField]
    private string PATH_CHAPTERS = "ChapterObjekt";

    private ChapterObject[] _chapters;
    private GameData _gameData;

    private void Awake()
    {
        FillMenu();
    }

    private void FillMenu()
    {
        _chapters = LoaderAssets<ChapterObject>.GetAssets(PATH_CHAPTERS);
        _gameData = GameManager.Instantiate().GameData;

        foreach (ChapterObject chapter in _chapters)
        {
            SelectChapter selectChapter = CreateMenuItem.CreateSelectItem(_chapterPrefab, _content).GetComponent<SelectChapter>();
            selectChapter.SetData(chapter._numberChapter, _menuController, _nextState);

            CreateMenuItem.SetBackgroundSelectItem(chapter._backgrounds[0], selectChapter.GetComponent<Image>());

            if(_gameData.LevelsDone.Count < chapter._numberChapter)
            {
                CreateMenuItem.SetOptionToSelect(selectChapter.GetComponent<Button>());
            }
        }
    }
}
