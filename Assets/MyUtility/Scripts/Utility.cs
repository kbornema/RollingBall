using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>Static class that holds several functionalities that might be used on many games.</summary>
public static class Utility
{
    public static string CreateTimeStamp(DateTime time)
    {
        string dayString = time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString();
        string monthString = time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString();
        string hourString = time.Hour < 10 ? ("0" + time.Hour.ToString()) : time.Hour.ToString();
        string minuteString = time.Minute < 10 ? ("0" + time.Minute.ToString()) : time.Minute.ToString();

        return string.Concat(dayString, ".", monthString, ".", time.Year, " - ", hourString, ":", minuteString);
    }

    public static string CreateTimeStamp()
    {
        return CreateTimeStamp(DateTime.Now);
    }

    public static void DrawScreenFilledQuad(Material material)
    {
        material.SetPass(0);

        GL.PushMatrix();
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);

        GL.TexCoord2(0.0f, 0.0f);
        GL.Vertex3(0, 0, -1);

        GL.TexCoord2(0.0f, 1.0f);
        GL.Vertex3(0, 1, -1);

        GL.TexCoord2(1.0f, 1.0f);
        GL.Vertex3(1, 1, -1);

        GL.TexCoord2(1.0f, 0.0f);
        GL.Vertex3(1, 0, -1);

        GL.End();
        GL.PopMatrix();
    }

    public static Vector2 pos2(this Transform t)
    {
        return t.position;
    }

    /*
    public static T CreateAsset<T>(string name, string folder = "") where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        if (!FolderExists(folder))
        {
            if (CreateFolder(folder))
                AssetDatabase.Refresh();
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets" + Path.DirectorySeparatorChar + folder + Path.DirectorySeparatorChar + name + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return asset;
    }

    public static bool CreateFolder(string relativePath)
    {
        if (!FolderExists(relativePath))
        {
            string globalPath = GetGlobalPathFrom(relativePath);
            DirectoryInfo info = Directory.CreateDirectory(globalPath);

            if (info.Exists)
                AssetDatabase.Refresh();

            return info.Exists;
        }

        return false;
    }

    public static bool FolderExists(string relativePath)
    {
        bool result = Directory.Exists(GetGlobalPathFrom(relativePath));
        return result;
    }

    public static string GetGlobalPathFrom(string relativePath)
    {
        return Application.dataPath + Path.DirectorySeparatorChar + relativePath;
    }

    public static string CreatePath(params string[] folders)
    {
        string path = "";

        for (int i = 0; i < folders.Length; i++)
            path = path + folders[i] + Path.DirectorySeparatorChar;

        return path;
    }
    */
}
