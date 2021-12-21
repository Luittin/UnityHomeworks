using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private InputPlayer _inputPlayer;

    [SerializeField]
    private List<IGun> _guns;

    private void Update()
    {
        if (_inputPlayer.LightShooting)
        {
            _guns[0].StartShooting();
        }
        else
        {
            _guns[0].StopSooting();
        }
        if (_inputPlayer.HardShooting)
        {
            _guns[1].StartShooting();
        }
        else
        {
            _guns[1].StopSooting();
        }
    }
}
