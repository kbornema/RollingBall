using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : APickup 
{
    [SerializeField]
    private AudioSource _audio;

    protected override void OnPickup()
    {
        _audio.Play();

        Destroy(GameManager.Instance.Player.gameObject);

        LevelManager.Instance.RestartLevel();
    }
}
