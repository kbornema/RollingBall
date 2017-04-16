#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Text;

public class TagsLayersEnumBuilder : EditorWindow
{
    private const string TAG_ENUM_NAME = "Tags";
    private const string TAG_CLASS_NAME = "TagUtil";
    private const string LAYER_CLASS_NAME = "LayerUtil";
    private const string CLASS_FILE_ENDING = ".cs";
    private const string PATH = "/External/MyUtility/Scripts/";

    [MenuItem("Utility/Rebuild Tags And Layers Enums")]
    static void RebuildTagsAndLayersEnums()
    {
        string tagClassRelativePath = PATH + TAG_CLASS_NAME + CLASS_FILE_ENDING;
        string tagClassFullPath = Application.dataPath + tagClassRelativePath;
        rebuildTagsFile(tagClassFullPath);
        //AssetDatabase.ImportAsset(tagClassRelativePath, ImportAssetOptions.ForceUpdate);

        string layerClassRelativePath = PATH + LAYER_CLASS_NAME + CLASS_FILE_ENDING; 
        string layerClassFullPath = Application.dataPath + layerClassRelativePath;
        rebuildLayersFile(layerClassFullPath);
        //AssetDatabase.ImportAsset(layerClassRelativePath, ImportAssetOptions.ForceUpdate);
    }

    static void rebuildTagsFile(string filePath)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("//This class is auto-generated, do not modify (TagsLayersEnumBuilder.cs)\n");
        sb.Append("public static class " + TAG_CLASS_NAME + " {\n");

        var srcArr = UnityEditorInternal.InternalEditorUtility.tags;
        var tags = new String[srcArr.Length];
        Array.Copy(srcArr, tags, tags.Length);
        Array.Sort(tags, StringComparer.InvariantCultureIgnoreCase);

        sb.Append("\tpublic enum " + TAG_ENUM_NAME + " {\n");

        for (int i = 0, n = tags.Length; i < n; ++i)
        {
            string tagName = tags[i];

            sb.Append("\t\t" + tagName + ",\n");
        }

        sb.Append("\t}\n");

        sb.Append("}\n");

#if !UNITY_WEBPLAYER
        File.WriteAllText(filePath, sb.ToString());
#endif
    }

    static void rebuildLayersFile(string filePath)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("//This class is auto-generated, do not modify (use Tools/TagsLayersEnumBuilder)\n");
        sb.Append("public static class " + LAYER_CLASS_NAME + " {\n");

        var layers = UnityEditorInternal.InternalEditorUtility.layers;

        for (int i = 0, n = layers.Length; i < n; ++i)
        {
            string layerName = layers[i];

            sb.Append("\tpublic const string " + GetVariableName(layerName) + " = \"" + layerName + "\";\n");
        }

        sb.Append("\n");

        for (int i = 0, n = layers.Length; i < n; ++i)
        {
            string layerName = layers[i];
            int layerNumber = LayerMask.NameToLayer(layerName);
            string layerMask = layerNumber == 0 ? "1" : ("1 << " + layerNumber);

            sb.Append("\tpublic const int " + GetVariableName(layerName) + "Mask" + " = " + layerMask + ";\n");
        }

        sb.Append("\n");

        for (int i = 0, n = layers.Length; i < n; ++i)
        {
            string layerName = layers[i];
            int layerNumber = LayerMask.NameToLayer(layerName);

            sb.Append("\tpublic const int " + GetVariableName(layerName) + "Number" + " = " + layerNumber + ";\n");
        }

        sb.Append("}\n");

#if !UNITY_WEBPLAYER
        File.WriteAllText(filePath, sb.ToString());
#endif
    }

    private static string GetVariableName(string str)
    {
        return str.Replace(" ", "");
    }
}
#endif
