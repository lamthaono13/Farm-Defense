using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataGetPack")]
public class DataGetPack : SerializedScriptableObject
{
    public List<Buy> DataPack;
}
