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
            Texture texture = chapter._backgrounds[0];
            selectChapter.GetComponent<Image>().sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}
