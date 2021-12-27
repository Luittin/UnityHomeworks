using UnityEngine;

public class HardGun : Gun
{
    [SerializeField, Range(0.0f, 10.0f)]
    private float _radiusShooting = 0.5f;
    [SerializeField, Range(1.0f, 10.0f)]
    private float _maxDistance = 5.0f;

    protected override void Shoot()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, _radiusShooting, transform.forward, _maxDistance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Health>().Healths -= DamageValue;
            }
        }
    }
}
