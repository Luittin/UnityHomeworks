using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, _speed));
    }
}
