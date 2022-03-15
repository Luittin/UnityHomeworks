using UnityEngine;

public class FillSelectChapterMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    private GameObject _chapterPrefab;

    private void Awake()
    {
        FillMenu();
    }

    private void FillMenu()
    {
        Vector2 startPosition = new Vector2(0.0f, 530.0f);

        ChapterObject[] chapters = Resources.LoadAll<ChapterObject>("ChapterObjekt");
        for (int i = 0; i < chapters.Length; i++)
        {
            Vector2 position = Vector2.zero;
            position.y = startPosition.y - 530.0f * i;
            SelectChapter chapter = Instantiate(_chapterPrefab, position, Quaternion.identity, _content).GetComponent<SelectChapter>();
            chapter.SetData(chapters[i]._numberChapter);
        }
    }
}
