using UnityEngine;

public class LevelSetting
{
    private int _levelNumber;
    private int _chapterNumber;

    private GameObject _platform;
    private GameObject _ball;

    public int LevelNumber { get => _levelNumber; set => _levelNumber = value; }
    public int ChapterNumber { get => _chapterNumber; set => _chapterNumber = value; }
    public GameObject Platform { get => _platform; set => _platform = value; }
    public GameObject Ball { get => _ball; set => _ball = value; }
}
