using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;
using DG.Tweening;

public class DataManager : MonoBehaviour
{
    [SerializeField] private DataManagerMainGame dataManagerMainGame;
    
    [SerializeField] private DataUnlock dataUnlock;

    private int numberReward;

    public DataUnlock DataUnlock => dataUnlock;

    public delegate void EventChangeMusic(bool isActive);

    public delegate void EventChangeSound(bool isActive);

    public delegate void EventChangeGold(bool hasAction);

    public delegate void EventChangeGem(bool hasAction);

    public delegate void EventChangeTicket(bool hasAction);

    public delegate void EventChangeSlotEquip(TypeSlotEquip typeSlotEquip, TypeGroup typeGroup, TypeTier typeTier, TypeId typeId);

    public delegate void EventChangeStar();

    public delegate void EventChangeLevelAlly();

    public delegate void EventDoneUnlockLv3();

    public EventChangeMusic OnChangeMusic;

    public EventChangeSound OnChangeSound;

    public EventChangeGold OnChangeGold;

    public EventChangeGem OnChangeGem;

    public EventChangeTicket OnChangeTicket;

    public EventChangeSlotEquip OnChangeSlotEquip;

    public EventChangeStar OnChangeStar;

    public EventChangeLevelAlly OnChangeLevelAlly;

    public EventDoneUnlockLv3 OnDoneUnlockLv3;


    private const int NUMBER_REWARD_SHOP_PER_DAY = 3;

    public DataManagerMainGame DataManagerMainGame => dataManagerMainGame;

    private bool canRebornTicket;

    private float timeCount;

    private bool hasDoneTut;

    private bool isTutorialUpgradeLobby;

    // use to manager player_pref & data user

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for(int i = 0; i < dataManagerMainGame.dataConfigForTypeChars.Count; i++)
        {
            for(int j = 0; j < dataManagerMainGame.dataConfigForTypeChars[i].Count; j++)
            {
                for(int k = 0; k < dataManagerMainGame.dataConfigForTypeChars[i][j].Count; k++)
                {
                    if (!CheckContainStarAlly((TypeGroup)i, (TypeTier)j, (TypeId)k))
                    {
                        SetStarAlly(dataManagerMainGame.dataConfigForTypeChars[i][j][k].dataConfigForTypeCharBase.BaseStar, (TypeGroup)i, (TypeTier)j, (TypeId)k);
                    }
                }
            }
        }

        InitRebornTicket();

