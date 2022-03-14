using UnityEngine;

public class FillLevel : MonoBehaviour
{
    [SerializeField]
    private LevelObject _level;
    [SerializeField]
    private ChapterObject _chapter;

    [SerializeField]
    private Vector2 _startFieldPoin;

    private void Awake()
    {
        _level = Resources.Load<LevelObject>($"Assets/Resources/Chapter/Level.asset");
        _chapter = _level._chapter;
    }

    private void FillGamePlane()
    {
        int countBlockX = _level._presetBlock.GetLength(0);
        int countBlockY = _level._presetBlock.GetLength(1);

        float stepInstantiateX = (_startFieldPoin.x * 2) / countBlockX;

        for (int i = 0; i < countBlockX; i++)
        {
            for(int j = 0; j < countBlockY; j++)
            {
                string[] valuePreset = _level._presetBlock[i, j].Split('|');
                int valuePresetBlock = int.Parse(valuePreset[0]);
                if(valuePresetBlock > 0)
                {
                    Vector2 createPosition = new Vector2(_startFieldPoin.x + stepInstantiateX * i, _startFieldPoin.y + stepInstantiateX * j);
                    GameObject block = Instantiate(_chapter._blocks[valuePresetBlock]._prefabBlock, createPosition, Quaternion.identity);
                    int valuePresetEffect = int.Parse(valuePreset[1]);
                    if(valuePresetEffect > 0)
                    {
                        //Add to BlockEffect
                    }
                }
            }
        }
    }
}
