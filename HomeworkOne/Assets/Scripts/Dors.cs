using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dors : MonoBehaviour
{
    [SerializeField]
    private Transform _leftDor;
    [SerializeField]
    private Transform _rightDor;
    [SerializeField]
    private Transform _targetPlayer;


    [SerializeField, Range(0.0f,10.0f)]
    private float _speedMoveDor = 2.0f;

    private Vector2 _positionHorizontal;

    private IEnumerator courutine;

    private void Start()
    {
        _positionHorizontal = new Vector2(transform.position.x, transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector2 playerHorizontal = new Vector2(_targetPlayer.position.x, _targetPlayer.position.z);

        float distance = Vector2.Distance(playerHorizontal, _positionHorizontal);

        if (courutine == null)
        {
            if (distance < 5.0f && _leftDor.localPosition.x >= 3.0f)
            {
                courutine = MoveDors(-1);
                StartCoroutine(courutine);
            }
            if (distance > 5.0f && _leftDor.localPosition.x <= 0.0f)
            {
                courutine = MoveDors(1);
                StartCoroutine(courutine);
            }
        }
    }

    IEnumerator MoveDors(int direction)
    {
        Vector3 positionLeftDor = Vector3.zero;
        Vector3 positionRightDor = Vector3.zero;

        while (true)
        {
            positionLeftDor = _leftDor.localPosition;
            positionRightDor = _rightDor.localPosition;
            positionLeftDor.x += _speedMoveDor * direction;
            positionRightDor.x -= _speedMoveDor * direction;

            _leftDor.localPosition = positionLeftDor;
            _rightDor.localPosition = positionRightDor;

            if (_leftDor.localPosition.x <= 0.0f || _leftDor.localPosition.x >= 3.0f) StopMoveCoroutine();

            yield return new WaitForFixedUpdate();
        }        
    }

    private void StopMoveCoroutine()
    {
        Debug.Log("!!");
        StopCoroutine(courutine);
        courutine = null;
    }
}
