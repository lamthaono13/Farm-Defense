using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableObject/DataConfigAnimation")]
public class DataConfigAnimation : SerializedScriptableObject
{
    [TableList]
    public List<DataEachAnimation> DataEachAnimations;
}

public class DataEachAnimation
{
    public TypeAnimation TypeAnimation;
    public string NameAnimtion;
    public string NameEvent;
    public float speedAnimBase;
}
