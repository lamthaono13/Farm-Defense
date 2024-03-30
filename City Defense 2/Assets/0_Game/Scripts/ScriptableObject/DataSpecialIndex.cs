using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataSpecialIndex")]
public class DataSpecialIndex : SerializedScriptableObject
{
    public List<float> DataConfigIndex;

    public float GetSpecialIndex(TypeSpecialIndex typeSpecialIndex)
    {
        return DataConfigIndex[(int)typeSpecialIndex];
    }
}
