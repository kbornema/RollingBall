using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickScene : MonoBehaviour {

    [SerializeField]
    private SceneRef targetScene;

	// Use this for initialization
	void Start () 
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(OnClick);
	}

    private void OnClick()
    {
        if (targetScene)
        {
            LevelManager.Instance.ChangeLevel(targetScene);
        }
        
        else
        {
            LevelManager.Instance.ChangeLevel(LevelManager.Instance.CurrentLevel);
        }
    }
	
	
}
