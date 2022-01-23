using System;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _sightTransform;

    [SerializeField]
    private float _cosFDV;

    [SerializeField]
    private float _foundDelay = 0.5f;

    public Action<bool, Vector3> onFoundPlayer;

    public Transform PlayerTransform { get => _playerTransform; }

    public bool CheckForPlayer()
    {
        Vector3 targetToDot = (_playerTransform.position - _sightTransform.position).normalized;
        float targetDot = Vector3.Dot(_sightTransform.forward, targetToDot);
        bool isInSight = targetDot > _cosFDV;
        Debug.DrawRay(_sightTransform.position, targetToDot, Color.red);
        if (isInSight && Physics.Raycast(_sightTransform.position, targetToDot, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
