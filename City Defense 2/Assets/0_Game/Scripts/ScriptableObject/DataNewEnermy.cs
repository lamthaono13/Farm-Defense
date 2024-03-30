using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataNewEnermy")]
public class DataNewEnermy : SerializedScriptableObject
{
    public List<DescriptionEnermy> DescriptionEnermies;
}

public class DescriptionEnermy
{
    public int levelShow;

    public string name;

    public string description;

    public int id;
}