        OnChangeTicket += InitRebornTicket;
    }

    public bool CheckIsHack()
    {
        return GameManager.Instance.IsHack;
    }


    public void LevelUpStarAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        int a = GetStarAlly(typeGroup, typeTier, typeId);

        if(a + 1 > 5)
        {
            return;
        }

        SetStarAlly(a + 1, typeGroup, typeTier, typeId);

        HandleFireBase.Instance.LogEventWithParameter("Level_Up_Star_Ally", new FirebaseParam[]
        {
            new FirebaseParam("Id_Ally", typeGroup.ToString() + typeTier.ToString() + typeId.ToString()),
            new FirebaseParam("Level_Star_To", a + 1),
            new FirebaseParam("Level_Stage", GetLevelMaxUnlock())
        });

        OnChangeStar?.Invoke();
    }

    public void SetStarAlly(int numberStar, TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        PlayerPrefs.SetInt("StarAlly" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString(), numberStar);
    }

    public int GetStarAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return PlayerPrefs.GetInt("StarAlly" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString(), 1);
    }
    
    public bool CheckContainStarAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return PlayerPrefs.HasKey("StarAlly" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString());
    }

    public bool CheckIsMaxStarAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return GetStarAlly(typeGroup, typeTier, typeId) < 5 ? false : true;
    }

    public bool CheckIsMaxLevelInStarAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        int star = GetStarAlly(typeGroup, typeTier, typeId);

        int level = GetLevelAlly(typeGroup, typeTier, typeId);

        int levelCanReach = star * 20;

        return level < levelCanReach ? false : true;
    }

    public int GetLevelAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        return PlayerPrefs.GetInt("LevelAlly" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString(), 1);
    }

    public void SetLevelUpAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        int a = GetLevelAlly(typeGroup, typeTier, typeId);

        PlayerPrefs.SetInt("LevelAlly" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString(), a + 1);

        HandleFireBase.Instance.LogEventWithParameter("Upgrade_Ally", new FirebaseParam[]
        {
            new FirebaseParam("Id_Ally", typeGroup.ToString() + typeTier.ToString() + typeId.ToString()),
            new FirebaseParam("Level_Upgrade_To", a + 1),
            new FirebaseParam("Level_Stage", GetLevelMaxUnlock())
        });

        OnChangeLevelAlly?.Invoke();
    }

    public void SetLevelAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId, int level)
    {
        int a = GetLevelAlly(typeGroup, typeTier, typeId);

        PlayerPrefs.SetInt("LevelAlly" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString(), level);
    }

    public TypeEquip GetEquipAlly(TypeSlotEquip typeSlotEquip)
    {
        TypeEquip defaultTypeEquip = new TypeEquip()
        {
            TypeGroup = TypeGroup.Barrier,
            TypeTier = TypeTier.C,
            TypeId = TypeId.Id0
        };

        switch (typeSlotEquip)
        {
            case TypeSlotEquip.Slot1:

                defaultTypeEquip.TypeGroup = TypeGroup.Barrier;

                break;
            case TypeSlotEquip.Slot2:

                defaultTypeEquip.TypeGroup = TypeGroup.Vanguard;

                break;
            case TypeSlotEquip.Slot3:

                defaultTypeEquip.TypeGroup = TypeGroup.Gunner;

                break;
        }

        string defaultString = JsonUtility.ToJson(defaultTypeEquip);

        var a = JsonUtility.FromJson<TypeEquip>(PlayerPrefs.GetString("Slot Equip" + typeSlotEquip.ToString(), defaultString));

        return a;
    }

    public void SetEquipAlly(TypeSlotEquip typeSlotEquip, TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        TypeEquip defaultTypeEquip = new TypeEquip()
        {
            TypeGroup = typeGroup,
            TypeTier = typeTier,
            TypeId = typeId
        };

        string defaultString = JsonUtility.ToJson(defaultTypeEquip);

        PlayerPrefs.SetString("Slot Equip" + typeSlotEquip.ToString(), defaultString);

        HandleFireBase.Instance.LogEventWithParameter("Equip_Ally", new FirebaseParam[]
        {
            new FirebaseParam("Id_Ally", typeGroup.ToString() + typeTier.ToString() + typeId.ToString()),
            new FirebaseParam("Level_Ally", GetLevelAlly(typeGroup, typeTier, typeId)),
            new FirebaseParam("Slot_Id", (int)typeSlotEquip),
            new FirebaseParam("Level_Stage", GetLevelMaxUnlock()),

        });

        OnChangeSlotEquip?.Invoke(typeSlotEquip, typeGroup, typeTier, typeId);
    }

    public void SetUnlockAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        PlayerPrefs.SetInt("UnlockAllyInUnit" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString(), 1);

        HandleFireBase.Instance.LogEventWithParameter("Unlock_Ally", new FirebaseParam[] 
        {
            new FirebaseParam("Id_Ally", typeGroup.ToString() + typeTier.ToString() + typeId.ToString()),
            new FirebaseParam("Level_Stage", GetLevelMaxUnlock())
        });

        OnChangeLevelAlly?.Invoke();
    }

    public bool GetUnlockAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        if (CheckIsHack())
        {
            return true;
        }

        if(typeTier == TypeTier.C && typeId == TypeId.Id0 && (typeGroup == TypeGroup.Barrier || typeGroup == TypeGroup.Vanguard))
        {
            return true;
        }
        else
        {
            return PlayerPrefs.GetInt("UnlockAllyInUnit" + typeGroup.ToString() + typeTier.ToString() + typeId.ToString(), 0) == 1? true : false;
        }
    }

    public int GetMap()
    {
        if (CheckIsHack())
        {
            return PlayerPrefs.GetInt("CurrentMap", 2);
        }
        else
        {
            return PlayerPrefs.GetInt("CurrentMap", 1);
        }
    }

    public void LevelUpMap()
    {
        int i = GetMap();

        if(CheckIsMaxMap())
        {
            PlayerPrefs.SetInt("CurrentMap", i);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentMap", i + 1);
        }
    }

    public void LevelDownMap()
    {
        int i = GetMap();

        if ((i - 1) < 1)
        {
            PlayerPrefs.SetInt("CurrentMap", i);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentMap", i - 1);
        }
    }

    public void SetMap(int map)
    {
        PlayerPrefs.SetInt("CurrentMap", map);
    }


    private bool CheckIsMaxMap()
    {
        int a = GetMap();

        if(a < dataManagerMainGame.DataMapRender.dataMapRenders.Count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt(Help.DATA_LEVEL, 1);
    }

    public void SetLevel(int levelSet)
    {
        int maxLevelPerMap = 15;

        SetMap(((levelSet - 1) / maxLevelPerMap) + 1);

        PlayerPrefs.SetInt(Help.DATA_LEVEL, levelSet);
    }

    public void LevelUp()
    {
        int maxLevelPerMap = 15;

        int currentLevel = GetLevel();

        if(currentLevel < GetLevelMaxUnlock())
        {
            SetMap(((currentLevel) / maxLevelPerMap) + 1);

            PlayerPrefs.SetInt(Help.DATA_LEVEL, currentLevel + 1);

            return;
        }

        if((currentLevel + 1) >= (maxLevelPerMap * dataManagerMainGame.DataMapRender.dataMapRenders.Count))
        {
            //LevelUpMap();

            return;
        }

        SetMap(((currentLevel) / maxLevelPerMap) + 1);

        SetLevelMaxUnlock(currentLevel + 1);

        PlayerPrefs.SetInt(Help.DATA_LEVEL, currentLevel + 1);
    }

    public int GetLevelMaxUnlock()
    {
        if (CheckIsHack())
        {
            return PlayerPrefs.GetInt("LevelMaxUnlock", 30);
        }
        else
        {
            return PlayerPrefs.GetInt("LevelMaxUnlock", 1);
        }


    }

    public void SetLevelMaxUnlock(int level)
    {
        PlayerPrefs.SetInt("LevelMaxUnlock", level);
    }

    public int GetLevelPerMap(int idMap)
    {
        int numberLevelPerMap = 15;

        int currentLevel = GetLevel();

        if (CheckIsMaxMap() && idMap == GetMap() && currentLevel - ((idMap + 1) * numberLevelPerMap) > 0)
        {
            return numberLevelPerMap;
        }

        int a = currentLevel - (numberLevelPerMap * (idMap));

        int b = a % numberLevelPerMap;

        if(b == 0)
        {
            return numberLevelPerMap;
        }
        else
        {
            return b;
        }
    }

    public bool GetSetting(TypeSetting typeSetting)
    {
        int a = PlayerPrefs.GetInt("Setting" + typeSetting.ToString(), 1);

        return a == 1 ? true : false;
    }

    public void SetSetting(TypeSetting typeSetting, bool isAvtive)
    {
        int a = 1;

        a = isAvtive ? 1 : 0;

        PlayerPrefs.SetInt("Setting" + typeSetting.ToString(), a);

        switch (typeSetting)
        {
            case TypeSetting.Music:

                OnChangeMusic?.Invoke(isAvtive);

                break;
            case TypeSetting.Sound:

                OnChangeSound?.Invoke(isAvtive);

                break;
        }
    }

    public void AddGold(long number, string positionAdd, bool hasAction = false)
    {
        long a = GetGold();

        PlayerPrefs.SetString("Gold", (a + number).ToString());

        HandleFireBase.Instance.LogEventWithString(positionAdd);

        OnChangeGold?.Invoke(hasAction);
    }

    public long GetGold()
    {
        if (CheckIsHack())
        {
            string a = PlayerPrefs.GetString("Gold", (1000000000).ToString());

            return System.Int64.Parse(a);
        }
        else
        {
            string a = PlayerPrefs.GetString("Gold", dataManagerMainGame.DataGame.GoldStart.ToString());

            return System.Int64.Parse(a);
        }


    }

    public void AddGem(long number, string positionAdd, bool hasAction = false)
    {
        long a = GetGem();

        PlayerPrefs.SetString("Gem", (a + number).ToString());

        HandleFireBase.Instance.LogEventWithString(positionAdd);

        OnChangeGem?.Invoke(hasAction);
    }

    public long GetGem()
    {
        if (CheckIsHack())
        {
            string a = PlayerPrefs.GetString("Gem", (1000000000).ToString());

            return System.Int64.Parse(a);
        }
        else
        {
            string a = PlayerPrefs.GetString("Gem", dataManagerMainGame.DataGame.GemStart.ToString());

            return System.Int64.Parse(a);
        }


    }

    public int GetTicket()
    {
        if (CheckIsHack())
        {
            return PlayerPrefs.GetInt("Ticket", 1000000);
        }
        else
        {
            return PlayerPrefs.GetInt("Ticket", dataManagerMainGame.DataGame.TicketStart);
        }


    }

    public void AddTicket(int number, string positionAdd, bool hasAction = false)
    {
        int a = GetTicket();

        PlayerPrefs.SetInt("Ticket", a + number);

        HandleFireBase.Instance.LogEventWithString(positionAdd);

        OnChangeTicket?.Invoke(hasAction);
    }

    public void RebornTicket(bool hasAction = false)
    {
        int a = GetTicket();

        PlayerPrefs.SetInt("Ticket", a + dataManagerMainGame.DataGame.TicketReborn);

        PlayerPrefs.SetString("TimeStartRebornTicket", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        OnChangeTicket?.Invoke(hasAction);
    }

    public void RebornMaxTicket(bool hasAction = false)
    {
        PlayerPrefs.SetInt("Ticket", dataManagerMainGame.DataGame.MaxTicket);

        OnChangeTicket?.Invoke(hasAction);
    }

    public void StartCountRebornTicket()
    {
        PlayerPrefs.SetString("TimeStartRebornTicket", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        PlayerPrefs.SetInt("RebornTicket", 1);

        timeCount = GetTimeCountRebornTicket();

        canRebornTicket = true;
    }

    private void Update()
    {
        if (canRebornTicket)
        {
            if(timeCount <= 0)
            {
                InitRebornTicket();
            }
            else
            {
                timeCount -= Time.deltaTime;
            }
        }
    }

    public int GetTimeCountTicket()
    {
        return (int)timeCount;
    }

    public void StopCountRebornTicket()
    {
        canRebornTicket = false;

        PlayerPrefs.SetInt("RebornTicket", 0);
    }

    public bool GetCanReborn()
    {
        return canRebornTicket;
    }

    public void InitRebornTicket(bool hasAction = false)
    {
        if(GetTicket() >= dataManagerMainGame.DataGame.MaxTicket)
        {
            StopCountRebornTicket();
        }
        else
        {
            int a = GetTimeRealRebornTicket() / dataManagerMainGame.DataGame.SecondToRebornTicket;

            //Debug.Log(a);

            if((a * dataManagerMainGame.DataGame.TicketReborn + GetTicket()) >= dataManagerMainGame.DataGame.MaxTicket)
            {
                RebornMaxTicket();

                StopCountRebornTicket();
            }
            else
            {
                for(int i = 0; i < a; i++)
                {
                    RebornTicket();
                }

                StartCountRebornTicket();      
            }
        }
    }

    public int GetTimeRealRebornTicket()
    {
        if (!PlayerPrefs.HasKey("TimeStartRebornTicket"))
        {
            PlayerPrefs.SetString("TimeStartRebornTicket", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        string a = PlayerPrefs.GetString("TimeStartRebornTicket", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        try
        {
            int hour = int.Parse(b.Substring(11, 2)) - int.Parse(a.Substring(11, 2));

            int minue = int.Parse(b.Substring(14, 2)) - int.Parse(a.Substring(14, 2));

            int second = int.Parse(b.Substring(17, 2)) - int.Parse(a.Substring(17, 2));

            int realSecond = hour * 3600 + minue * 60 + second;

            //int timeCount = dataManagerMainGame.DataGame.SecondToRebornTicket - realSecond;

            //if (timeCount <= 0)
            //{
            //    InitRebornTicket();
            //}

            return realSecond;
        }
        catch
        {
            return 0;
        }
    }

    public int GetTimeCountRebornTicket()
    {
        if (!PlayerPrefs.HasKey("TimeStartRebornTicket"))
        {
            PlayerPrefs.SetString("TimeStartRebornTicket", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        string a = PlayerPrefs.GetString("TimeStartRebornTicket", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        try
        {
            int hour = int.Parse(b.Substring(11, 2)) - int.Parse(a.Substring(11, 2));

            int minue = int.Parse(b.Substring(14, 2)) - int.Parse(a.Substring(14, 2));

            int second = int.Parse(b.Substring(17, 2)) - int.Parse(a.Substring(17, 2));

            int realSecond = hour * 3600 + minue * 60 + second;

            int timeCount = dataManagerMainGame.DataGame.SecondToRebornTicket - realSecond;

            if(timeCount <= 0)
            {
                //InitRebornTicket();
            }

            return timeCount;
        }
        catch
        {
            return 0;
        }
    }

    public void SetUnlockPackShop(int idPackShop)
    {
        PlayerPrefs.SetInt("PackShop" + idPackShop, 1);
    }

    public bool GetUnlockPackShop(int idPackShop)
    {
        int a = PlayerPrefs.GetInt("PackShop" + idPackShop, 0);

        return a == 1 ? true : false;
    }

    public bool CheckDaily()
    {
        string a = PlayerPrefs.GetString("date time", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if(a.Contains(b.Substring(0, 4)) && a.Contains(b.Substring(5, 2)) && a.Contains(b.Substring(8, 2)))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetDaily()
    {
        PlayerPrefs.SetString("date time", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        //Debug.Log(PlayerPrefs.GetString("date time"));
    }

    public bool GetRate()
    {
        int a = PlayerPrefs.GetInt("Rate", 0);

        return a == 1 ? true : false;
    }

    public void SetRate()
    {
        PlayerPrefs.SetInt("Rate", 1);
    }

    public void ResetConfigLevel()
    {
        PlayerPrefs.SetInt("ConfigCheckLevel", 0);
    }

    public int GetIndexCheck()
    {
        return PlayerPrefs.GetInt("ConfigCheckLevel", 0);
    }

    public void SetConfigLevel()
    {
        int a = GetIndexCheck();

        PlayerPrefs.SetInt("ConfigCheckLevel", a + 1);
    }

    public bool GetFreeSpin()
    {
        int a = PlayerPrefs.GetInt("FreeSpinLobby", 0);

        return a == 0 ? true : false;
    }

    public void SetFreeSpin()
    {
        PlayerPrefs.SetInt("FreeSpinLobby", 1);
    }

    public bool GetRewardSpin()
    {
        if (!PlayerPrefs.HasKey("RewardSpinLobby"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("RewardSpinLobby", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        //Debug.Log(PlayerPrefs.GetInt("Number_RewardGoldShop", 0));

        if (!a.Contains(b.Substring(8, 2)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetRewardSpin()
    {
        PlayerPrefs.SetString("RewardSpinLobby", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    }

    public void SetRefreshDailyQuest()
    {
        PlayerPrefs.SetString("DailyQuestTime", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    }

    public bool CheckRefreshDailyQuest()
    {
        if (!PlayerPrefs.HasKey("DailyQuestTime"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("DailyQuestTime", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        //int dayA = Int32.Parse(a.Substring(8, 2));

        //int dayB = Int32.Parse(b.Substring(8, 2));

        if (!a.Contains(b.Substring(8, 2)))
        {
            if(Int32.Parse(b.Substring(11, 2)) >= 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CheckHasClaim()
    {


        return false;
    }

    public DataLevelStage GetLevelStage(int level)
    {
        DataLevelStage typeEquip = new DataLevelStage()
        {
            HasComplete = false,
            StarMax = 0
        };

        string defaultString = JsonUtility.ToJson(typeEquip);

        var a = PlayerPrefs.GetString("LevelStage" + level.ToString(), defaultString);

        return JsonUtility.FromJson<DataLevelStage>(a);
    }

    public void SetLevelStage(int level, int starMax)
    {
        var a = GetLevelStage(level);

        a.HasComplete = true;

        if(starMax > a.StarMax)
        {
            a.StarMax = starMax;
        }

        string stringSet = JsonUtility.ToJson(a);

        PlayerPrefs.SetString("LevelStage" + level.ToString(), stringSet);
    }

    public void AddBoom(int number)
    {
        int a = GetBoom();

        PlayerPrefs.SetInt("BoomInGame", a + number);
    }

    public int GetBoom()
    {
        return PlayerPrefs.GetInt("BoomInGame", dataManagerMainGame.DataGame.NuclearStart);
    }

    public bool HasTutorialBoom()
    {
        return PlayerPrefs.GetInt("TutorialBoomLv7", 0) == 1 ? true : false;
    }

    public void SetHasTutorialBoom()
    {
        PlayerPrefs.SetInt("TutorialBoomLv7", 1);
    }

    public DataCard GetDataCard(TypeSlotEquip typeSlotEquip)
    {
        var a = GetEquipAlly(typeSlotEquip);

        ConfigBaseIndex configBaseIndex = dataManagerMainGame.GetConfigBaseIndex(a.TypeGroup, a.TypeTier, a.TypeId);

        DataCard dataCard = new DataCard()
        {
            TypeGroup = a.TypeGroup,
            TypeTier = a.TypeTier,
            TypeId = a.TypeId,

            Energy = configBaseIndex.dataConfigForTypeCharBase.Energy,
            Level = GetLevelAlly(a.TypeGroup, a.TypeTier, a.TypeId),
            Star = GetStarAlly(a.TypeGroup, a.TypeTier, a.TypeId)
        };

        return dataCard;
    }

    public DataCard GetDataCard(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        ConfigBaseIndex configBaseIndex = dataManagerMainGame.GetConfigBaseIndex(typeGroup, typeTier, typeId);

        DataCard dataCard = new DataCard()
        {
            TypeGroup = typeGroup,
            TypeTier = typeTier,
            TypeId = typeId,

            Energy = configBaseIndex.dataConfigForTypeCharBase.Energy,
            Level = GetLevelAlly(typeGroup, typeTier, typeId),
            Star = GetStarAlly(typeGroup, typeTier, typeId)
        };

        return dataCard;
    }

    public DataProfileAlly GetDataProfileAlly(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        ConfigBaseIndex configBaseIndex = dataManagerMainGame.GetConfigBaseIndex(typeGroup, typeTier, typeId);

        DataCard dataCard = GetDataCard(typeGroup, typeTier, typeId);

        float damage = configBaseIndex.dataConfigForTypeCharBase.Damage + configBaseIndex.dataConfigIndexGrow.AttackGrow * (dataCard.Level - 1);

        float health = configBaseIndex.dataConfigForTypeCharBase.HP + configBaseIndex.dataConfigIndexGrow.HPGrow * (dataCard.Level - 1);

        //for (int i = 0; i < dataCard.Level - 1; i++)
        //{
        //    damage = damage + configBaseIndex.dataConfigIndexGrow.AttackGrow;

        //    health = health + configBaseIndex.dataConfigIndexGrow.HPGrow;
        //}



        DataProfileAlly dataProfileAlly = new DataProfileAlly()
        {
            DataCard = dataCard,

            Name = configBaseIndex.dataConfigForTypeCharBase.Name,

            Description = configBaseIndex.dataConfigForTypeCharBase.Description,

            DescriptionAbility = configBaseIndex.dataConfigForTypeCharBase.DescriptionAbility,

            Damage = damage,

            Health = health,

            LevelConditionUnlock = configBaseIndex.dataConfigForTypeCharBase.levelConditionUnlockGold,

            GoldUnlock = configBaseIndex.dataConfigForTypeCharBase.goldUnlock,

            DesUnlockGold = configBaseIndex.dataConfigForTypeCharBase.stringDesUnlockGold,

            dataCardUnlockGold = configBaseIndex.dataCardConditionGold
        };

        return dataProfileAlly;
    }

    public bool CheckCanUnlockWithGold(DataProfileAlly dataProfileAlly)
    {
        int levelAllyToUnlock = GetLevelAlly(dataProfileAlly.dataCardUnlockGold.TypeGroup, dataProfileAlly.dataCardUnlockGold.TypeTier, dataProfileAlly.dataCardUnlockGold.TypeId);

        int goldNeed = dataProfileAlly.GoldUnlock;

        Debug.Log("Khang" + dataProfileAlly.LevelConditionUnlock);

        Debug.Log("Khang" + goldNeed);

        if((levelAllyToUnlock >= dataProfileAlly.LevelConditionUnlock) && (goldNeed <= GetGold()))
        {
            Debug.Log("Khang123");

            return true;
        }
        else
        {
            Debug.Log("Khang456");

            return false;
        }
    }

    public bool CheckCanEarnGemShop()
    {
        if (GetFreeGemShop() || CheckCanEarnRewardGemShop())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckCanEarnGoldShop()
    {
        if (GetFreeGoldShop() || CheckCanEardRewardGoldShop())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckCanEarnTicketShop()
    {
        if(GetFreeTicketShop() || CheckCanEardRewardTicketShop())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckCanEarnShop()
    {
        if (CheckCanEarnGemShop() || CheckCanEarnGoldShop() || CheckCanEarnTicketShop())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetEarnRewardGemShop()
    {
        PlayerPrefs.SetString("RewardGemShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        int a = PlayerPrefs.GetInt("Number_RewardGemShop", 0);

        PlayerPrefs.SetInt("Number_RewardGemShop", a + 1);
    }

    public void SetEarnRewardGoldShop()
    {
        PlayerPrefs.SetString("RewardGoldShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        int a = PlayerPrefs.GetInt("Number_RewardGoldShop", 0);

        PlayerPrefs.SetInt("Number_RewardGoldShop", a + 1);
    }

    public void SetEarnRewardTicketShop()
    {
        PlayerPrefs.SetString("RewardTicketShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        int a = PlayerPrefs.GetInt("Number_RewardTicketShop", 0);

        PlayerPrefs.SetInt("Number_RewardTicketShop", a + 1);
    }

    public bool CheckCanEarnRewardGemShop()
    {
        if (!PlayerPrefs.HasKey("RewardGemShop"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("RewardGemShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        Debug.Log(PlayerPrefs.GetInt("Number_RewardGemShop", 0));

        if (!a.Contains(b.Substring(8, 2)))
        {
            PlayerPrefs.SetInt("Number_RewardGemShop", 0);

            return true;
        }
        else
        {
            if (PlayerPrefs.GetInt("Number_RewardGemShop", 0) <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool CheckCanEardRewardGoldShop()
    {
        if (!PlayerPrefs.HasKey("RewardGoldShop"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("RewardGoldShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        Debug.Log(PlayerPrefs.GetInt("Number_RewardGoldShop", 0));

        if (!a.Contains(b.Substring(8, 2)))
        {
            PlayerPrefs.SetInt("Number_RewardGoldShop", 0);

            return true;
        }
        else
        {
            if (PlayerPrefs.GetInt("Number_RewardGoldShop", 0) <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool CheckCanEardRewardTicketShop()
    {
        if (!PlayerPrefs.HasKey("RewardTicketShop"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("RewardTicketShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        Debug.Log(PlayerPrefs.GetInt("Number_RewardTicketShop", 0));

        if (!a.Contains(b.Substring(8, 2)))
        {
            PlayerPrefs.SetInt("Number_RewardTicketShop", 0);

            return true;
        }
        else
        {
            if (PlayerPrefs.GetInt("Number_RewardTicketShop", 0) <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool GetFreeGemShop()
    {
        if (!PlayerPrefs.HasKey("FreeGemShop"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("FreeGemShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (!a.Contains(b.Substring(8, 2)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFreeGemShop()
    {
        PlayerPrefs.SetString("FreeGemShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    }

    public bool GetFreeGoldShop()
    {
        if (!PlayerPrefs.HasKey("FreeGoldShop"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("FreeGoldShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (!a.Contains(b.Substring(8, 2)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFreeGoldShop()
    {
        PlayerPrefs.SetString("FreeGoldShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    }

    public bool GetFreeTicketShop()
    {
        if (!PlayerPrefs.HasKey("FreeTicketShop"))
        {
            return true;
        }

        string a = PlayerPrefs.GetString("FreeTicketShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

        string b = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (!a.Contains(b.Substring(8, 2)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFreeTicketShop()
    {
        PlayerPrefs.SetString("FreeTicketShop", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    }

    public int GetStarInMap(int map)
    {
        int a = 0;

        for(int i = 15 * (map - 1); i < 15 * map; i++)
        {
            a += GetLevelStage(i + 1).StarMax;
        }

        return a;
    }

    public bool CheckGetRewardInMap(int star, int idMap)
    {
        return PlayerPrefs.GetInt("GetRewardInMap" + star.ToString() + idMap.ToString(), 0) == 1 ? true : false;
    }

    public void SetGetRewardInMap(int star, int idMap)
    {
        PlayerPrefs.SetInt("GetRewardInMap" + star.ToString() + idMap.ToString(), 1);
    }

    public bool CheckCanGetRewardInMap(int idMap)
    {
        int currentStar = GetStarInMap(idMap);

        if(currentStar < 15)
        {
            return false;
        }
        else
        {
            if(currentStar < 30)
            {
                if (!CheckGetRewardInMap(15, idMap))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (currentStar < 45)
                {
                    if (!CheckGetRewardInMap(30, idMap) || !CheckGetRewardInMap(15, idMap))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (!CheckGetRewardInMap(30, idMap) || !CheckGetRewardInMap(15, idMap) || !CheckGetRewardInMap(45, idMap))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }

    public void SetFirstDron()
    {
        PlayerPrefs.SetInt("FirstDron", 1);
    }

    public bool GetFirstDron()
    {
        return PlayerPrefs.GetInt("FirstDron", 0) == 0 ? true : false;
    }

    public bool GetUnlockBtnSwap()
    {
        return PlayerPrefs.GetInt("", 1) == 1 ? true : false;
    }

    public void SetUnlockBtnSwap()
    {
        PlayerPrefs.SetInt("UnlockBtnSwap", 1);
    }

    public bool GetHasTutorialChooseChar()
    {
        return PlayerPrefs.GetInt("TutorialChooseChar", 0) == 1 ? true : false;
    }

    public void SetHasTutorialChooseChar()
    {
        PlayerPrefs.SetInt("TutorialChooseChar", 1);
    }

    public bool GetHasTutorialBtnNextLv1()
    {
        return PlayerPrefs.GetInt("TutorialBtnNextLv1", 0) == 1 ? true : false;
    }

    public void SetHasTutorialBtnNextLv1()
    {
        PlayerPrefs.SetInt("TutorialBtnNextLv1", 1);
    }

    public bool GetHasTutorialBtnHomeLv2()
    {
        return PlayerPrefs.GetInt("TutorialBtnHomeLv2", 0) == 1? true : false;
    }

    public void SetHasTutorialBtnHomeLv2()
    {
        PlayerPrefs.SetInt("TutorialBtnHomeLv2", 1);
    }

    public bool GetHasTutorialBtnHomeLv3()
    {
        return PlayerPrefs.GetInt("TutorialBtnHomeLv3", 0) == 1 ? true : false;
    }

    public void SetHasTutorialBtnHomeLv3()
    {
        PlayerPrefs.SetInt("TutorialBtnHomeLv3", 1);
    }

    public bool GetHasTutorialBtnHomeLv4()
    {
        return PlayerPrefs.GetInt("TutorialBtnHomeLv4", 0) == 1 ? true : false;
    }

    public void SetHasTutorialBtnHomeLv4()
    {
        PlayerPrefs.SetInt("TutorialBtnHomeLv4", 1);
    }

    public bool GetHasTutorialBtnHomeLv5()
    {
        return PlayerPrefs.GetInt("TutorialBtnHomeLv5", 0) == 1 ? true : false;
    }

    public void SetHasTutorialBtnHomeLv5()
    {
        PlayerPrefs.SetInt("TutorialBtnHomeLv5", 1);
    }

    public bool GetHasTutorialLobbyLv3()
    {
        return PlayerPrefs.GetInt("TutorialLobbyLv3", 0) == 1 ? true : false;
    }

    public void SetHasTutorialLobbyLv3()
    {
        PlayerPrefs.SetInt("TutorialLobbyLv3", 1);
    }

    public void SetEquidTutLv3()
    {
        hasDoneTut = true;

        OnDoneUnlockLv3?.Invoke();
    }

    public bool GetDoneTutEquidLv3()
    {
        return hasDoneTut;
    }

    public bool GetHasTutorialLobbyLv6()
    {
        return PlayerPrefs.GetInt("TutorialLobbyLv6", 0) == 1 ? true : false;
    }

    public void SetHasTutorialLobbyLv6()
    {
        PlayerPrefs.SetInt("TutorialLobbyLv6", 1);
    }

    public bool GetHasTutorialUpgrade()
    {
        return (PlayerPrefs.GetInt("TutorialUpgradeLobby", 0) == 1 ? true : false);
    }

    public void SetHasTutorialUpgrade()
    {
        PlayerPrefs.SetInt("TutorialUpgradeLobby", 1);
    }

    public void SetIsTutorialUpgradeLobby()
    {
        isTutorialUpgradeLobby = true;
    }

    public bool GetIsTutorialUpgradeLobby()
    {
        return isTutorialUpgradeLobby;
    }

    public int GetLoseInLevel(int level)
    {
        return PlayerPrefs.GetInt("LoseInLevel" + level.ToString(), 0);
    }

    public void SetLoseInLevel(int level)
    {
        int a = GetLoseInLevel(level);

        PlayerPrefs.SetInt("LoseInLevel" + level.ToString(), a + 1);
    }
}

public class TimeEdit
{
    public int Hour;
    public int Minue;
    public int Second;
}

public class TimeUitls
{
    public static long GetTimestamp(DateTime dateTime)
    {
        long unixTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        return unixTime;
    }
    public static DateTime TimestampToDatetime(long value)
    {
        return DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
    }
    public static string ToHMS(DateTime value)
    {
        return string.Format("{0:00}:{1:00}:{2:00}", value.Hour, value.Minute, value.Second);
    }
    public static string ToDMY(DateTime value)
    {
        return string.Format("{0:00}:{1:00}:{2:00}", value.Day, value.Month, value.Year);
    }
    public static string ToMS(DateTime value)
    {
        return string.Format("{0:00}:{1:00}", value.Minute, value.Second);
    }
    public static string ToDHM(DateTime value)
    {
        return string.Format("{0:00}:{1:00}:{2:00}", value.Day, value.Hour, value.Minute);
    }
}

public class TypeEquip
{
    public TypeGroup TypeGroup;

    public TypeTier TypeTier;

    public TypeId TypeId;
}

public class DataLevelStage
{
    public bool HasComplete;

    public int StarMax;
}

public class DataCard
{
    public int Energy;

    public int Star;

    public int Level;

    public TypeGroup TypeGroup;

    public TypeTier TypeTier;

    public TypeId TypeId;
}

public class DataProfileAlly
{
    public DataCard DataCard;

    public float Health;

    public float Damage;

    public string Name;

    public string Description;

    public string DescriptionAbility;

    public int LevelConditionUnlock;

    public int GoldUnlock;

    public string DesUnlockGold;

    public DataCard dataCardUnlockGold;
}