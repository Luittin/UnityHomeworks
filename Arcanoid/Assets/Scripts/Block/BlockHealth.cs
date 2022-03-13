using UnityEngine;

[RequireComponent(typeof(Block), typeof(BlockStats))]
public class BlockHealth : MonoBehaviour
{
    private Block _block;
    private BlockStats _blockStats;

    private void Awake()
    {
        _block = GetComponent<Block>();
        _blockStats = GetComponent<BlockStats>();
    }

    public void DecrementHealth(int damage)
    {
        if (!_blockStats.Invulnerability)
        {
            _blockStats.Health -= damage;

            if (_blockStats.Health <= 0)
            {
                _block.DestroyBlock();
                //_block.DestroyBloc?.Invoke();
            }
        }
    }
}
