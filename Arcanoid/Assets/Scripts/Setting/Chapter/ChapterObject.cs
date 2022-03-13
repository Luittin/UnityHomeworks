using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "CreateLevel/Chapter", order = 1)]
public class ChapterObject : ScriptableObject
{
    public int _numberChapter;
    public string _nameChapter;

    public Sprite[] _backgrounds;

    public BlockObject[] _blocks;

    public EffectObject[] _effeñts;
}
