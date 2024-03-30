using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UiLose : UiCanvas
{
    [SerializeField] private Button btnHome;

    [SerializeField] private Button btnReplay;

    [SerializeField] private Button btnReward;

    [SerializeField] private Sprite spriteMissionUncomplete;

    [SerializeField] private Sprite spriteMissionComplete;

    [SerializeField] private Image imageMissionStarQuest_1;

    [SerializeField] private Image imageMissionStarQuest_2;

    [SerializeField] private TextMeshProUGUI textQuest_1;

    [SerializeField] private TextMeshProUGUI textQuest_2;

    [SerializeField] private TextMeshProUGUI textGoldReward;

    [SerializeField] private TextMeshProUGUI textGoldX2;

    [SerializeField] private GameObject objHideRewardX2;

    private int goldReward;

    private int goldX2;

    private bool hasTutorial;

    private bool hasGetReward;

    protected override void Start()
    {
        base.Start();

        btnHome.onClick.AddListener(OnClickBtnHome);

        btnReplay.onClick.AddListener(OnClickBtnReplay);

        btnReward.onClick.AddListener(OnClickBtnReward);
    }

    public void OnEndgame(GameResult gameResult)
    {
        if (gameResult == GameResult.Lose)
        {
            Init();
            Show(true);
        } 
    }

    IEnumerator WaitToStop()
    {
        yield return new WaitForSeconds(0);

        //Show(false);

        GameManager.Instance.LoadLobby("");
    }



    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            GameManager.Instance.SoundManager.PlaySoundInGame(false);

            GameManager.Instance.SoundManager.PlaySoundEndGame(true, GameResult.Lose);

            int currentLevel = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            // Log

            GameManager.Instance.DataManager.SetLoseInLevel(currentLevel);

            if (levelUnlock == currentLevel)
            {
                HandleFireBase.Instance.LogEventFail(currentLevel, GameManager.Instance.DataManager.GetLoseInLevel(currentLevel));
            }
            else
            {
                HandleFireBase.Instance.LogEventWithParameter("Level_Lose_Play_Again", new FirebaseParam[] { new FirebaseParam("Level", currentLevel), new FirebaseParam("Lose_Count", GameManager.Instance.DataManager.GetLoseInLevel(currentLevel)) });
            }

            //bool b = GameManager.Instance.DataManager.GetHasTutorialBtnHomeLv2();

            if (!GameManager.Instance.NoTutorial)
            {
                if (currentLevel < 3)
                {
                    LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.StartTutorialReplayUi();

                    btnHome.enabled = false;

                    hasTutorial = true;

                    //GameManager.Instance.DataManager.SetHasTutorialBtnHomeLv2();
                }
                else
                {
                    if (currentLevel < 6 && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
                    {
                        LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.StartTutorialHomeUi();

                        GameManager.Instance.DataManager.SetIsTutorialUpgradeLobby();

                        btnReplay.enabled = false;

                        hasTutorial = true;
                    }
                }
            }
        }
    }

    public override void Init()
    {
        base.Init();

        hasGetReward = false;

        textQuest_1.text = GameManager.Instance.DataManager.DataManagerMainGame.DataQuestDisplay.GetDisplayString((int)LevelManagerMainGame.Instance.QuestManager.GetQuest_1());

        textQuest_2.text = GameManager.Instance.DataManager.DataManagerMainGame.DataQuestDisplay.GetDisplayString((int)LevelManagerMainGame.Instance.QuestManager.GetQuest_2());

        goldReward = LevelManagerMainGame.Instance.GetGoldLevel() / 4;

        goldX2 = goldReward * 2;

        textGoldReward.text = goldReward.ToString();

        textGoldX2.text = goldX2.ToString();
    }

    public void OnClickBtnReward()
    {
        AdsManager.Instance.ShowRewarded(() => 
        {
            GameManager.Instance.DataManager.AddGold(goldX2, "Reward_X2_Lose");

            objHideRewardX2.gameObject.SetActive(true);

            hasGetReward = true;

        }, "Reward Lose");
    }

    public void OnClickBtnHome()
    {
        btnHome.enabled = false;

        HandleFireBase.Instance.LogEventWithParameter("Click_Btn_Home_Lose", new FirebaseParam[] { new FirebaseParam("Level", GameManager.Instance.DataManager.GetLevel()) });

        if (!hasGetReward)
        {
            GameManager.Instance.DataManager.AddGold(goldReward, "Reward_Lose");
        }

        GameManager.Instance.Loading(TypeLoading.LoadingToLobby, () => 
        {
            int currentLevel = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            if (currentLevel >= 4 && !GameManager.Instance.NoTutorial && !hasTutorial)
            {
                AdsManager.Instance.ShowInterstitial("End_Game_Lose_BtnHome");
            }

            GameManager.Instance.LoadLobby("");
        } , "");
    }

    public void OnClickBtnReplay()
    {
        btnReplay.enabled = false;

        HandleFireBase.Instance.LogEventWithParameter("Click_Btn_Replay_Lose", new FirebaseParam[] { new FirebaseParam("Level", GameManager.Instance.DataManager.GetLevel()) });

        if (!hasGetReward)
        {
            GameManager.Instance.DataManager.AddGold(goldReward, "Reward_Lose");
        }

        GameManager.Instance.Loading(TypeLoading.LoadingToInGame, () => 
        {
            int currentLevel = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            if (currentLevel >= 4 && !GameManager.Instance.NoTutorial && !hasTutorial)
            {
                AdsManager.Instance.ShowInterstitial("End_Game_Lose_BtnReplay");
            }

            GameManager.Instance.LoadLevel("LoadingLevelInititalGame");
        } , "LoadingInititalGame");
    }
}