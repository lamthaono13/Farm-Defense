using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataConfig")]
public class DataConfig : SerializedScriptableObject
{
    public List<List<ConfigBaseIndex>> configsForAlly;

    public List<List<ConfigBaseIndex>> configsForEnermy;

    public void InitEnermy(List<DataConfigForTypeChar> dataConfigForTypeChars, List<DataConfigIndexGrow> dataConfigIndexGrows)
    {
        //for(int i = 0; i < configsForEnermy.Count; i++)
        //{
        //    configsForEnermy[i].Init(dataConfigForTypeChars[i], dataConfigIndexGrows[i]);
        //}
    }

    public void InitAlly()
    {

    }
}