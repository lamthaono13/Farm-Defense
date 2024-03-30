using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class DataLevel
{
    public DataMap DataMap;

    public int gemDrop;

    public bool isEnergySub;

    public List<TypeEquip> EnermiesInLevel;

    public void AddEnermiesInLevel(TypeEquip typeEquip)
    {
        if(EnermiesInLevel == null)
        {
            EnermiesInLevel = new List<TypeEquip>();
        }

        if (!EnermiesInLevel.Contains(typeEquip))
        {
            EnermiesInLevel.Add(typeEquip);
        }
    }

    public DataLevel(DataInitDataLevel dataInitDataLevel, int _gemDrop, bool _isEnergySub, TypeEquip _typeEquip)
    {
        EnermiesInLevel = new List<TypeEquip>();

        EnermiesInLevel.Add(_typeEquip);

        Energy = dataInitDataLevel.Energy;

        GoldDrop = dataInitDataLevel.GoldDrop;

        DataMap = dataInitDataLevel.DataMap;

        gemDrop = (int)dataInitDataLevel.GemDrop;

        isEnergySub = _isEnergySub;

        if(DataTurnEnermy == null)
        {
            DataTurnEnermy = new List<DataTurnEnermy>();
        }

        DataTurnEnermy.Add(dataInitDataLevel.DataTurnEnermies);

        IndexQuest_1 = dataInitDataLevel.IndexQuest_1;

        IndexQuest_2 = dataInitDataLevel.IndexQuest_2;
    }

    public void SetDataLevel(DataTurnEnermy DataTurnEnermies, TypeEquip _typeEquip)
    {
        if (DataTurnEnermy == null)
        {
            DataTurnEnermy = new List<DataTurnEnermy>();
        }

        DataTurnEnermy.Add(DataTurnEnermies);

        bool checkContain = false;

        for (int i = 0; i < EnermiesInLevel.Count; i++)
        {
            if (EnermiesInLevel[i].TypeGroup == _typeEquip.TypeGroup && EnermiesInLevel[i].TypeTier == _typeEquip.TypeTier && EnermiesInLevel[i].TypeId == _typeEquip.TypeId)
            {
                checkContain = true;
            }
        }

        if (!checkContain)
        {
            EnermiesInLevel.Add(_typeEquip);
        }
    }

    public int GetGoldDrop(int numberEnermy)
    {
        float a = GoldDrop / numberEnermy;

        return (int)a;
    }


    public List<DataTurnEnermy> DataTurnEnermy;

    public int Energy;

    public float GoldDrop;

    public int IndexQuest_1;

    public int IndexQuest_2;
}

public class DataInitDataLevel
{
    public int IndexQuest_1;

    public int IndexQuest_2;

    public int Energy;

    public float GoldDrop;

    public float GemDrop;

    public DataMap DataMap;

    public DataTurnEnermy DataTurnEnermies;
}
