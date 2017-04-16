using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneRef))]
public class SceneRefInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update"))
        {
            SceneRef sceneRef = (SceneRef)this.target;
            sceneRef.UpdateString();
        }
    }
}
