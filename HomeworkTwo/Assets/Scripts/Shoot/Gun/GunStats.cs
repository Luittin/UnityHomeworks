using UnityEngine;

public delegate void BulletHandler(int countBullet, int countAllBullet);
public delegate void ReloadGunHandler();

public class GunStats : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int _bulletInMagazine;
    [SerializeField]
    private int _maxBulletInMagazine = 10;
    [SerializeField]
    private int _allBullet = 100;

    private bool isReloadGun = false;

    public int BulletInMagazine { get => _bulletInMagazine; set { _bulletInMagazine = value; OnMagazineHandler?.Invoke(_bulletInMagazine, _allBullet); } }
    public int AllBullet { get => _allBullet; set { _allBullet = value; OnAllBulletHandler?.Invoke(_bulletInMagazine, _allBullet); } }
    public bool IsReloadGun { get => isReloadGun; set { isReloadGun = value; if(isReloadGun) OnReloadGunHandler?.Invoke(); } }

    public int Damage { get => damage; set => damage = value; }

    public event BulletHandler OnMagazineHandler;
    public event BulletHandler OnAllBulletHandler;
    public event ReloadGunHandler OnReloadGunHandler;
    
    public void Setup(ShootingDelay shootingDelay)
    {
        OnReloadGunHandler += shootingDelay.StartReload;
        UIManager uiManager = FindObjectOfType<UIManager>();
        OnMagazineHandler += uiManager.RefreshGunMenu;
        OnAllBulletHandler += uiManager.RefreshGunMenu;
        BulletInMagazine = _maxBulletInMagazine;
    }

    public void RechargeGun()
    {
        if (_bulletInMagazine == _maxBulletInMagazine)
        {
            return;
        }
        if (_bulletInMagazine != 0)
        {
            _allBullet += _bulletInMagazine;            
            _bulletInMagazine = 0;
        }
        if (_allBullet >= _maxBulletInMagazine)
        {
            BulletInMagazine = _maxBulletInMagazine;
            AllBullet -= _maxBulletInMagazine;
        }
        else if (_allBullet > 0)
        {
            BulletInMagazine = _allBullet;
            AllBullet = 0;
        }
    }

}
