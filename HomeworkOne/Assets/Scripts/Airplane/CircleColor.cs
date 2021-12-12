using UnityEngine;

public class CircleColor : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private MeshRenderer _renderer;

    private Material _material;

    private float _h;
    private float _s;
    private float _v;

    private void Start()
    {
        _material = _renderer.materials[1];
        Color.RGBToHSV(_material.color,out _h, out _s,out _v);
    }

    private void Update()
    {
        _h = _h >= 1 ? 0 : Mathf.Clamp(_h + _speed * Time.deltaTime, 0.0f, 1.0f);
        _material.color = Color.HSVToRGB(_h, _s, _v);
    }
}
