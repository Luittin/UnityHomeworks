using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting
{
    private static LevelSetting levelSetting = null;

    private int _levelNumber;
    private int _chapterNumber;
    private UnityEngine.GameObject _platform;
    private UnityEngine.GameObject _ball;

    private LevelSetting()
    {

    }

    public int LevelNumber { get => _levelNumber; set => _levelNumber = value; }
    public int ChapterNumber { get => _chapterNumber; set => _chapterNumber = value; }
    public UnityEngine.GameObject Platform { get => _platform; set => _platform = value; }
    public UnityEngine.GameObject Ball { get => _ball; set => _ball = value; }

    public static LevelSetting Instantiate()
    {
        if(levelSetting == null)
        {
            return levelSetting = new LevelSetting();
        }

        return levelSetting;
    }
}
