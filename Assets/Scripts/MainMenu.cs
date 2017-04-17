using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
    public static bool IsOpen = false;

    [SerializeField]
    private Button startButton;
    [SerializeField]
    private GameObject mainMenuRoot;
    [SerializeField]
    private GameObject levelSelectRoot;

    [SerializeField]
    private LevelSelectElement levelSelectElementPrefab;

    void OnEnable()
    {
        IsOpen = true;
    }

    void OnDisable()
    {
        IsOpen = false;
    }

	// Use this for initialization
	void Awake ()
    {

        startButton.onClick.AddListener(StartButtonClick);

        List<SceneRef> scenes = LevelManager.Instance.GetAllLevel();

        for (int i = 0; i < scenes.Count; i++)
        {
            if(scenes[i].IsPlayable)
            {
                LevelSelectElement elemInstance = Instantiate(levelSelectElementPrefab, levelSelectRoot.transform);
                elemInstance.SetTargetLevel(scenes[i]);
            }
        }
	}

    private void StartButtonClick()
    {
        mainMenuRoot.SetActive(false);
        levelSelectRoot.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Menu))
        {
            if(!mainMenuRoot.activeSelf)
            {
                mainMenuRoot.SetActive(true);
                levelSelectRoot.SetActive(false);
            }

            else
            {
                Application.Quit();
            }
     
        }
    }
}
