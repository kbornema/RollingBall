using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public abstract class AScreenEffect : MonoBehaviour 
{
    [SerializeField]
    private bool _instantiateMaterial = false;

    [SerializeField]
    protected Material _materialPrefab;
    protected Material _usedMaterial;
    public Material EffectMaterial { get { return _usedMaterial; } }

    [SerializeField]
    protected Camera _effectedCam;
    public Camera EffectedCam { get { return _effectedCam; } }

    void Reset()
    {
        if (!_effectedCam)
            _effectedCam = GetComponent<Camera>();
    }

    protected virtual void Awake()
    {
        if(_usedMaterial == null)
            AssignMaterial();
    }

    protected void AssignMaterial()
    {
        Debug.Assert(_usedMaterial == null);

        if (_instantiateMaterial)
            _usedMaterial = Instantiate(_materialPrefab);

        else
            _usedMaterial = _materialPrefab;
    }

    protected void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        BeforeRenderImage();
        
        Debug.Assert(_usedMaterial);
        Graphics.Blit(src, dest, _usedMaterial);
        
    }

    protected abstract void BeforeRenderImage();
}
