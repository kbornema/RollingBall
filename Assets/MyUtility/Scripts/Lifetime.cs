using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Lifetime 
{
    public float current;
    public float max;

    public float Percent { get { return current / max; } }
    
    public Lifetime(float max, float cur = 0)
    {
        this.max = max;
        this.current = cur;
    }

    public void UpdateTime(float deltaTime)
    {
        current += deltaTime;
    }
}
