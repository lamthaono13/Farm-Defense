using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataEnergy")]
public class DataEnergy : SerializedScriptableObject
{
    public List<int> DataEnergySub;

    public void Init(List<int> _dataEnergySub)
    {
        DataEnergySub = _dataEnergySub;
    }
}
