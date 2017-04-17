using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Levels", menuName = "LevelHierarchy", order = 0)]
public class LevelHierarchy : ScriptableObject
{
    [SerializeField]
    private List<SceneRef> levels;

    public int NumLevels { get { return levels.Count; } }

    public SceneRef GetLevel(int levelId)
    {
        if (levelId < 0 || levelId >= NumLevels)
            return null;

        return levels[levelId];
    }

    public List<SceneRef> GetAllLevel()
    {
        return levels;
    }

    public SceneRef GetLevel(string sceneName)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].GetSceneName() == sceneName)
                return levels[i];
        }

        return null;
    }

    private void OnValidate()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].LevelId = i;
        }
    }



}
