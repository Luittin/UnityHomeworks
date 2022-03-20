using UnityEngine;

public class FillSelectChapterMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    private UnityEngine.GameObject _chapterPrefab;

    [SerializeField]
    private UIMainMenuController _menuController;

    [SerializeField]
    private State _nextState;

    private void Awake()
    {
        FillMenu();
    }

    private void FillMenu()
    {
        ChapterObject[] chapters = LoaderAssets<ChapterObject>.GetAssets("ChapterObjekt");
        foreach (ChapterObject chapter in chapters)
        {
            SelectChapter selectChapter = Instantiate(_chapterPrefab, _content).GetComponent<SelectChapter>();
            selectChapter.SetData(chapter._numberChapter, _menuController, _nextState);
        }
    }
}
