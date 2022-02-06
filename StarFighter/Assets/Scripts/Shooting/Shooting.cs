using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private PCInputHandler _inputHandler;

    [SerializeField]
    private LightGun _lightGun;
    //[SerializeField]
    //private HardGun _hardGun;
    
    private void Awake()
    {
        _inputHandler.MouseLeft += LightShoot;
        _inputHandler.MouseHard += HardShoot;
    }

    private void LightShoot(ButtonStatus buttonStatus)
    {
        if(buttonStatus == ButtonStatus.ButtonDown)
        {
            _lightGun.StartShoot();
        }
        if(buttonStatus == ButtonStatus.ButtonUp)
        {
            _lightGun.StopShoot();
        }
    }

    private void HardShoot(ButtonStatus buttonStatus)
    {

    }
}
