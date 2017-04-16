using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public struct Vector2i : IEquatable<Vector2i>
{
    public int x;
    public int y;

    public Vector2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return string.Concat(x.ToString(), ", ", y.ToString());
    }
    
    public static bool operator !=(Vector2i left, Vector2i right)
    {
        return left.x != right.x || left.y != right.y;
    }

    public static bool operator ==(Vector2i left, Vector2i right)
    {
        return left.x == right.x && left.y == right.y;
    }
    
    public static Vector2i operator -(Vector2i left, Vector2i right)
    {
        return new Vector2i(left.x - right.x, left.y - right.y);
    }

    public static Vector2i operator +(Vector2i left, Vector2i right)
    {
        return new Vector2i(left.x + right.x, left.y + right.y);
    }

    public override bool Equals(object obj)
    {
        if(obj is Vector2i)
        {
            Vector2i vec = (Vector2i)obj;

            return this.Equals(vec);
        }

        return false;
    }

    public bool Equals(Vector2i other)
    {
        return this.x == other.x && this.y == other.y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
