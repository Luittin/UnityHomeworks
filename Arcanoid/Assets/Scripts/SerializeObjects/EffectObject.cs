using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetEffect{
    Platform,
    Ball
}

[CreateAssetMenu(fileName = "Effect", menuName = "LevelAsset/Effect", order = 1)]
public class EffectObject : ScriptableObject
{
    public Texture _iconEffect;
    public TargetEffect _target;
    public GameObject _prefabBonus;
    public GameObject _prefabEffect;
}
