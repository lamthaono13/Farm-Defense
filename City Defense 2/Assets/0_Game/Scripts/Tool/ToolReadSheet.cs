using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Yade.Runtime;

[CreateAssetMenu(menuName = "ToolReadSheet")]
public class ToolReadSheet : SerializedScriptableObject, ICellParser
{
    public float TimeDelay;

    public int HealthInGame;

    public int MaxTicket;

    public int SecondToRebornTicket;

    public int TicketReborn;

    public int TicketPlay;

    [Button]
    public void ReadAllSheet()
    {
        ReadDataConfig();
        ReadSqawnEnermy();
        ReadQuest();
        ReadRewardMap();
        ReadDataShop();
        //ReadUpdateCost();
        //ReadUpdateBarrack();
    }

    [Button]
    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
    }


    #region ReadDataConfig
    public List<YadeSheetData> configsForAlly;

    public List<YadeSheetData> configsForEnermy;
    //public YadeSheetData YadeSheetRangeEnermy;
    //public YadeSheetData YadeSheetSpecialEnermy;

    //public ConfigBaseIndex ConfigBaseIndexNormalEnermy;
    //public ConfigBaseIndex ConfigBaseIndexRangeEnermy;
    //public ConfigBaseIndex ConfigBaseIndexSpecialEnermy;

    public DataConfig dataConfig;

    private void ReadDataConfig()
    {
        ReadDataAlly();

        ReadDataEnermy();

        ReadSpecialIndex();
    }

    [SerializeField] private DataEnergy dataEnergy;

    [SerializeField] private List<List<DataEachUnlock>> dataEachUnlocks;

    private void ReadDataAlly()
    {
        // Index

        int indexName = 1;

        int indexEnergy = 2;

        int indexAttack = 6;

        int indexHp = 8;

        int indexSpeed = 12;

        int indexAttackSpeed = 10;

        int indexRadiusCheck = 14;

        int indexRangeToAttack = 13;

        int indexAttackGrow = 7;

        int indexHpGrow = 9;

        int indexAttackSpeedGrow = 11;

        int indexBaseStar = 15;

        int indexDescription = 16;

        int indexGoldUpgrade = 4;

        int indexGoldGrow = 5;

        int indexGemUnlock = 17;

        int indexGemUpgradeStar = 18;

        int indexDescriptionAbility = 19;

        int indexLevelConditionUnlockGold = 20;

        int indexGoldUnlock = 21;

        int indexStringDesUnlockGold = 22;

        // read ally

        List<int> listEnergy = new List<int>();

        for (int i = 0; i < dataConfig.configsForAlly.Count; i++)
        {
            YadeSheetData yadeSheetData = configsForAlly[i];

            for (int j = 0; j < dataConfig.configsForAlly[i].Count; j++)
            {
                DataConfigForTypeChar dataConfigForTypeChar;
                DataConfigIndexGrow dataConfigIndexGrow;

                int k = j + 1;


                dataEachUnlocks[i][j].Init
                    (
                    yadeSheetData.GetCell(k, indexGoldUpgrade).GetFloat(), 
                    yadeSheetData.GetCell(k, indexGoldGrow).GetFloat(), 
                    yadeSheetData.GetCell(k, indexGemUnlock).GetFloat(),
                    yadeSheetData.GetCell(k, indexGemUpgradeStar).GetFloat()
                    );


                dataConfigForTypeChar = new DataConfigForTypeChar()
                {
                    Name = yadeSheetData.GetCell(k, indexName).GetValue(),
                    Energy = yadeSheetData.GetCell(k, indexEnergy).GetInt(),
                    Damage = Mathf.Round(yadeSheetData.GetCell(k, indexAttack).GetFloat()),
                    HP = Mathf.Ceil(yadeSheetData.GetCell(k, indexHp).GetFloat()),
                    AttackSpeed = yadeSheetData.GetCell(k, indexAttackSpeed).GetFloat(),
                    Speed = yadeSheetData.GetCell(k, indexSpeed).GetFloat(),
                    RadiusCheck = yadeSheetData.GetCell(k, indexRadiusCheck).GetFloat(),
                    RangeToAttack = yadeSheetData.GetCell(k, indexRangeToAttack).GetFloat(),
                    BaseStar = yadeSheetData.GetCell(k, indexBaseStar).GetInt(),
                    Description = yadeSheetData.GetCell(k, indexDescription).GetValue(),
                    DescriptionAbility = yadeSheetData.GetCell(k, indexDescriptionAbility).GetValue(),
                    levelConditionUnlockGold = yadeSheetData.GetCell(k, indexLevelConditionUnlockGold).GetInt(),
                    goldUnlock = yadeSheetData.GetCell(k, indexGoldUnlock).GetInt(),
                    stringDesUnlockGold = yadeSheetData.GetCell(k, indexStringDesUnlockGold).GetValue()
                };

                dataConfigIndexGrow = new DataConfigIndexGrow()
                {
                    AttackGrow = yadeSheetData.GetCell(k, indexAttackGrow).GetFloat(),
                    HPGrow = yadeSheetData.GetCell(k, indexHpGrow).GetFloat(),
                    AttackSpeedGrow = yadeSheetData.GetCell(k, indexAttackSpeedGrow).GetFloat()
                };

                dataConfig.configsForAlly[i][j].Init(dataConfigForTypeChar, dataConfigIndexGrow);
            }

            listEnergy.Add(yadeSheetData.GetCell(1, indexEnergy).GetInt());
        }

        dataEnergy.Init(listEnergy);
    }

    private void ReadDataEnermy()
    {
        // Index

        int indexName = 1;

        int indexEnergy = 2;

        int indexAttack = 4;

        int indexHp = 6;

        int indexSpeed = 10;

        int indexAttackSpeed = 8;

        int indexRadiusCheck = 12;

        int indexRangeToAttack = 11;

        int indexAttackGrow = 5;

        int indexHpGrow = 7;

        int indexAttackSpeedGrow = 9;

        int indexDescription = 13;

        // read enermy

        for (int i = 0; i < dataConfig.configsForEnermy.Count; i++)
        {
            for (int j = 0; j < dataConfig.configsForEnermy[i].Count; j++)
            {
                DataConfigForTypeChar dataConfigForTypeChar;
                DataConfigIndexGrow dataConfigIndexGrow;

                int k = j + 1;

                YadeSheetData yadeSheetData = configsForEnermy[i];

                dataConfigForTypeChar = new DataConfigForTypeChar()
                {
                    Name = yadeSheetData.GetCell(k, indexName).GetValue(),
                    Energy = yadeSheetData.GetCell(k, indexEnergy).GetInt(),
                    Damage = Mathf.Round(yadeSheetData.GetCell(k, indexAttack).GetFloat()),
                    HP = Mathf.Ceil(yadeSheetData.GetCell(k, indexHp).GetFloat()),
                    AttackSpeed = yadeSheetData.GetCell(k, indexAttackSpeed).GetFloat(),
                    Speed = yadeSheetData.GetCell(k, indexSpeed).GetFloat(),
                    RadiusCheck = yadeSheetData.GetCell(k, indexRadiusCheck).GetFloat(),
                    RangeToAttack = yadeSheetData.GetCell(k, indexRangeToAttack).GetFloat(),
                    Description = yadeSheetData.GetCell(k, indexDescription).GetValue()
                };

                dataConfigIndexGrow = new DataConfigIndexGrow()
                {
                    AttackGrow = yadeSheetData.GetCell(k, indexAttackGrow).GetFloat(),
                    HPGrow = yadeSheetData.GetCell(k, indexHpGrow).GetFloat(),
                    AttackSpeedGrow = yadeSheetData.GetCell(k, indexAttackSpeedGrow).GetFloat()
                };

                dataConfig.configsForEnermy[i][j].Init(dataConfigForTypeChar, dataConfigIndexGrow);
            }
        }
    }

    #endregion

    #region ReadSqawnEnermy

    public YadeSheetData yadeSheetDataConfigLevel;

    public YadeSheetData YadeSheetDataSqawn;

    public DataGame DataGame;

    private void ReadSqawnEnermy()
    {
        DataGame.Init(YadeSheetDataSqawn, yadeSheetDataShop, TimeDelay);

        int indexRow = 1;

        int indexColumCheck = 0;

        int indexColumConfig = 1;

        //DataGame.HealthInGame = HealthInGame;

        DataGame.DataGrowConfigLevels = new List<DataGrowConfigLevel>();

        // Ticket

        //DataGame.MaxTicket = MaxTicket;

        //DataGame.SecondToRebornTicket = SecondToRebornTicket;

        //DataGame.TicketReborn = TicketReborn;

        //DataGame.TicketPlay = TicketPlay;

        //

        for (int i = 1; i < yadeSheetDataConfigLevel.GetRowCount(); i ++)
        {
            DataGame.DataGrowConfigLevels.Add(new DataGrowConfigLevel()
            {
                NumberCheckConfigLevel = yadeSheetDataConfigLevel.GetCell(i, indexColumCheck).GetInt(),

                IndexConfigLevel = yadeSheetDataConfigLevel.GetCell(i, indexColumConfig).GetFloat()
            });
        }
    }
    #endregion

    #region ReadUpdateCost
    //public YadeSheetData YadeSheetUpdateCost;

    //public DataUnlock DataUnlock;
    //private void ReadUpdateCost()
    //{
    //    DataUnlock.Init(YadeSheetUpdateCost);
    //}
    #endregion

    //public YadeSheetData YadeSheetDataBarrackBarrier;

    //public YadeSheetData YadeSheetDataBarrackMelee;

    //public YadeSheetData YadeSheetDataBarrackRange;

    //public DataUnlock DataUnlock;
    #region ReadSpecialIndex

    [SerializeField] private YadeSheetData yadeSheetDataSpecialIndex;

    [SerializeField] private DataSpecialIndex dataSpecialIndex;

    public void ReadSpecialIndex()
    {
        int indexName = 0;

        int indexNumber = 1;

        dataSpecialIndex.DataConfigIndex = new List<float>();

        for(int i = 0; i < System.Enum.GetNames(typeof(TypeSpecialIndex)).Length; i++)
        {
            dataSpecialIndex.DataConfigIndex.Add(yadeSheetDataSpecialIndex.GetCell(i + 1, indexNumber).GetFloat());
        }
    }

    #endregion

    [SerializeField] private YadeSheetData yadeSheetQuest;

    [SerializeField] private DataQuestDisplay dataQuestDisplay;

    private void ReadQuest()
    {
        List<string> listStringQuest = new List<string>();

        int indexRow = 4;

        int indexColumn = 3;

        for(int i = 0; i < System.Enum.GetNames(typeof(TypeQuestGame)).Length; i++)
        {
            try
            {
                listStringQuest.Add(yadeSheetQuest.GetCell(indexRow, indexColumn).GetValue());
            }
            catch
            {
                listStringQuest.Add("");
            }



            indexRow++;
        }

        dataQuestDisplay.Init(listStringQuest);
    }

    private void ReadUpdateBarrack()
    {
        //// index

        //int indexEffectType = 1;

        //int indexStatId = 2;

        //int indexValueAffect = 3;

        //int indexGemForce = 4;

        //int indexStatBonusGrow = 5;

        //int indexMaxTierUpgrade = 6;

        //int indexName = 7;

        //// logic

        //// Barrier

        //bool barrier = true;

        //int rowBarrier = 1;

        //DataUnlock.dataEachUnlocks[0].dataClassPacks = new List<List<DataClassPack>>();

        //while (YadeSheetDataBarrackBarrier.GetCell(rowBarrier, 0) != null)
        //{
        //    //if ()
        //    //{
        //    //    barrier = false;

        //    //    break;
        //    //}


        //    int EffectType = YadeSheetDataBarrackBarrier.GetCell(rowBarrier, indexEffectType).GetInt();

        //    int StatId = YadeSheetDataBarrackBarrier.GetCell(rowBarrier, indexStatId).GetInt();

        //    float ValueAffect = YadeSheetDataBarrackBarrier.GetCell(rowBarrier, indexValueAffect).GetFloat();

        //    int GemForce = YadeSheetDataBarrackBarrier.GetCell(rowBarrier, indexGemForce).GetInt();

        //    float StatBonusGrow = YadeSheetDataBarrackBarrier.GetCell(rowBarrier, indexStatBonusGrow).GetFloat();

        //    int MaxTierUpgrade = YadeSheetDataBarrackBarrier.GetCell(rowBarrier, indexMaxTierUpgrade).GetInt();

        //    string Name = YadeSheetDataBarrackBarrier.GetCell(rowBarrier, indexName).GetValue();

        //    //if(DataUnlock.dataEachUnlocks[0].dataClassPacks.Count < 1)
        //    //{
        //    //    DataUnlock.dataEachUnlocks[0].dataClassPacks = new List<List<DataClassPack>>();
        //    //}


        //    DataUnlock.dataEachUnlocks[0].dataClassPacks.Add(new List<DataClassPack>());

        //    for (int i = 0; i < MaxTierUpgrade; i++)
        //    {
        //        //DataUnlock.dataEachUnlocks[0].dataClassPacks.Add(new List<DataClassPack>());


        //        float valueEffectReal = ValueAffect + StatBonusGrow * i;

        //        string titleBonus = "No Bonus";

        //        if (StatId != 0)
        //        {
        //            string bonusKeyWord = "";

        //            switch ((TypeBonus)StatId)
        //            {
        //                case TypeBonus.Health:

        //                    bonusKeyWord = "Health";

        //                    break;
        //                case TypeBonus.Attack:

        //                    bonusKeyWord = "Damage";

        //                    break;
        //                case TypeBonus.AttackSpeed:

        //                    bonusKeyWord = "Attack Speed";

        //                    break;
        //                case TypeBonus.MoveSpeed:

        //                    bonusKeyWord = "Speed";

        //                    break;
        //                case TypeBonus.ReduceDamage:

        //                    bonusKeyWord = "Reduce Damage";

        //                    break;
        //                case TypeBonus.AttackRange:

        //                    bonusKeyWord = "Range Attack";

        //                    break;
        //            }

        //            titleBonus = "+ " + ((int)((valueEffectReal - 1) * 100)).ToString() + "% " + bonusKeyWord;
        //        }

        //        Unlock unlock = new Unlock()
        //        {
        //            TypeUnlock = TypeUnlock.Gem,
        //            IndexToUnlock = GemForce
        //        };

        //        if(i != 0)
        //        {
        //            unlock.TypeUnlock = TypeUnlock.None;
        //        }


        //        DataUnlock.dataEachUnlocks[0].dataClassPacks[rowBarrier - 1].Add(new DataClassPack()
        //        {
        //            unlockInBarrack = unlock,

        //            Title = Name,

        //            Bonus = new Bonus() 
        //            {
        //                TypeBonus = (TypeBonus)StatId,
        //                index = valueEffectReal,
        //                TitleBonus = titleBonus
        //            }
        //        });
        //    }

        //    rowBarrier++;
        //}

        //// melee

        //bool melee = true;

        //int rowMelee = 1;

        //DataUnlock.dataEachUnlocks[1].dataClassPacks = new List<List<DataClassPack>>();

        //while (YadeSheetDataBarrackMelee.GetCell(rowMelee, 0) != null)
        //{
        //    //if ()
        //    //{
        //    //    barrier = false;

        //    //    break;
        //    //}


        //    int EffectType = YadeSheetDataBarrackMelee.GetCell(rowMelee, indexEffectType).GetInt();

        //    int StatId = YadeSheetDataBarrackMelee.GetCell(rowMelee, indexStatId).GetInt();

        //    float ValueAffect = YadeSheetDataBarrackMelee.GetCell(rowMelee, indexValueAffect).GetFloat();

        //    int GemForce = YadeSheetDataBarrackMelee.GetCell(rowMelee, indexGemForce).GetInt();

        //    float StatBonusGrow = YadeSheetDataBarrackMelee.GetCell(rowMelee, indexStatBonusGrow).GetFloat();

        //    int MaxTierUpgrade = YadeSheetDataBarrackMelee.GetCell(rowMelee, indexMaxTierUpgrade).GetInt();

        //    string Name = YadeSheetDataBarrackMelee.GetCell(rowMelee, indexName).GetValue();

        //    //if(DataUnlock.dataEachUnlocks[0].dataClassPacks.Count < 1)
        //    //{
        //    //    DataUnlock.dataEachUnlocks[0].dataClassPacks = new List<List<DataClassPack>>();
        //    //}

        //    DataUnlock.dataEachUnlocks[1].dataClassPacks.Add(new List<DataClassPack>());

        //    for (int i = 0; i < MaxTierUpgrade; i++)
        //    {
        //        //DataUnlock.dataEachUnlocks[0].dataClassPacks.Add(new List<DataClassPack>());


        //        float valueEffectReal = ValueAffect + StatBonusGrow * i;

        //        string titleBonus = "No Bonus";

        //        if (StatId != 0)
        //        {
        //            string bonusKeyWord = "";

        //            switch ((TypeBonus)StatId)
        //            {
        //                case TypeBonus.Health:

        //                    bonusKeyWord = "Health";

        //                    break;
        //                case TypeBonus.Attack:

        //                    bonusKeyWord = "Damage";

        //                    break;
        //                case TypeBonus.AttackSpeed:

        //                    bonusKeyWord = "Attack Speed";

        //                    break;
        //                case TypeBonus.MoveSpeed:

        //                    bonusKeyWord = "Speed";

        //                    break;
        //                case TypeBonus.ReduceDamage:

        //                    bonusKeyWord = "Reduce Damage";

        //                    break;
        //                case TypeBonus.AttackRange:

        //                    bonusKeyWord = "Range Attack";

        //                    break;
        //            }

        //            titleBonus = "+ " + ((int)((valueEffectReal - 1) * 100.0f)).ToString() + " % " + bonusKeyWord;
        //        }

        //        Unlock unlock = new Unlock()
        //        {
        //            TypeUnlock = TypeUnlock.Gem,
        //            IndexToUnlock = GemForce
        //        };

        //        if (i != 0)
        //        {
        //            unlock.TypeUnlock = TypeUnlock.None;
        //        }


        //        DataUnlock.dataEachUnlocks[1].dataClassPacks[rowMelee - 1].Add(new DataClassPack()
        //        {
        //            unlockInBarrack = unlock,

        //            Title = Name,

        //            Bonus = new Bonus()
        //            {
        //                TypeBonus = (TypeBonus)StatId,
        //                index = valueEffectReal,
        //                TitleBonus = titleBonus
        //            }
        //        });
        //    }

        //    rowMelee++;
        //}

        //// range

        //bool range = true;

        //int rowRange = 1;

        //DataUnlock.dataEachUnlocks[2].dataClassPacks = new List<List<DataClassPack>>();

        //while (YadeSheetDataBarrackRange.GetCell(rowRange, 0) != null)
        //{
        //    //if ()
        //    //{
        //    //    barrier = false;

        //    //    break;
        //    //}


        //    int EffectType = YadeSheetDataBarrackRange.GetCell(rowRange, indexEffectType).GetInt();

        //    int StatId = YadeSheetDataBarrackRange.GetCell(rowRange, indexStatId).GetInt();

        //    float ValueAffect = YadeSheetDataBarrackRange.GetCell(rowRange, indexValueAffect).GetFloat();

        //    int GemForce = YadeSheetDataBarrackRange.GetCell(rowRange, indexGemForce).GetInt();

        //    float StatBonusGrow = YadeSheetDataBarrackRange.GetCell(rowRange, indexStatBonusGrow).GetFloat();

        //    int MaxTierUpgrade = YadeSheetDataBarrackRange.GetCell(rowRange, indexMaxTierUpgrade).GetInt();

        //    string Name = YadeSheetDataBarrackRange.GetCell(rowRange, indexName).GetValue();

        //    //if(DataUnlock.dataEachUnlocks[0].dataClassPacks.Count < 1)
        //    //{
        //    //    DataUnlock.dataEachUnlocks[0].dataClassPacks = new List<List<DataClassPack>>();
        //    //}

        //    DataUnlock.dataEachUnlocks[2].dataClassPacks.Add(new List<DataClassPack>());



        //    for (int i = 0; i < MaxTierUpgrade; i++)
        //    {
        //        //DataUnlock.dataEachUnlocks[0].dataClassPacks.Add(new List<DataClassPack>());

        //        float valueEffectReal = ValueAffect + StatBonusGrow * i;

        //        string titleBonus = "No Bonus";

        //        if (StatId != 0)
        //        {
        //            string bonusKeyWord = "";

        //            switch ((TypeBonus)StatId)
        //            {
        //                case TypeBonus.Health:

        //                    bonusKeyWord = "Health";

        //                    break;
        //                case TypeBonus.Attack:

        //                    bonusKeyWord = "Damage";

        //                    break;
        //                case TypeBonus.AttackSpeed:

        //                    bonusKeyWord = "Attack Speed";

        //                    break;
        //                case TypeBonus.MoveSpeed:

        //                    bonusKeyWord = "Speed";

        //                    break;
        //                case TypeBonus.ReduceDamage:

        //                    bonusKeyWord = "Reduce Damage";

        //                    break;
        //                case TypeBonus.AttackRange:

        //                    bonusKeyWord = "Range Attack";

        //                    break;
        //            }

        //            titleBonus = "+ " + ((int)((valueEffectReal - 1) * 100.0f)).ToString() + " % " + bonusKeyWord;
        //        }


        //        Unlock unlock = new Unlock()
        //        {
        //            TypeUnlock = TypeUnlock.Gem,
        //            IndexToUnlock = GemForce
        //        };

        //        if (i != 0)
        //        {
        //            unlock.TypeUnlock = TypeUnlock.None;
        //        }


        //        DataUnlock.dataEachUnlocks[2].dataClassPacks[rowRange - 1].Add(new DataClassPack()
        //        {
        //            unlockInBarrack = unlock,

        //            Title = Name,

        //            Bonus = new Bonus()
        //            {
        //                TypeBonus = (TypeBonus)StatId,
        //                index = valueEffectReal,
        //                TitleBonus = titleBonus
        //            }
        //        });
        //    }

        //    rowRange++;
        //}
    }

    public void ParseFrom(string s)
    {
        
    }

    #region ReadRewardMap

    [SerializeField] private YadeSheetData yadeSheetDataRewardMap;

    [SerializeField] private List<DataRewardMap> dataRewardMaps;

    public void ReadRewardMap()
    {
        // index

        int numberQuest = 3;

        int numberStar_1 = 15;

        int numberStar_2 = 30;

        int numberStar_3 = 45;

        int indexGold = 3;

        int indexGem = 5;

        // read

        int column = 1;

        int indexAdd = 0;

        for(int i = 0; i < dataRewardMaps.Count; i++)
        {
            while(indexAdd < numberQuest)
            {
                float gold = yadeSheetDataRewardMap.GetCell(column, indexGold).GetFloat();

                float gem = yadeSheetDataRewardMap.GetCell(column, indexGem).GetFloat();

                dataRewardMaps[i].DataEachRewardStages[indexAdd].GemEarn = (int)gem;

                dataRewardMaps[i].DataEachRewardStages[indexAdd].GoldEarn = (int)gold;

                column++;
                indexAdd++;
            }

            indexAdd = 0;
        }
    }

    #endregion

    #region ReadDataShop

    [SerializeField] private YadeSheetData yadeSheetDataShop;

    [SerializeField] private DataGetTicket dataGetTicket;

    [SerializeField] private DataGetGold dataGetGold;

    [SerializeField] private DataSpecialWeapon dataSpecialWeapon;

    [SerializeField] private DataGetGem dataGetGem;

    public void ReadDataShop()
    {
        // read ticket

        int indexColumnTicket = 9;

        int indexRowEarnTicket = 1;

        int indexRowPriceTicket = 2;


        for(int i = 0; i < dataGetTicket.ElementGetTickets.Count; i++)
        {
            dataGetTicket.ElementGetTickets[i].IndexEarn = yadeSheetDataShop.GetCell(indexColumnTicket, indexRowEarnTicket).GetFloat();

            dataGetTicket.ElementGetTickets[i].Buy.Index = yadeSheetDataShop.GetCell(indexColumnTicket, indexRowPriceTicket).GetFloat();

            indexColumnTicket++;
        }


        // read gold

        int indexColumnGold = 2;

        int indexRowEarnBaseGold = 1;

        int indexRowEarnGrowGold = 2;

        int indexRowPriceGold = 3;

        for(int i = 0; i < dataGetGold.ElementGetGolds.Count; i++)
        {
            dataGetGold.ElementGetGolds[i].IndexEarn = yadeSheetDataShop.GetCell(indexColumnGold, indexRowEarnBaseGold).GetFloat();

            dataGetGold.ElementGetGolds[i].IndexGrow = yadeSheetDataShop.GetCell(indexColumnGold, indexRowEarnGrowGold).GetFloat();

            dataGetGold.ElementGetGolds[i].Buy.Index = yadeSheetDataShop.GetCell(indexColumnGold, indexRowPriceGold).GetFloat();

            indexColumnGold++;
        }

        // read nuclear

        int indexColumnNuclear = 16;

        int indexRowEarnNuclear = 1;

        int indexRowPriceNuclear = 2;

        for(int i = 0; i < dataSpecialWeapon.ElementGetSpecials.Count; i++)
        {
            dataSpecialWeapon.ElementGetSpecials[i].IndexEarn = yadeSheetDataShop.GetCell(indexColumnNuclear, indexRowEarnNuclear).GetFloat();

            dataSpecialWeapon.ElementGetSpecials[i].Buy.Index = yadeSheetDataShop.GetCell(indexColumnNuclear, indexRowPriceNuclear).GetFloat();

            indexColumnNuclear++;
        }

        // read gem

        int indexColumnGem = 29;

        int indexRowEarnGem = 1;

        int indexRowPriceGem = 2;

        for(int i = 0; i < dataGetGem.ElementGetGems.Count; i++)
        {
            dataGetGem.ElementGetGems[i].IndexEarn = yadeSheetDataShop.GetCell(indexColumnGem, indexRowEarnGem).GetFloat();

            dataGetGem.ElementGetGems[i].Buy.Index = yadeSheetDataShop.GetCell(indexColumnGem, indexRowPriceGem).GetFloat();

            indexColumnGem++;
        }

    }

    #endregion
}
