using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Color _color;

    [SerializeField]
    private LightBulletPool _lightPool;

    public void Shoot()
    {
        LightBullet lightBullet = (LightBullet)_lightPool.RequestObject();
        lightBullet.GetComponent<MeshRenderer>().material.color = _color;
        lightBullet.transform.position = transform.position;
    }
}
