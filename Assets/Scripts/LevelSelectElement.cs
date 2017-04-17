using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectElement : MonoBehaviour {

    [SerializeField]
    private Button myButton;
    [SerializeField]
    private Image myButtonImage;


    [SerializeField]
    private Text levelTitleText;
    [SerializeField]
    private Text highscoreText;

    [SerializeField]
    private Image medalImage;

    [SerializeField, ReadOnly]
    private SceneRef _myScene;

    public void SetTargetLevel(SceneRef target)
    {
        this._myScene = target;

        this.levelTitleText.text =  "Level " + (target.LevelId + 1);

        if (SavegameManager.Instance.GetCurrentSavegame().IsLevelUnlocked(_myScene))
        {
            myButton.onClick.AddListener(OnClick);
        }

        else
        {
            Color c = myButtonImage.color;
            c.a = 0.5f;
            myButtonImage.color = c;
            myButton.enabled = false;
        }

        SetMedal();
    }

    private void OnClick()
    {
        LevelManager.Instance.ChangeLevel(_myScene);
    }

    private void SetMedal()
    {
        Savegame s = SavegameManager.Instance.GetCurrentSavegame();

        HighscoreTime time;

        if(s.GetHighscore(_myScene, out time))
        {
            if (time <= _myScene.GoldTime)
                medalImage.color = Color.yellow;

            else if (time <= _myScene.SilverTime)
                medalImage.color = Color.gray;

            else if (time <= _myScene.BronzeTime)
                medalImage.color = Color.red;

            else
                medalImage.color = Color.clear;

            highscoreText.text = "Rekord:\n" + time.GetDisplayString();
        }

        else
        {
            highscoreText.text = "Rekord: /";
            medalImage.color = Color.clear;
        }
    }
	
}
