using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector2 startPosition = new Vector2(-300.0f, 740.0f);

        ChapterObject[] chapters = Resources.LoadAll<ChapterObject>("ChapterObjekt");
        foreach (ChapterObject chapter in chapters)
        {
            int countLevel = chapter.CountLevel;
            for (int i = 1; i <= countLevel; i++)
            {
                Vector2 position = startPosition;
                SelectLevel selectLevel = Instantiate(_levelPrefab, position, Quaternion.identity, _content).GetComponent<SelectLevel>();
                selectLevel.SetLevelData(chapter._numberChapter, i);
            }
        }
    }
}
