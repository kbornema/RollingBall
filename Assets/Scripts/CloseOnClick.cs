using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseOnClick : MonoBehaviour 
{
	private void Start () 
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}

    private void OnClick()
    {
        Application.Quit();
    }
}
