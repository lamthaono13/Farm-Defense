using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UITabStageLobby : UiTabLobby
{
    [SerializeField] private List<ElementLevelStage> elementLevelStages;

    [SerializeField] private List<GameObject> objMapStage;

    [SerializeField] private Button btnPlayStage;

    [SerializeField] private Button btnPlay;

    [SerializeField] private Button btnChest;

    [SerializeField] private Button btnBackMap;

    [SerializeField] private Button btnNextMap;

    [SerializeField] private GameObject objStage;

    [SerializeField] private GameObject objChoose;

    [SerializeField] private TextMeshProUGUI textMap;

    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private TextMeshProUGUI textQuest_1;

    [SerializeField] private TextMeshProUGUI textQuest_2;

    [SerializeField] private TextMeshProUGUI text_Gold;

    [SerializeField] private TextMeshProUGUI text_Gem;

    [SerializeField] private TextMeshProUGUI textStarChest;

    [SerializeField] private TextMeshProUGUI textTicket;

    [SerializeField] private List<ElementUiEnermyStage> elementUiEnermyStages;

    [SerializeField] private GameObject objTweenRewardStage;

    private int currentIdMap;

    protected override void Start()
    {
        base.Start();

        //Init();

        btnPlayStage.onClick.AddListener(OnClickBtnPlayStage);

        btnPlay.onClick.AddListener(OnClickBtnPlay);

        btnChest.onClick.AddListener(OnClickBtnChest);

        btnBackMap.onClick.AddListener(OnClickBtnBackMap);

        btnNextMap.onClick.AddListener(OnClickBtnNextMap);
    }

    public override void Init(int _idTab, bool _isDefault)
    {
        base.Init(_idTab, _isDefault);

        for (int i = 0; i < elementLevelStages.Count; i++)
        {
            elementLevelStages[i].Init(this, i + 1);
        }

        SetDefaulTab();

        currentIdMap = GameManager.Instance.DataManager.GetMap();

        LobbyManager.Instance.SetCurrentIdMapStage(currentIdMap);

        SetMap(currentIdMap);

        textTicket.text = GameManager.Instance.DataManager.DataManagerMainGame.DataGame.TicketPlay.ToString();
    }

    public void SetMap(int id)
    {
        for(int i = 0; i < objMapStage.Count; i++)
        {
            if(i == (id - 1))
            {
                objMapStage[i].gameObject.SetActive(true);
            }
            else
            {
                objMapStage[i].gameObject.SetActive(false);
            }
        }

        InitChestWarning();

        textMap.text = GameManager.Instance.DataManager.DataManagerMainGame.DataMapRender.dataMapRenders[id - 1].NameMap;

        textStarChest.text = GameManager.Instance.DataManager.GetStarInMap(GameManager.Instance.DataManager.GetMap()) + "/45";

        btnBackMap.gameObject.SetActive(currentIdMap <= 1 ? false: true);

        btnNextMap.gameObject.SetActive(currentIdMap >= GameManager.Instance.DataManager.DataManagerMainGame.DataMapRender.dataMapRenders.Count ? false : true);
    }

    public void InitChestWarning()
    {
        if (GameManager.Instance.DataManager.CheckCanGetRewardInMap(GameManager.Instance.DataManager.GetMap()))
        {
            objTweenRewardStage.gameObject.transform.localScale = Vector3.one;

            objTweenRewardStage.transform.DOScale(1.1f, 0.4f).SetEase(DG.Tweening.Ease.InSine).SetUpdate(true).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            objTweenRewardStage.gameObject.transform.localScale = Vector3.one;
        }
    }

    public void OnClickBtnNextMap()
    {
        GameManager.Instance.DataManager.LevelUpMap();

        currentIdMap = GameManager.Instance.DataManager.GetMap();

        LobbyManager.Instance.SetCurrentIdMapStage(currentIdMap);

        SetMap(currentIdMap);
    }

    public void OnClickBtnBackMap()
    {
        GameManager.Instance.DataManager.LevelDownMap();

        currentIdMap = GameManager.Instance.DataManager.GetMap();

        LobbyManager.Instance.SetCurrentIdMapStage(currentIdMap);

        SetMap(currentIdMap);
    }

    public override void SetChoosing(bool isChoose)
    {
        base.SetChoosing(isChoose);

        SetDefaulTab();
    }

    private void OnClickBtnPlayStage()
    {
        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (levelUnlock == 3 && !a)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
        }

        if (levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeLose();
        }

        ChangeTab(1);
    }

    private void OnClickBtnPlay()
    {
        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (levelUnlock == 3 && !a)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
        }

        if (levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeLose();
        }

        int currentTicket = GameManager.Instance.DataManager.GetTicket();

        int ticketToPlay = GameManager.Instance.DataManager.DataManagerMainGame.DataGame.TicketPlay;

        if (currentTicket >= ticketToPlay)
        {
            GameManager.Instance.DataManager.AddTicket(-ticketToPlay, "Use_Ticket_To_Play");

            TypeEquip typeEquipSlot1 = GameManager.Instance.DataManager.GetEquipAlly(TypeSlotEquip.Slot1);
            TypeEquip typeEquipSlot2 = GameManager.Instance.DataManager.GetEquipAlly(TypeSlotEquip.Slot2);
            TypeEquip typeEquipSlot3 = GameManager.Instance.DataManager.GetEquipAlly(TypeSlotEquip.Slot3);

            HandleFireBase.Instance.LogEventWithParameter("Play_With_Units", new FirebaseParam[] 
            {
                new FirebaseParam("level", GameManager.Instance.DataManager.GetLevel()),
                new FirebaseParam("Slot1", typeEquipSlot1.TypeGroup.ToString() + typeEquipSlot1.TypeTier.ToString() + typeEquipSlot1.TypeId.ToString()),
                new FirebaseParam("Slot2", typeEquipSlot2.TypeGroup.ToString() + typeEquipSlot2.TypeTier.ToString() + typeEquipSlot2.TypeId.ToString()),
                new FirebaseParam("Slot3", typeEquipSlot3.TypeGroup.ToString() + typeEquipSlot3.TypeTier.ToString() + typeEquipSlot3.TypeId.ToString())
            });

            GameManager.Instance.Loading(TypeLoading.LoadingToInGame, () => GameManager.Instance.LoadScene(2, TypeLoadScene.Additive, ""), "");
        }
        else
        {
            LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.EnergyPopup, true);
        }
    }

    private void OnClickBtnChest()
    {
        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if (levelUnlock == 6 && !b)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeUnit();
        }

        LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.RewardStagePopup, true);
    }

    private void SetDefaulTab()
    {
        ChangeTab(0);

        GameManager.Instance.DataManager.SetLevel(GameManager.Instance.DataManager.GetLevelMaxUnlock());
    }

    public void ChangeTab(int a)
    {
        if(a == 0)
        {
            GameManager.Instance.DataManager.SetLevel(GameManager.Instance.DataManager.GetLevelMaxUnlock());
        }

        objStage.gameObject.SetActive(a == 0 ? true : false);

        objChoose.gameObject.SetActive(a == 1 ? true : false);

        if(a == 1)
        {
            int currentLevel = GameManager.Instance.DataManager.GetLevel();

            DataLevel dataLevel = GameManager.Instance.DataManager.DataManagerMainGame.DataGame.DataLevels[currentLevel - 1];

            textLevel.text = "LEVEL " + currentLevel.ToString();

            textQuest_1.text = GameManager.Instance.DataManager.DataManagerMainGame.DataQuestDisplay.GetDisplayString(dataLevel.IndexQuest_1);

            textQuest_2.text = GameManager.Instance.DataManager.DataManagerMainGame.DataQuestDisplay.GetDisplayString(dataLevel.IndexQuest_2);

            text_Gem.text = dataLevel.gemDrop.ToString();

            text_Gold.text = dataLevel.GoldDrop.ToString();

            for(int i = 0; i < elementUiEnermyStages.Count;i++)
            {
                if(i >= dataLevel.EnermiesInLevel.Count)
                {
                    elementUiEnermyStages[i].gameObject.SetActive(false);
                }
                else
                {
                    elementUiEnermyStages[i].Init(dataLevel.EnermiesInLevel[i]);

                    elementUiEnermyStages[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
