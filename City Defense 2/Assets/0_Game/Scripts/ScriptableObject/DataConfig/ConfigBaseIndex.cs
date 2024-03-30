using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ConfigBaseIndex")]
public class ConfigBaseIndex : SerializedScriptableObject
{
    //public List<DataConfigForTypeChar> dataConfigForTypeChars;

    public DataConfigForTypeChar dataConfigForTypeCharBase;

    public DataConfigIndexGrow dataConfigIndexGrow;

    public DataCard dataCardConditionGold;

    //public DataConfigForTypeChar GetDataConfigForTypeChar(int level)
    //{
    //    if(level <= dataConfigForTypeChars.Count)
    //    {
    //        return dataConfigForTypeChars[level - 1];
    //    }
    //    else
    //    {
    //        return dataConfigForTypeChars[dataConfigForTypeChars.Count - 1];
    //    }
    //}

    public void Init(DataConfigForTypeChar _dataConfigForTypeChar, DataConfigIndexGrow _dataConfigIndexGrow)
    {
        dataConfigForTypeCharBase = _dataConfigForTypeChar;

        dataConfigIndexGrow = _dataConfigIndexGrow;
    }
}

public class DataConfigForTypeChar
{
    public int Energy;
    public float Damage;
    public float HP;
    public float Speed;
    public float AttackSpeed;
    public float RadiusCheck;
    public float RangeToAttack;
    public int BaseStar;
    public string Name;
    public string Description;
    public string DescriptionAbility;
    public int levelConditionUnlockGold;
    public int goldUnlock;
    public string stringDesUnlockGold;
}

public class DataConfigIndexGrow
{
    public float AttackGrow;

    public float HPGrow;

    public float AttackSpeedGrow;
}
