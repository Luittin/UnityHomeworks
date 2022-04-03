using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private float _muveSpeed = 2.0f;
    [SerializeField]
    private float _destroyPosition = 8.0f;

    [SerializeField]
    private Vector2 _numberPresetBlock;

    private int _numberEffect;
    private TargetEffect _targetEffect;
    
    public int NumberEffect { get => _numberEffect; set => _numberEffect = value; }

    public TargetEffect TargetEffect{ get => _targetEffect; set => _targetEffect = value; }
    
    public Vector2 NumberPresetBlock { get => _numberPresetBlock; set => _numberPresetBlock = value; }
    
    private void Update()
    {
        Vector2 position = transform.position;

        position.y -= _muveSpeed * Time.deltaTime;

        if(position.y < _destroyPosition)
        {
            Destroy(this.gameObject);
        }
    }
}
