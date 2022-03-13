using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelAsset/Level", order = 1)]
public class LevelObject : ScriptableObject
{
    public ChapterObject _chapter;
    public int _levelNumber;

    public int _background;
    public string[,] _presetBlock;
}
