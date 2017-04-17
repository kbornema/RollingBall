using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : APickup 
{

    protected override void OnPickup()
    {
        Destroy(GameManager.Instance.Player.gameObject);

        LevelManager.Instance.RestartLevel();
    }
}
