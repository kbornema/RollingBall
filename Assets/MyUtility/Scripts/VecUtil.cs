using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VecUtil
{
    public static Vector2 RandDir()
    {
        return RadToDir(Mathf.PI * 2.0f * UnityEngine.Random.value);
    }

    public static float DirToRad(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x);
    }

    public static Vector2 RadToDir(float rad)
    {
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static Vector2 LeftSide(Vector2 v)
    {
        return new Vector2(-v.y, v.x);
    }

    public static Vector2 RightSide(Vector2 v)
    {
        return new Vector2(v.y, -v.x);
    }

    public static Vector2 RoundToNearestVector2f(Vector2 input, float division)
    {
        return new Vector2( (float)(Math.Round(input.x * division, MidpointRounding.AwayFromZero)) / division,
                            (float)(Math.Round(input.y * division, MidpointRounding.AwayFromZero)) / division);
    }

    public static Vector2 ToLeft(this Vector2 v)
    {
        return LeftSide(v);
    }

    public static Vector2 ToRight(this Vector2 v)
    {
        return RightSide(v);
    }

    public static float ToRad(this Vector2 v)
    {
        return DirToRad(v);
    }

    public static Vector2 Rotate(this Vector2 v, float offsetRad)
    {
        return RadToDir(DirToRad(v) + offsetRad);
    }

    public static void MinMax(Vector2 a, Vector2 b, out Vector2 min, out Vector2 max)
    {
        min.x = Mathf.Min(a.x, b.x);
        max.x = Mathf.Max(a.x, b.x);

        min.y = Mathf.Min(a.y, b.y);
        max.y = Mathf.Max(a.y, b.y);
    }
}
