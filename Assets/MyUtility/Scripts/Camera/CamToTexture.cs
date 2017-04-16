using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CamToTexture : MonoBehaviour 
{
    [SerializeField]
    private Camera cam;
    private RenderTexture renderTexture;

	void Awake () 
    {
        if (!cam)
            cam = GetComponent<Camera>();

        
        SetupRenderTexture();
	}

    private void SetupRenderTexture()
    {
        if (renderTexture)
            DeleteRenderTexture();

        renderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
        cam.targetTexture = renderTexture;
    }

    private void DeleteRenderTexture()
    {
        Debug.Assert(renderTexture);

        cam.targetTexture = null;

#if UNITY_EDITOR
        if (Application.isPlaying)
            Destroy(renderTexture);
        else
            DestroyImmediate(renderTexture);
#else
        Destroy(renderTexture);
#endif

    }
	
	void Update () 
    {
		if(renderTexture.width != Screen.width || renderTexture.height != Screen.height)
        {
            SetupRenderTexture();
        }
	}
}
