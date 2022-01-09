using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.x -= _speed * Time.deltaTime;
        transform.position = position;
    }
}
