using UnityEngine;

public class MuveBonus : MonoBehaviour
{
    [SerializeField]
    private float _muveSpeed = 2.0f;
    [SerializeField]
    private float _destroyPosition = 8.0f;
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
