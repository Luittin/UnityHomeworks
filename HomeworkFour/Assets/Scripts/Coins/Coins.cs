using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    private int _countCoins = 1;

    private Action<int> onPickedCoin;

    public Action<int> OnPickedCoin { set => onPickedCoin = value; }

    private void Awake()
    {
        onPickedCoin += GameManager.Instance().OnAddCoins;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            onPickedCoin(_countCoins);
            gameObject.SetActive(false);
        }
    }
}
