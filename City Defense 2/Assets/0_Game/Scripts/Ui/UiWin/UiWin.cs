using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UiWin : UiCanvas
{
    [SerializeField] private Button btnHome;

    [SerializeField] private Button btnNextLevel;

    [SerializeField] private Button btnReward;

    [SerializeField] private Sprite spriteMissionUncomplete;

    [SerializeField] private Sprite spriteMissionComplete;

    [SerializeField] private Image imageMissionStarQuest_1;

    [SerializeField] private Image imageMissionStarQuest_2;

    [SerializeField] private TextMeshProUGUI textQuest_1;

    [SerializeField] private TextMeshProUGUI textQuest_2;

    [SerializeField] private TextMeshProUGUI textGoldReward;

    [SerializeField] private TextMeshProUGUI textGemReward;

    [SerializeField] private TextMeshProUGUI textGoldX2;

    [SerializeField] private TextMeshProUGUI textGemX2;

    [SerializeField] private GameObject objHasEarnGem;

    [SerializeField] private GameObject objStar_Quest_1;

    [SerializeField] private GameObject objStar_Quest_2;

    [SerializeField] private GameObject objX2Gem;

    [SerializeField] private GameObject objHideRewardX2;

    [SerializeField] private GameObject objHideBtnNext;

    [SerializeField] private TextMeshProUGUI textTicket;

    private int goldReward;

    private int goldX2;

    private int gemReward;

    private int gemX2;

    private bool hasTutorial;

    private bool hasGetReward;

    protected override void Start()
    {
        base.Start();

        btnHome.onClick.AddListener(OnClickBtnHome);

        btnNextLevel.onClick.AddListener(OnClickBtnNextLevel);

        btnReward.onClick.AddListener(OnClickBtnReward);
    }


    public void OnEndgame(GameResult gameResult)
    {
        if(gameResult == GameResult.Win)
        {
            Init();

            Show(true);
        }
        
    }

    //IEnumerator WaitToStop()
    //{
    //    yield return new WaitForSeconds(0);

    //    //Show(false);

    //    GameManager.Instance.OnNextLevel("");
    //}



    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            GameManager.Instance.SoundManager.PlaySoundInGame(false);

            GameManager.Instance.SoundManager.PlaySoundEndGame(true, GameResult.Win);

            int currentLevel = GameManager.Instance.DataManager.GetLevel();

            int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            // Log

            if (levelUnlock == currentLevel)
            {
                HandleFireBase.Instance.LogEventComplete(currentLevel, LevelManagerMainGame.Instance.GetTimePlay());

                HandleAppsflyer.Instance.LogEventLevelAchieved(currentLevel.ToString(), "");
            }
            else
            {
                HandleFireBase.Instance.LogEventWithParameter("Level_Complete_Play_Again", new FirebaseParam[] { new FirebaseParam("Level", currentLevel), new FirebaseParam("Timeplayed", LevelManagerMainGame.Instance.GetTimePlay()) });
            }

            bool a = GameManager.Instance.DataManager.GetHasTutorialBtnNextLv1();

            if(currentLevel == 1 && !a && !GameManager.Instance.NoTutorial)
            {
                LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.StartTutorialNextUiWin();

                HandleFireBase.Instance.LogEventWithString("Tut3");

                GameManager.Instance.DataManager.SetHasTutorialBtnNextLv1();

                btnHome.enabled = false;

                hasTutorial = true;
            }

            bool b = GameManager.Instance.DataManager.GetHasTutorialBtnHomeLv2();

            if(currentLevel == 2 && !b && !GameManager.Instance.NoTutorial)
            {
                LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.StartTutorialHomeUi();

                GameManager.Instance.DataManager.SetHasTutorialBtnHomeLv2();

                HandleFireBase.Instance.LogEventWithString("Tut4");

                btnNextLevel.enabled = false;

                hasTutorial = true;
            }

            bool c = GameManager.Instance.DataManager.GetHasTutorialBtnHomeLv5();

            if(currentLevel == 5 && !c && !GameManager.Instance.NoTutorial)
            {
                LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.StartTutorialHomeUi();

                GameManager.Instance.DataManager.SetHasTutorialBtnHomeLv5();

                HandleFireBase.Instance.LogEventWithString("Tut12");

                btnNextLevel.enabled = false;

                hasTutorial = true;
            }

            if (!GameManager.Instance.DataManager.GetRate() && currentLevel == 3 && !GameManager.Instance.NoTutorial)
            {
                GameManager.Instance.DataManager.SetRate();

                GameManager.Instance.CallRatePoppup();

                hasTutorial = true;
            }
        }

    }

    public override void Init()
    {
        base.Init();

        hasGetReward = false;

        int numberTicketNext = GameManager.Instance.DataManager.DataManagerMainGame.DataGame.TicketPlay;

        textTicket.text = numberTicketNext.ToString();

        if(GameManager.Instance.DataManager.GetTicket() >= numberTicketNext)
        {
            objHideBtnNext.gameObject.SetActive(false);
        }
        else
        {
            objHideBtnNext.gameObject.SetActive(true);
        }

        textQuest_1.text = GameManager.Instance.DataManager.DataManagerMainGame.DataQuestDisplay.GetDisplayString((int)LevelManagerMainGame.Instance.QuestManager.GetQuest_1());

        textQuest_2.text = GameManager.Instance.DataManager.DataManagerMainGame.DataQuestDisplay.GetDisplayString((int)LevelManagerMainGame.Instance.QuestManager.GetQuest_2());

        int numberQuest = LevelManagerMainGame.Instance.QuestManager.CheckQuestCompleted();

        if(numberQuest >= 1)
        {
            imageMissionStarQuest_1.sprite = spriteMissionComplete;

            objStar_Quest_1.gameObject.SetActive(true);
        }

        if (numberQuest >= 2)
        {
            imageMissionStarQuest_2.sprite = spriteMissionComplete;

            objStar_Quest_2.gameObject.SetActive(true);
        }

        goldReward = LevelManagerMainGame.Instance.GetGoldLevel();

        goldX2 = goldReward * 2;

        gemReward = LevelManagerMainGame.Instance.GetGemLevel();

        gemX2 = gemReward * 2;

        textGoldReward.text = goldReward.ToString();

        textGoldX2.text = goldX2.ToString();

        textGemReward.text = gemReward.ToString();

        textGemX2.text = gemX2.ToString();

        bool a = GameManager.Instance.DataManager.GetLevelStage(GameManager.Instance.DataManager.GetLevel()).HasComplete;

        if (!a)
        {
            objHasEarnGem.gameObject.SetActive(false);

            objX2Gem.gameObject.SetActive(true);
        }
        else
        {
            objHasEarnGem.gameObject.SetActive(true);

            objX2Gem.gameObject.SetActive(false);
        }
    }

    public void OnClickBtnReward()
    {
        AdsManager.Instance.ShowRewarded(() => 
        {
            bool a = GameManager.Instance.DataManager.GetLevelStage(GameManager.Instance.DataManager.GetLevel()).HasComplete;

            if (!a)
            {
                GameManager.Instance.DataManager.AddGold(goldX2, "Gold_X2_Reward_Win");

                GameManager.Instance.DataManager.AddGem(gemX2, "Gem_X2_Reward_Win");
            }
            else
            {
                GameManager.Instance.DataManager.AddGold(goldX2, "Gold_X2_Reward_Win");
            }

            hasGetReward = true;

            objHideRewardX2.gameObject.SetActive(true);

        }, "Reward Win");


    }

    public void OnClickBtnHome()
    {
        btnHome.enabled = false;

        HandleFireBase.Instance.LogEventWithParameter("Click_Btn_Home_Win", new FirebaseParam[] { new FirebaseParam("Level", GameManager.Instance.DataManager.GetLevel()) });

        bool a = GameManager.Instance.DataManager.GetLevelStage(GameManager.Instance.DataManager.GetLevel()).HasComplete;

        if (!hasGetReward)
        {
            if (!a)
            {
                GameManager.Instance.DataManager.AddGold(goldReward, "Gold_Reward_Win");

                GameManager.Instance.DataManager.AddGem(gemReward, "Gem_Reward_Win");
            }
            else
            {
                GameManager.Instance.DataManager.AddGold(goldReward, "Gold_Reward_Win");
            }
        }

        int numberQuest = LevelManagerMainGame.Instance.QuestManager.CheckQuestCompleted();

        GameManager.Instance.DataManager.SetLevelStage(GameManager.Instance.DataManager.GetLevel(), numberQuest + 1);

        GameManager.Instance.DataManager.LevelUp();

        GameManager.Instance.Loading(TypeLoading.LoadingToLobby, () => 
        {
            int currentLevel = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            if (currentLevel >= 4 && !GameManager.Instance.NoTutorial && !hasTutorial)
            {
                AdsManager.Instance.ShowInterstitial("End_Game_Win_BtnHome");
            }

            GameManager.Instance.LoadLobby("");
        } , "");
    }

    public void OnClickBtnNextLevel()
    {
        btnNextLevel.enabled = false;

        HandleFireBase.Instance.LogEventWithParameter("Click_Btn_NextLevel_Win", new FirebaseParam[] { new FirebaseParam("Level", GameManager.Instance.DataManager.GetLevel()) });

        bool a = GameManager.Instance.DataManager.GetLevelStage(GameManager.Instance.DataManager.GetLevel()).HasComplete;

        if (!hasGetReward)
        {
            if (!a)
            {
                GameManager.Instance.DataManager.AddGold(goldReward, "Gold_Reward_Win");

                GameManager.Instance.DataManager.AddGem(gemReward, "Gem_Reward_Win");
            }
            else
            {
                GameManager.Instance.DataManager.AddGold(goldReward, "Gold_Reward_Win");
            }
        }

        int numberQuest = LevelManagerMainGame.Instance.QuestManager.CheckQuestCompleted();

        GameManager.Instance.DataManager.SetLevelStage(GameManager.Instance.DataManager.GetLevel(), numberQuest + 1);

        GameManager.Instance.DataManager.AddTicket(-GameManager.Instance.DataManager.DataManagerMainGame.DataGame.TicketPlay, "Use_Ticket_To_Next_Level");

        GameManager.Instance.OnNextLevel(() => 
        {
            int currentLevel = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            if (currentLevel >= 4 && !GameManager.Instance.NoTutorial && !hasTutorial)
            {
                AdsManager.Instance.ShowInterstitial("End_Game_Win_BtnNextLevel");
            }
        },"");
    }
}


