using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Point
{

    public float x;
    public float y;

    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }
    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.y - b.y);
    }
    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Point)
        {
            Point p = (Point)obj;
            return x == p.x && y == p.y;
        }
        return false;

    }

    public bool Equals(Point p)
    {
        return x == p.x && y == p.y;
    }

    //暫時不要用
    public override int GetHashCode()
    {
        return 0;
    }
    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }
}