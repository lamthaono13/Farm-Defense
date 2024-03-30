using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataGetGold")]
public class DataGetGold : SerializedScriptableObject
{
    public List<ElementGetGold> ElementGetGolds;
}

public class ElementGetGold
{
    public Buy Buy;

    public float IndexGrow;

    public float IndexEarn;

    public float GetInDexEarn(float map)
    {
        float a = IndexEarn;

        for(int i = 0; i < map - 1; i++)
        {
            a = a * IndexGrow;
        }

        return a;
    }
}
