using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private Vector3 _spaunPosition;
    [SerializeField, Range(0.0f, 10.0f)]
    private float _radiusSpaun;

    // Update is called once per frame
    private void Update()
    {
        
    }
}
