using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataEachUnlock")]
public class DataEachUnlock : SerializedScriptableObject
{
    public float goldLevel;

    public float goldGrow;

    public float gemUnlock;

    public float gemUpgradeStar;

    //public List<List<DataClassPack>> dataClassPacks;

    //public List<Sprite> spritesRenderFollowLevel;

    public void Init(float _goldLevel, float _goldGrow, float _gemUnlock, float _gemUpgradeStar)
    {
        //UpgradeLevel = new List<Unlock>();

        //for (int i = 0; i < listUpdateCost.Count; i++)
        //{
        //    UpgradeLevel.Add(new Unlock() 
        //    {
        //        TypeUnlock = TypeUnlock.Coin,
        //        IndexToUnlock = listUpdateCost[i]
        //    });
        //}

        goldLevel = _goldLevel;

        goldGrow = _goldGrow;

        gemUnlock = _gemUnlock;

        gemUpgradeStar = _gemUpgradeStar;

    }

    public long GetLevelUp(int currentLevel)
    {
        //if(currentLevel <= UpgradeLevel.Count)
        //{
        //    return UpgradeLevel[currentLevel - 1];
        //}
        //else
        //{
        //    return null;
        //}

        long gold = (long)goldLevel;

        for(int i = 0; i < currentLevel - 1; i++)
        {
            gold = (long)(gold * goldGrow);
        }

        Unlock unlock = new Unlock()
        {
            IndexToUnlock = gold,
            TypeUnlock = TypeUnlock.Coin
        };

        return gold;
    }

    public int GetGemUnlock()
    {
        return (int)gemUnlock;
    }

    public int GetGemUpgradeStar()
    {
        return (int)gemUpgradeStar;
    }
}

public class Unlock
{
    public float IndexToUnlock;

    public TypeUnlock TypeUnlock;
}

public class Bonus
{
    public string TitleBonus;

    public TypeBonus TypeBonus;

    public float index;
}
