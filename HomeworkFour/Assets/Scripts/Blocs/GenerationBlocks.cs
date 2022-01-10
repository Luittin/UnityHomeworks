using System.Collections;
using UnityEngine;

public class GenerationBlocks : MonoBehaviour
{
    [SerializeField]
    private Transform _level;
    [SerializeField]
    private float _spawnDelay;
    [SerializeField]
    private Vector2 _generationPosition;

    [SerializeField]
    private BlocksManager _blocksManager;

    private Coroutine _generationBlocks;

    private void Start()
    {
        _generationBlocks = StartCoroutine(GenerationBlock());
    }

    private IEnumerator GenerationBlock()
    {
        while (true)
        {
            CreateBlock();
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }

    private void CreateBlock()
    {

        Blocks block = _blocksManager.RequestObject();
        block.transform.position = _generationPosition;
        block.transform.parent = _level;
        block.OnEndLifetime = () => { ReturnBlockToPool(block); };
    }

    private void ReturnBlockToPool(Blocks block)
    {
        _blocksManager.ReturnObjectToPool(block);
    }
}
