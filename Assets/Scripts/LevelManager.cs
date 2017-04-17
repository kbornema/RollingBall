using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : AManager<LevelManager> 
{
    public class Event : UnityEvent<LevelManager> { }

    [HideInInspector]
    public Event onLevelStart = new Event();
    [HideInInspector]
    public Event onLevelEnd = new Event();
    [HideInInspector]
    public Event onLevelInvisible = new Event();


    [SerializeField]
    private Text _levelNameText;

    [SerializeField]
    private LevelHierarchy _levels;

    [SerializeField, ReadOnly]
    private SceneRef _currentLevel;
    public SceneRef CurrentLevel { get { return _currentLevel; } }

    [SerializeField]
    private Material _changeLevelMaterialPrefab;
    private Material _changeLevelMaterialInstance;

    [SerializeField, ReadOnly]
    private bool levelIsRunning = false;
    public bool LevelIsRunning { get { return levelIsRunning; } set { levelIsRunning = value; } }

    [SerializeField, ReadOnly]
    private bool levelIsChanging = false;
    public bool LevelIsChanging { get { return levelIsChanging; } }

    protected override void OnAwake()
    {
        _changeLevelMaterialInstance = Instantiate(_changeLevelMaterialPrefab);

        _currentLevel = _levels.GetLevel(SceneManager.GetActiveScene().name);
        StartLevel();
    }

    private void Start()
    {

    }

    public void StartLevel()
    {
        levelIsRunning = true;
        onLevelStart.Invoke(this);
    }

    /// <summary>Only called when a level has been sucessfully finished.</summary>
    public void EndLevel()
    {
        levelIsRunning = false;
        onLevelEnd.Invoke(this);

        SavegameManager.Instance.GetCurrentSavegame().SaveLevelFinished(_currentLevel);
        SavegameManager.Instance.GetCurrentSavegame().SetHighscore(_currentLevel, GameManager.Instance.CurrentScore);

        //ChangeLevel(_levels.GetLevel(_currentLevel.LevelId + 1));
    }

    private void OnSceneInvisible()
    {
        _levelNameText.text = "Level " + (_currentLevel.LevelId + 1);

        SavegameManager.Instance.SaveCurrentSavegame();

        onLevelInvisible.Invoke(this);
    }
    
    public void ChangeLevel(SceneRef newScene)
    {
        if (levelIsChanging)
            return;

        GameManager.Instance.Pause(false);

        StartCoroutine(ChangeLevelRoutine(newScene));
    }

    private IEnumerator ChangeLevelRoutine(SceneRef newScene)
    {
        _currentLevel = newScene;
        yield return FadeLevel(newScene, 1.0f, Color.black);
        StartLevel();
    }

    private IEnumerator FadeLevel(SceneRef scene, float fadeTime, Color inColor)
    {
        levelIsChanging = true;

        float curFadeTime = 0.0f;
        float t = 0.0f;

        Color color = inColor;
        color.a = 0.0f;

        while (t < 1.0f)
        {
            yield return new WaitForEndOfFrame();

            curFadeTime += Time.deltaTime;
            t = curFadeTime / fadeTime;

            color.a = t;

            _changeLevelMaterialInstance.color = color;

            Utility.DrawScreenFilledQuad(_changeLevelMaterialInstance);
        }



        curFadeTime = fadeTime;
        t = 1.0f;
        color = inColor;
        
        SceneManager.LoadScene(scene.GetSceneName());
        OnSceneInvisible();


        while (t > 0.0f)
        {
            yield return new WaitForEndOfFrame();

            curFadeTime -= Time.deltaTime;
            t = curFadeTime / fadeTime;

            color.a = t;
            _changeLevelMaterialInstance.color = color;
            Utility.DrawScreenFilledQuad(_changeLevelMaterialInstance);
        }

        levelIsChanging = false;
    }



    public void ChangeToNextLevel()
    {
        ChangeLevel(_levels.GetLevel(_currentLevel.LevelId + 1));
    }

    public void RestartLevel()
    {
        ChangeLevel(_currentLevel);
    }

    public List<SceneRef> GetAllLevel()
    {
        return _levels.GetAllLevel();
    }
}
