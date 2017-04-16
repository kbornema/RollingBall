using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnClick : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}

    private void OnClick()
    {
        SoundManager.Instance.PlayButtonSound();
    }
	
	
}
