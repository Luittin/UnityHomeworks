using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "LevelAsset/Chapter", order = 1)]
public class ChapterObject : ScriptableObject
{
    public int _numberChapter;
    public string _nameChapter;
    public Texture[] _backgrounds;
    public BlockObject[] _blocks;
    public EffectObject[] _effects;

    private int _countLevel = 0;

    public int CountLevel { get => _countLevel; set => _countLevel = value; }
}
