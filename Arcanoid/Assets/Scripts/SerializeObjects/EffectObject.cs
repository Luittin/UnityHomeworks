using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "LevelAsset/Effect", order = 1)]
public class EffectObject : ScriptableObject
{
    public Texture _iconEffect;
    public UnityEngine.GameObject _prefubEffect;
}
