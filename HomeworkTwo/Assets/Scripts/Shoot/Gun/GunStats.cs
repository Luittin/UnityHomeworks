using UnityEngine;

public delegate void BolletHandler(int countBollet, int countAllBollet);
public delegate void ReloadGunHandler();

public class GunStats : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int _bolletInMagazine;
    [SerializeField]
    private int _maxBolletInMagazine = 10;
    [SerializeField]
    private int _allBollet = 100;

    private bool isReloadGun = false;

    public int BolletInMagazine { get => _bolletInMagazine; set { _bolletInMagazine = value; OnMagazineHandler?.Invoke(_bolletInMagazine, _allBollet); } }
    public int AllBollet { get => _allBollet; set { _allBollet = value; OnAllBolletHandler?.Invoke(_bolletInMagazine, _allBollet); } }
    public bool IsReloadGun { get => isReloadGun; set { isReloadGun = value; if(isReloadGun) OnReloadGunHandler?.Invoke(); } }

    public int Damage { get => damage; set => damage = value; }

    public event BolletHandler OnMagazineHandler;
    public event BolletHandler OnAllBolletHandler;
    public event ReloadGunHandler OnReloadGunHandler;
    
    public void Setup(ShootingDelay shootingDelay)
    {
        OnReloadGunHandler += shootingDelay.StartReload;
        UIManager uiManager = FindObjectOfType<UIManager>();
        OnMagazineHandler += uiManager.RefreshGunMenu;
        OnAllBolletHandler += uiManager.RefreshGunMenu;
        BolletInMagazine = _maxBolletInMagazine;
    }

    public void RechargeGun()
    {
        if (_bolletInMagazine == _maxBolletInMagazine)
        {
            return;
        }
        if (_bolletInMagazine != 0)
        {
            _allBollet += _bolletInMagazine;            
            _bolletInMagazine = 0;
        }
        if (_allBollet >= _maxBolletInMagazine)
        {
            BolletInMagazine = _maxBolletInMagazine;
            AllBollet -= _maxBolletInMagazine;
        }
        else if (_allBollet > 0)
        {
            BolletInMagazine = _allBollet;
            AllBollet = 0;
        }
    }

}
