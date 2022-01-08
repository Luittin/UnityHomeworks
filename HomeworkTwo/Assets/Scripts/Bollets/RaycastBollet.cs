using System;
using UnityEngine;

public class RaycastBollet : Bollet
{
    [SerializeField, Range(0.0f, 10.0f)]
    private float _radiusShooting = 0.5f;
    [SerializeField, Range(1.0f, 10.0f)]
    private float _maxDistance = 5.0f;

    private void FixedUpdate()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, _radiusShooting, transform.forward, _maxDistance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<HealthEnemy>().Healths -= Damage;
            }
        }

        onEndLifetime.Invoke(this);
    }
}
