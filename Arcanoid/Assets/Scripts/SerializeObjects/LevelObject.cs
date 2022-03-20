using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelAsset/Level", order = 1)]
public class LevelObject : ScriptableObject
{
    public ChapterObject _chapter;
    public int _levelNumber;

    public int _background;
    public List<FieldSquare> _levelSquares = new List<FieldSquare>();
}

[Serializable]
public class FieldSquare
{
    [SerializeField] private int row;
    [SerializeField] private int colum;

    [SerializeField] private int blockNumber;
    [SerializeField] private int effectNumber;

    public int Row { get => row; set => row = value; }
    public int Colum { get => colum; set => colum = value; }

    public int BlockNumber { get => blockNumber; set => blockNumber = value; }
    public int EffectNumber { get => effectNumber; set => effectNumber = value; }
}
