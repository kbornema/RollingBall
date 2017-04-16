using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearDebugText : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        DebugManager.Instance.ClearDebugText();
    }
}
