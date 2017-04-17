using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class Savegame
{
    private string slotName = null;
    public string SlotName { get { return this.slotName; } set { this.slotName = value; } }

    private Dictionary<string, object> serializedObjects = new Dictionary<string, object>();

    public Savegame(string slotName)
    {
        this.slotName = slotName;
    }
    
    public void SetFloat(string key, float val)
    {
        SetObject(key, val);
    }

    public void SetInt(string key, int val)
    {
        SetObject(key, val);
    }

    public void SetString(string key, string val)
    {
        SetObject(key, val);
    }

    public void SetBool(string key, bool val)
    {
        SetObject(key, val);
    }

    public bool GetFloat(string key, out float result)
    {
        return GetObject<float>(key, out result);
    }

    public bool GetInt(string key, out int result)
    {
        return GetObject<int>(key, out result);
    }

    public bool GetInt(string key, out string result)
    {
        return GetObject<string>(key, out result);
    }

    public bool GetBool(string key, out bool result)
    {
        return GetObject<bool>(key, out result);
    }

    public bool GetObject<T>(string key, out T obj)
    {
        if (serializedObjects.ContainsKey(key))
        {
            obj = (T)serializedObjects[key];
            return true;
        }

        obj = default(T);
        return false;
    }

    public void SetObject<T>(string key, T obj)
    {
        if (serializedObjects.ContainsKey(key))
            serializedObjects[key] = obj;

        else
            serializedObjects.Add(key, obj);
    }

    public bool RemoveObject(string key)
    {
        return serializedObjects.Remove(key);
    }

    public void SaveLevelFinished(SceneRef scene)
    {
        string key = GetLevelFinishString(scene.LevelId);
        SetBool(key, true);
    }

    public bool GetHighscore(SceneRef scene, out HighscoreTime time)
    {
        return GetObject<HighscoreTime>(GetLevelHighscoreString(scene.LevelId), out time);
    }

    public void SetHighscore(SceneRef scene, HighscoreTime time)
    {
        HighscoreTime oldHighscore;

        if(GetHighscore(scene, out oldHighscore))
        {
            if(time < oldHighscore)
            {
                SetObject<HighscoreTime>(GetLevelHighscoreString(scene.LevelId), time);
            }
        }

        else
        {
            SetObject<HighscoreTime>(GetLevelHighscoreString(scene.LevelId), time);
        }
    }

    public bool IsLevelUnlocked(SceneRef scene)
    {
        if(scene.LevelId <= 0)
            return true;

        bool result = false;

        GetBool(GetLevelFinishString(scene.LevelId - 1), out result);

        return result;
    }

    private static string GetLevelFinishString(int id)
    {
        return id + "_finished";
    }

    private static string GetLevelHighscoreString(int id)
    {
        return id + "_highscore";
    }
}
