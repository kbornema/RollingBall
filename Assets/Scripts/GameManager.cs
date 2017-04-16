using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : AManager<GameManager> 
{
    [SerializeField]
    private Text _levelFinishTextField;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _nextLevelButton;
    [SerializeField]
    private GameObject _ingameUiRoot;
    [SerializeField]
    private GameObject _gravityRoot;
    [SerializeField]
    private GameObject _levelFinishRoot;
    [SerializeField]
    private float _maxWorldRotationDegree = 15.0f;
    [SerializeField]
    private Text _pointsText;
    [SerializeField]
    private Text _timeText;
    [SerializeField]
    private float _minDampSecs;
    [SerializeField]
    private float _maxDampSecs;
    [SerializeField]
    private float _endBlinkFactor = 5.0f;

    public Vector3 MoveVec { get; private set; }
    private Vector2 _movementFactors;
    public Vector2 MoveFactors { get { return _movementFactors; } }

    private int totalCoins;
    private int pickedCoins;

    private Vector3 targetUp;
    private Vector3 currentUp;
    private Vector3 helpVelocity;
    
    private HighscoreTime currentTimer;
    private float highscoreTime;    

    protected override void OnAwake()
    {
        DontDestroyOnLoad(gameObject);

        _restartButton.onClick.AddListener(OnRestart);
        _nextLevelButton.onClick.AddListener(OnNextLevel);
    }

    private void OnNextLevel()
    {
       LevelManager.Instance.ChangeToNextLevel();
    }

    private void OnRestart()
    {
        LevelManager.Instance.RestartLevel();
    }

    private void Start()
    {
        LevelManager.Instance.onLevelEnd.AddListener(OnEndLevel);
        LevelManager.Instance.onLevelStart.AddListener(OnLevelStart);
        LevelManager.Instance.onLevelInvisible.AddListener(OnLevelInvisible);

        OnLevelInvisible(LevelManager.Instance);
        OnLevelStart(LevelManager.Instance);
    }

    private void OnLevelInvisible(LevelManager l)
    {
        Clear();
        _gravityRoot.transform.rotation = Quaternion.identity;
        _ingameUiRoot.SetActive(l.CurrentLevel.IsPlayable);
        _levelFinishRoot.SetActive(false);
    }

    private void OnLevelStart(LevelManager l)
    {
        _levelFinishRoot.SetActive(false);
    }

    private void OnEndLevel(LevelManager l)
    {
        _levelFinishRoot.SetActive(true);
        _levelFinishTextField.text = "Level geschafft!\nDeine Zeit: " + currentTimer.GetDisplayString();
        MoveVec = Vector3.zero;
        _movementFactors = Vector2.zero;
        StartCoroutine(EndRoutine());
    }
    
    public void Clear()
    {
        totalCoins = 0;
        pickedCoins = 0;
        targetUp = Vector3.up;
        currentUp = targetUp;
        helpVelocity = Vector3.zero;
        highscoreTime = 0.0f;
        currentTimer.SetTime(0.0f);
        SetPointText();
        SetTimeText();
        
        _timeText.color = Color.white;
    }
    
    private IEnumerator EndRoutine()
    {
        Color c = _timeText.color;
        float time = 0.0f;

        while (!LevelManager.Instance.LevelIsRunning)
        {
            time += Time.deltaTime;
            c.a = Mathf.Sin(time * _endBlinkFactor) * 0.5f + 0.5f;

            _timeText.color = c;
            yield return new WaitForEndOfFrame();
        }
    }
     

    // Update is called once per frame
    private void Update()
    {

        if(LevelManager.Instance.LevelIsRunning)
        {
            HandleInput();

            HandleTimer();
        }

    }
    
    public void PickedCoin(CoinPickup c)
    {
        pickedCoins++;
        SetPointText();

        SoundManager.Instance.PlayPickupSound();

        if (pickedCoins == totalCoins)
        {
            LevelManager.Instance.EndLevel();
        }
    }
	
    public void RegisterCoins(CoinPickup c)
    {
        totalCoins++;
        SetPointText();
    }

    private void SetPointText()
    {
        _pointsText.text = pickedCoins + " / " + totalCoins;
    }

    private void Rotate(ref Vector3 vec, Vector3 axis, float amount)
    {
        Quaternion q = Quaternion.AngleAxis(amount, axis);
        vec = q * vec;
    }

    private void HandleTimer()
    {
        if (LevelManager.Instance.LevelIsRunning)
        {
            highscoreTime += Time.deltaTime;

            currentTimer.SetTime(highscoreTime);

            SetTimeText();

        }
    }

    private void SetTimeText()
    {
        _timeText.text = currentTimer.GetDisplayString();
    }

    private void HandleInput()
    {
        targetUp = Vector3.up;

#if UNITY_STANDALONE || UNITY_EDITOR
        UpdateOrientation(ref targetUp, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
#else
        UpdateOrientation(ref targetUp, Input.acceleration);
#endif

        currentUp = Vector3.SmoothDamp(currentUp, targetUp, ref helpVelocity, Vector3.Distance(currentUp, targetUp) * (_maxDampSecs - _minDampSecs) + _minDampSecs);
        _gravityRoot.transform.up = currentUp;

        MoveVec = Vector3.left * _movementFactors.x + Vector3.forward * _movementFactors.y;
    }

    private void UpdateOrientation(ref Vector3 newUp, Vector2 vec)
    {
        if (vec.x != 0.0f)
            Rotate(ref newUp, Vector3.forward, _maxWorldRotationDegree * vec.x);

        if (vec.y != 0.0f)
            Rotate(ref newUp, Vector3.left, _maxWorldRotationDegree * vec.y);

        _movementFactors.x = -vec.x;
        _movementFactors.y = vec.y;
    }


}
