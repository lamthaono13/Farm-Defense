using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DataManagerMainGame : SerializedMonoBehaviour
{
    [SerializeField] private DataGame dataGame;

    [SerializeField] private DataEnergy dataEnergy;

    [SerializeField] private DataSpecialIndex dataSpecialIndex;

    [SerializeField] private DataSprite dataSprite;

    [SerializeField] private DataQuestDisplay dataQuestDisplay;

    [SerializeField] private DataMapRender dataMapRender;

    public DataGame DataGame => dataGame;

    public DataEnergy DataEnergy => dataEnergy;

    public DataSpecialIndex DataSpecialIndex => dataSpecialIndex;

    public DataSprite DataSprite => dataSprite;

    public DataQuestDisplay DataQuestDisplay => dataQuestDisplay;

    public DataMapRender DataMapRender => dataMapRender;

    public List<List<List<ConfigBaseIndex>>> dataConfigForTypeChars;

    public List<List<List<ConfigBaseIndex>>> dataConfigForTypeCharsEnermy;

    public List<List<List<DataEachUnlock>>> dataEachUnlocks;

    public List<List<List<DataConfigAnimation>>> dataConfigAnimationsAlly;

    public ConfigBaseIndex GetConfigBaseIndex(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return dataConfigForTypeChars[(int)typeGroup][(int)typeTier][(int)typeId];
    }

    public ConfigBaseIndex GetConfigBaseIndexEnermy(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return dataConfigForTypeCharsEnermy[(int)typeGroup][(int)typeTier][(int)typeId];
    }

    public DataEachUnlock GetDataEachUnlock(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return dataEachUnlocks[(int)typeGroup][(int)typeTier][(int)typeId];
    }

    public DataConfigAnimation GetDataConfigAnimationAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return dataConfigAnimationsAlly[(int)typeGroup][(int)typeTier][(int)typeId];
    }
}
