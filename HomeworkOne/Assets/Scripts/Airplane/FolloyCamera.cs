using UnityEngine;

public class FolloyCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Vector3 _indent;
    [SerializeField]
    private Vector3 _identRotation;

    private Transform _observer;

    private void Start()
    {
        _observer = new GameObject().transform;
        _observer.parent = _player;
        _observer.position = _player.position + _indent;
        _observer.rotation = Quaternion.Euler(_identRotation);
    }

    private void LateUpdate()
    {
        transform.position = _observer.position;

        Vector3 observerRotation = _observer.rotation.eulerAngles;
        observerRotation.z = 0.0f;
        transform.rotation = Quaternion.Euler(observerRotation);
    }
}
