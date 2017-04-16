using UnityEngine;
using System.Collections;
using UnityEditor;

public static class EditorUtil
{

    [MenuItem("Utility/Create Prefabs From Selection")]
    static void CreatePrefabsFromSelection()
    {
        GameObject[] objects = Selection.gameObjects;

        for (int i = 0; i < objects.Length; i++)
        {
            PrefabUtility.CreatePrefab(string.Concat("Assets/", objects[i].name, ".prefab"), objects[i], ReplacePrefabOptions.ConnectToPrefab);

        }
    }

    [MenuItem("Utility/Make Prefabs From Sprite Selection")]
    static void InstantiateTextureSelection()
    {
        Object[] selectedObjects = Selection.objects;

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Texture2D texture = selectedObjects[i] as Texture2D;

            if (texture)
            {
                string spritesheetPath = AssetDatabase.GetAssetPath(texture);
                Object[] objects = AssetDatabase.LoadAllAssetRepresentationsAtPath(spritesheetPath);

                for (int j = 0; j < objects.Length; j++)
                {
                    Sprite sprite = objects[j] as Sprite;

                    if (sprite)
                    {
                        GameObject tmp = new GameObject(sprite.name);
                        SpriteRenderer tmpRenderer = tmp.AddComponent<SpriteRenderer>();
                        tmpRenderer.sprite = sprite;

                        PrefabUtility.CreatePrefab(string.Concat("Assets/", sprite.name, ".prefab"), tmp, ReplacePrefabOptions.ConnectToPrefab);

                        GameObject.DestroyImmediate(tmp);
                    }
                }
            }
        }

    }

    public static void Collapse(GameObject go, bool collapse)
    {
        // bail out immediately if the go doesn't have children
        if (go.transform.childCount == 0) 
            return;

        // get a reference to the hierarchy window
        var hierarchy = GetFocusedWindow("Hierarchy");

        // select our go
        SelectObject(go);

        // create a new key event (RightArrow for collapsing, LeftArrow for folding)
        var key = new Event { keyCode = collapse ? KeyCode.LeftArrow : KeyCode.RightArrow, type = EventType.keyDown };

        // finally, send the window the event
        hierarchy.SendEvent(key);
    }
    public static void SelectObject(Object obj)
    {
        Selection.activeObject = obj;
    }
    public static EditorWindow GetFocusedWindow(string window)
    {
        FocusOnWindow(window);
        return EditorWindow.focusedWindow;
    }

    public static void FocusOnWindow(string window)
    {
        EditorApplication.ExecuteMenuItem("Window/" + window);
    }

    public static void SetExpandedRecursive(GameObject go, bool expand)
    {
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        var methodInfo = type.GetMethod("SetExpandedRecursive");

        EditorApplication.ExecuteMenuItem("Window/Hierarchy");
        var window = EditorWindow.focusedWindow;

        methodInfo.Invoke(window, new object[] { go.GetInstanceID(), expand });
    }

    public static void DrawDebugWireRectangle(Vector2 min, Vector2 max)
    {
        DrawDebugWireRectangle(min.x, min.y, max.x, max.y);
    }

    public static void DrawDebugWireRectangle(float minX, float minY, float maxX, float maxY)
    {
        UnityEditor.Handles.DrawLine(new Vector3(minX, minY, 0.0f), new Vector3(maxX, minY, 0.0f));
        UnityEditor.Handles.DrawLine(new Vector3(maxX, minY, 0.0f), new Vector3(maxX, maxY, 0.0f));
        UnityEditor.Handles.DrawLine(new Vector3(maxX, maxY, 0.0f), new Vector3(minX, maxY, 0.0f));
        UnityEditor.Handles.DrawLine(new Vector3(minX, maxY, 0.0f), new Vector3(minX, minY, 0.0f));
    }
}
