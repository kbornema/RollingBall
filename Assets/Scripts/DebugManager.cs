using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : AManager<DebugManager> 
{
    [SerializeField]
    private GameObject _debugRoot;
    public GameObject DebugRoot { get { return _debugRoot; } }

    [SerializeField]
    private GameObject _debugTextPanel;
    public GameObject DebugTextPanel { get { return _debugTextPanel; } }


    [SerializeField]
    private Text _veloXYTextField;
    [SerializeField]
    private Text debugTextField;
    [SerializeField]
    private Text _fpsTextField;

    private const int MAX_DEBUG_LINES = 30;
    private List<string> debugLines = new List<string>();
    private bool _showFps = true;
    private float _deltaTime;

    protected override void OnAwake()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Application.logMessageReceived += LogMsgReceived;
        StartCoroutine(FpsRoutine());
#else
        this.enabled = false;
#endif
    }

    private void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime);

        _veloXYTextField.text = GameManager.Instance.MoveFactors.x.ToString("0.0") + ", " + GameManager.Instance.MoveFactors.y.ToString("0.0");
    }

    private void LogMsgReceived(string condition, string stackTrace, LogType type)
    {
        string msg = string.Concat(condition, " [", stackTrace, "]");
        PrintMsg(msg);
    }

    private IEnumerator FpsRoutine()
    {
        while(_showFps)
        {
            _fpsTextField.text = (1.0f / _deltaTime).ToString(); //.ToString("0.0");
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void PrintMsg(string text)
    {
        if (debugLines.Count >= MAX_DEBUG_LINES)
            debugLines.RemoveAt(0);

        debugLines.Add(text);

        string textToDisplay = "";
        for (int i = 0; i < debugLines.Count; i++)
        {
            textToDisplay += debugLines[i] + "\n";
        }

        debugTextField.text = textToDisplay;
    }

    public void ClearDebugText()
    {
        debugLines.Clear();
        debugTextField.text = "";
    }
}
