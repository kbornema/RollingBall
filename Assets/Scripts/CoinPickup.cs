using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : APickup
{
    private void Start()
    {
        GameManager.Instance.RegisterCoins(this);
    }

    protected override void OnPickup()
    {
        GameManager.Instance.PickedCoin(this);
    }
}
