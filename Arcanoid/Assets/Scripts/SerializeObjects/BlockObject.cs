using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "LevelAsset/Block", order = 1)]
public class BlockObject : ScriptableObject
{
    public Texture _iconBlock;
    public GameObject _prefabBlock;
}
