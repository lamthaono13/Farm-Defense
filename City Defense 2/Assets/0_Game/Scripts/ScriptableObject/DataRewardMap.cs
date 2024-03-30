using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataRewardMap")]
public class DataRewardMap : SerializedScriptableObject
{
    public List<DataEachRewardStage> DataEachRewardStages;
}

public class DataEachRewardStage
{
    public int StarNeed;
    public int GoldEarn;
    public int GemEarn;
}