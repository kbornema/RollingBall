using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateOnClick : MonoBehaviour {


    [SerializeField]
    private GameObject objectToActivate;

	// Use this for initialization
	void Start () 
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        objectToActivate.SetActive(!objectToActivate.activeSelf);
    }
	
}
