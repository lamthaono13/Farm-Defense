using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionToAngle2D
{
    public static float GetAngleFromDirection2D(Vector3 direction)
    {
        Vector3 a = direction;

        a.z = 0;

        float angle = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;

        return angle;
    }
}
