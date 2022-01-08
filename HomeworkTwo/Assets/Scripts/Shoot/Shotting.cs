using System;
using System.Collections.Generic;
using UnityEngine;

public class Shotting : MonoBehaviour
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
        _inputPlayer.OnMouseScrollWheel += ScrolGun;
        _inputPlayer.OnNumberButton += SelectNewGun;
        OnReloadGun += _uiManager.RefreshGunMenu;
    }

    public void Fire(ButtonState buttonState)
    {
        switch (buttonState)
        {
            case ButtonState.PressDown: _guns[_selectGun].ShootingDelay.StartShooting(); break;
            case ButtonState.PressUp: _guns[_selectGun].ShootingDelay.StopShotting(); break;
        }
    }

    public void ReloadGun(ButtonState buttonState)
    {
        switch (buttonState)
        {
            case ButtonState.PressDown: _guns[_selectGun].ShootingDelay.StartReload(); break;
            case ButtonState.PressUp: _guns[_selectGun].ShootingDelay.StopReload(); break;
        }
        Debug.Log(buttonState);
    }

    private void UpdateGunMenu()
    {
        OnReloadGun?.Invoke(_guns[_selectGun].Icon, _guns[_selectGun].GunStats.BolletInMagazine, _guns[_selectGun].GunStats.AllBollet);
    }

    public void SelectNewGun(int selectGun)
    {
        if (selectGun <= _guns.Count - 1)
        {
            _selectGun = selectGun;
            UpdateGunMenu();
        }
    }

    public void ScrolGun(float direction)
    {
        if (direction != 0)
            Scrol((int)Mathf.Sign(direction));
    }

    private void Scrol(int value)
    {
        int selectGun = _selectGun + value;
        if (selectGun > _guns.Count - 1)
            _selectGun = 0;
        if (selectGun < 0)
            _selectGun = _guns.Count - 1;
        UpdateGunMenu();
    }
}
