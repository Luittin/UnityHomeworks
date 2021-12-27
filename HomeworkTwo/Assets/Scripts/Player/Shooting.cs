using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private InputPlayer _inputPlayer;

    [SerializeField]
    private List<Gun> _guns;

    private int _selectGun = 0;

    private void Start()
    {
        GameManager.Instance.RefrashGunMenu(_guns[_selectGun].Icon, _guns[_selectGun].BooletMagazine, _guns[_selectGun].AllBullet);
    }

    private void Update()
    {
        if(_selectGun != _inputPlayer.SelectGun)
        {
            _selectGun = _inputPlayer.SelectGun;
            GameManager.Instance.RefrashGunMenu(_guns[_selectGun].Icon, _guns[_selectGun].BooletMagazine, _guns[_selectGun].AllBullet);
        }

        if (_inputPlayer.LightShooting)
        {
            _guns[_selectGun].StartShooting();
        }
        else
        {
            _guns[_selectGun].StopSooting();
        }

        if (_inputPlayer.Recharge)
        {
            _guns[_selectGun].RechargeGun();
        }        
    }
}
