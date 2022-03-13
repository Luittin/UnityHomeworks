using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "CreateLevel/Level", order = 1)]
public class Level : ScriptableObject
{
    public int _numberChapter;
    public int _numberLevel;
    public string _nameChapter;

    public string[,] _field;

    public int _numberBackground;
}
