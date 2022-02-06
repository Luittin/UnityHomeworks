using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGun : MonoBehaviour
{
    [SerializeField]
    private float _shootDelay = 1.0f;

    [SerializeField]
    private Gun[] _guns;

    private Coroutine _shoot;

    public void StartShoot()
    {
        _shoot = StartCoroutine(Shooting());
    }

    public void StopShoot()
    {
        if(_shoot != null)
        {
            StopCoroutine(_shoot);
            _shoot = null;
        }
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            foreach(Gun gun in _guns)
            {
                gun.Shoot();
            }

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

}
