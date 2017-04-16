using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    private LevelHierarchy _levels;

    [SerializeField, ReadOnly]
    private SceneRef _currentLevel;
    public SceneRef CurrentLevel { get { return _currentLevel; } }

    [SerializeField]
    private Material _changeLevelMaterialPrefab;
    private Material _changeLevelMaterialInstance;

    [SerializeField, ReadOnly]
    private bool levelIsRunning = false;
    public bool LevelIsRunning { get { return levelIsRunning; } }

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

    public void EndLevel()
    {
        levelIsRunning = false;
        onLevelEnd.Invoke(this);

        ChangeLevel(_levels.GetLevel(_currentLevel.LevelId + 1));
    }
    
    public void ChangeLevel(SceneRef newScene)
    {
        StartCoroutine(ChangeLevelRoutine(newScene));
    }

    private IEnumerator ChangeLevelRoutine(SceneRef newScene)
    {
        _currentLevel = newScene;
        yield return FadeLevel(newScene.GetSceneName(), 1.0f, Color.black);
        StartLevel();
    }

    private IEnumerator FadeLevel(string sceneName, float fadeTime, Color inColor)
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
        
        SceneManager.LoadScene(sceneName);

        onLevelInvisible.Invoke(this);

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

    
}
