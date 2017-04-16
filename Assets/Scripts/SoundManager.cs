using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : AManager<SoundManager>
{
    [SerializeField]
    private AudioSource pickupSound;

    [SerializeField]
    private AudioSource buttonSound;

    protected override void OnAwake()
    {
        
    }

    public void PlayPickupSound()
    {
        pickupSound.Play();
    }

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }
}
