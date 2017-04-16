using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class ReplaceMaterial : EditorWindow
{

    private Material oldMaterial;
    private Material newMaterial;

    [MenuItem("Utility/Find sprite material")]
    static void CreatePrefabsFromSelection()
    {
        EditorWindow.GetWindow(typeof(ReplaceMaterial));
    }

    void OnGUI()
    {

        if (GUILayout.Button("Replace in scene"))
        {
            if (!oldMaterial || !newMaterial)
                return;

            var spriteRenderer = GameObject.FindObjectsOfType<SpriteRenderer>();

            int count = 0;
            for (int i = 0; i < spriteRenderer.Length; i++)
            {

                if (IsInstance(spriteRenderer[i].sharedMaterial, oldMaterial))
                {
                    count++;
                    spriteRenderer[i].material = newMaterial;
                }  
            }

            Debug.Log("Replaced: " + count + " materials in the scene!");
        }

        if (GUILayout.Button("Select old material"))
        {
            if (!oldMaterial)
                return;

            var spriteRenderer = GameObject.FindObjectsOfType<SpriteRenderer>();

            List<GameObject> selection = new List<GameObject>();

            int count = 0;
            for (int i = 0; i < spriteRenderer.Length; i++)
            {

                if (IsInstance(spriteRenderer[i].sharedMaterial, oldMaterial))
                {
                    count++;
                    selection.Add(spriteRenderer[i].gameObject);
                }
            }

            Selection.objects = selection.ToArray();
        }


        oldMaterial = (Material)(EditorGUILayout.ObjectField("Old Material", oldMaterial, typeof(Material), false));

        newMaterial = (Material)(EditorGUILayout.ObjectField("New Material", newMaterial, typeof(Material), false));
       

    }

    private bool IsInstance(Material instance, Material material)
    {
        return instance.name == material.name + " (Instance)" || instance.name == material.name;
    }
}
