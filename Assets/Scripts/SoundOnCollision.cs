using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour {

    [SerializeField]
    private AudioSource _audio;

    void OnCollisionEnter(Collision col)
    {
        _audio.Play();
    }

}
