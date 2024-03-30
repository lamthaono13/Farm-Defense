using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertSpriteSize
{
    public static Vector2 GetResize(Vector2 currentSize, Vector2 originSize)
    {
        float kx = originSize.x;

        if (originSize.x >= originSize.y)
        {
            kx = originSize.x;

            float k1 = currentSize.x / kx;

            float k2 = currentSize.y / kx;

            float kTB = (k1 + k2) / 2;

            Vector2 newSize = originSize * k1;

            return newSize;
        }
        else
        {
            kx = originSize.y;

            float k1 = currentSize.x / kx;

            float k2 = currentSize.y / kx;

            float kTB = (k1 + k2) / 2;

            Vector2 newSize = originSize * k2;

            return newSize;
        }


    }

    public static float GetKResize(Vector2 currentSize, Vector2 originSize)
    {
        float kx = originSize.x;

        if (originSize.x >= originSize.y)
        {
            kx = originSize.x;

            float k1 = currentSize.x / kx;

            float k2 = currentSize.y / kx;

            float kTB = (k1 + k2) / 2;

            Vector2 newSize = originSize * k1;

            return k1;
        }
        else
        {
            kx = originSize.y;

            float k1 = currentSize.x / kx;

            float k2 = currentSize.y / kx;

            float kTB = (k1 + k2) / 2;

            Vector2 newSize = originSize * k2;

            return k2;
        }
    }
}
