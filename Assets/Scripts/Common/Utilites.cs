using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilites
{
    public static Vector2 Wrap(Vector2 point, Vector2 min, Vector2 max)
    {
        if (point.x > max.x)
        {
            point.x = min.x;
        }
        return point;

    }
}
