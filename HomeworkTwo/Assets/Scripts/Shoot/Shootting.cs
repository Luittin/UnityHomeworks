using System;
using System.Collections.Generic;
using UnityEngine;

public class Shootting : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private InputPlayer _inputPlayer;
    [SerializeField]
    private List<Gun> _guns;

    private int _selectGun = 0;

    private Action<Sprite, int, int> OnReloadGun;

    private void Awake()
    {
        _inputPlayer.OnReload += ReloadGun;
        _inputPlayer.OnFire += Fire;
        _inputPlayer.OnMouseScrollWheel += ScrollGun;
        _inputPlayer.OnNumberButton += SelectNewGun;
        OnReloadGun += _uiManager.RefreshGunMenu;
    }

    public void Fire(ButtonState buttonState)
    {
        switch (buttonState)
        {
            case ButtonState.PressDown: _guns[_selectGun].ShootingDelay.StartShooting(); break;
            case ButtonState.PressUp: _guns[_selectGun].ShootingDelay.StopShootting(); break;
        }
    }

    public void ReloadGun(ButtonState buttonState)
    {
        switch (buttonState)
        {
            case ButtonState.PressDown: _guns[_selectGun].ShootingDelay.StartReload(); break;
            case ButtonState.PressUp: _guns[_selectGun].ShootingDelay.StopReload(); break;
        }
    }

    private void UpdateGunMenu()
    {
        OnReloadGun?.Invoke(_guns[_selectGun].Icon, _guns[_selectGun].GunStats.BulletInMagazine, _guns[_selectGun].GunStats.AllBullet);
    }

    public void SelectNewGun(int selectGun)
    {
        if (selectGun <= _guns.Count - 1)
        {
            _selectGun = selectGun;
            UpdateGunMenu();
        }
    }

    public void ScrollGun(float direction)
    {
        if (direction != 0)
            Scroll((int)Mathf.Sign(direction));
    }

    private void Scroll(int value)
    {
        int selectGun = _selectGun + value;
        if (selectGun > _guns.Count - 1)
            _selectGun = 0;
        if (selectGun < 0)
            _selectGun = _guns.Count - 1;
        UpdateGunMenu();
    }
}
