using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "CreateLevel/Elements/Block", order = 1)]
public class BlockObject : ScriptableObject
{
    public Sprite _spriteBlock;
    public BlockStats _blockPrefab;
}
