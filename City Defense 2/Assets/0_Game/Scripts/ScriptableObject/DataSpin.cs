using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataSpin")]
public class DataSpin : SerializedScriptableObject
{
    public void Init()
    {

    }

    public List<DataEachElementSpin> dataEachElementSpins;
}

[System.Serializable]
public class DataEachElementSpin
{
    public TypeLuckySpin TypeLuckySpin;

    public int indexEarn;

    public int rate;
}