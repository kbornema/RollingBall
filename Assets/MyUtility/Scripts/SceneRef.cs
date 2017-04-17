using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SceneRef", menuName = "Custom/SceneRef", order = 1)]
public sealed class SceneRef : ScriptableObject 
{
    [SerializeField]
    private Object scene;
    [SerializeField]
    private int levelId;
    public int LevelId { get { return levelId; } set { levelId = value; } }
    [SerializeField]
    private bool isPlayable;
    public bool IsPlayable { get { return isPlayable; } }

    [SerializeField]
    private HighscoreTime goldTime;
    public HighscoreTime GoldTime { get { return goldTime; } }

    [SerializeField]
    public float silverTimePercent = 1.5f;
    public HighscoreTime SilverTime { get { return GetSilverTime(); } }

    private HighscoreTime GetSilverTime()
    {
        goldTime.ComputeTotalTime();
        HighscoreTime time = new HighscoreTime();
        time.SetTime(goldTime.TotalTime * silverTimePercent);

        return time;
    }

    [SerializeField]
    public float bronzeTimePercent = 2.0f;
    public HighscoreTime BronzeTime { get { return GetBronzeTime(); } }

    private HighscoreTime GetBronzeTime()
    {
        goldTime.ComputeTotalTime();
        HighscoreTime time = new HighscoreTime();
        time.SetTime(goldTime.TotalTime * bronzeTimePercent);

        return time;
    }

    [SerializeField, ReadOnly]
    private string sceneName;

    private void OnValidate()
    {
        UpdateString();
    }

    private void OnEnable()
    {
        UpdateString();
    }

    public void UpdateString()
    {
        if (scene && sceneName != scene.name)
        {
            sceneName = scene.name;
#if UNITY_EDITOR
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), sceneName + "Ref");
#endif
        }
    }

    public string GetSceneName()
    {
        return sceneName;
    }
}
