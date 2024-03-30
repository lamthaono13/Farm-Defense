using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ElementQuestRewardStage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textQuest;

    [SerializeField] private TextMeshProUGUI textGem;

    [SerializeField] private TextMeshProUGUI textGold;

    [SerializeField] private Image imgBg;

    [SerializeField] private Sprite spriteNomal;

    [SerializeField] private Sprite spriteCollect;

    [SerializeField] private Button btnGet;

    [SerializeField] private GameObject objHide;

    private DataEachRewardStage eachRewardStage;

    private PopupRewardStage popupRewardStage;

    private bool canButton;

    private void Start()
    {
        btnGet.onClick.AddListener(OnClickBtnGet);
    }

    public void Init(DataEachRewardStage _dataEachRewardStage, PopupRewardStage _popupRewardStage, TypeElementQuestRewardStage typeElementQuestRewardStage)
    {
        eachRewardStage = _dataEachRewardStage;

        popupRewardStage = _popupRewardStage;

        textGem.text = eachRewardStage.GemEarn.ToString();

        textGold.text = eachRewardStage.GoldEarn.ToString();

        textQuest.text = "Collect " + eachRewardStage.StarNeed.ToString() + " star";

        switch (typeElementQuestRewardStage)
        {
            case TypeElementQuestRewardStage.Nomarl:

                canButton = false;

                objHide.gameObject.SetActive(false);
                imgBg.sprite = spriteNomal;

                break;
            case TypeElementQuestRewardStage.CanGet:

                canButton = true;

                objHide.gameObject.SetActive(false);
                imgBg.sprite = spriteCollect;

                break;
            case TypeElementQuestRewardStage.HasGet:

                canButton = false;

                objHide.gameObject.SetActive(true);
                imgBg.sprite = spriteNomal;

                break;
        }

        imgBg.SetNativeSize();
    }

    public void OnClickBtnGet()
    {
        if (!canButton)
        {
            return;
        }

        GameManager.Instance.DataManager.AddGold(eachRewardStage.GoldEarn, "Gold_From_Reward_Quest_Star");

        GameManager.Instance.DataManager.AddGem(eachRewardStage.GemEarn, "Gem_From_Reward_Quest_Star");

        GameManager.Instance.DataManager.SetGetRewardInMap(eachRewardStage.StarNeed, GameManager.Instance.DataManager.GetMap());

        HandleFireBase.Instance.LogEventWithParameter("Get_Reward_Star_Lobby_Stage", new FirebaseParam[] 
        { 
            new FirebaseParam("Map", GameManager.Instance.DataManager.GetMap()), 
            new FirebaseParam("Star_Quest", eachRewardStage.StarNeed)
        });

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if (levelUnlock == 6 && !b)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeUnit();
        }

        popupRewardStage.Init();
    }
}

public enum TypeElementQuestRewardStage
{
    Nomarl,
    CanGet,
    HasGet
}