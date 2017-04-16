using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatRange
{
    [SerializeField]
    private float _min;
    public float Min { get { return _min; } }

    [SerializeField]
    private float _max;
    public float Max { get { return _max; } }

    public float GetRandVal()
    {
        return GetInterpolated(Random.value);
    }

    public float GetInterpolated(float t)
    {
        Debug.Assert(_min <= _max);
        return Mathf.Lerp(_min, _max, t);
    }

    public float InverseInterpolation(float value)
    {
        Debug.Assert(_min <= _max);
        return Mathf.InverseLerp(_min, _max, value);
    }
	
    public FloatRange(float min, float max)
    {
        Debug.Assert(min <= max);
        this._min = min;
        this._max = max;
    }
}
