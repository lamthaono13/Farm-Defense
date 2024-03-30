using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManagerModeSurvival : UiManagerMainGame
{
    public override void Init()
    {
        //base.Init(configBaseIndices);

        //int currentLevel = GameManager.Instance.DataManager.GetLevel();

        //if (currentLevel <= 3)
        //{
        //    if (currentLevel == 3 && !GameManager.Instance.DataManager.GetRate())
        //    {
        //        LevelManagerMainGame.Instance.EndGameEvent += (gameResult) =>
        //        {
        //            LevelManagerMainGame.Instance.UiManagerMainGame.RateCheat.OnShow
        //            (
        //                (gameResult) =>
        //                {
        //                    switch (gameResult)
        //                    {
        //                        case GameResult.Lose:

        //                            if (!FirebaseRemoteData.CHECK_SHOW_ADS_FOR_TIER_1_COUNTRY)
        //                            {
        //                                AdsManager.Instance.ShowInterstitial();
        //                            }

        //                            GameManager.Instance.Loading(TypeLoading.LoadingToLobby, () => GameManager.Instance.LoadLobby("LoadingLevelInititalGame"), "LoadingInititalGame");

        //                            break;

        //                        case GameResult.Win:

        //                            if (!FirebaseRemoteData.CHECK_SHOW_ADS_FOR_TIER_1_COUNTRY)
        //                            {
        //                                AdsManager.Instance.ShowInterstitial();
        //                            }

        //                            GameManager.Instance.OnNextLevel("");

        //                            break;
        //                    }
        //                },
        //                gameResult
        //            );
        //        };
        //    }
        //    else
        //    {
        //        LevelManagerMainGame.Instance.EndGameEvent += (gameResult) =>
        //        {
        //            switch (gameResult)
        //            {
        //                case GameResult.Lose:

        //                    if (!FirebaseRemoteData.CHECK_SHOW_ADS_FOR_TIER_1_COUNTRY)
        //                    {
        //                        AdsManager.Instance.ShowInterstitial();
        //                    }

        //                    GameManager.Instance.Loading(TypeLoading.LoadingToLobby, () => GameManager.Instance.LoadLobby("LoadingLevelInititalGame"), "LoadingInititalGame");
        //                    break;
        //                case GameResult.Win:

        //                    if (!FirebaseRemoteData.CHECK_SHOW_ADS_FOR_TIER_1_COUNTRY)
        //                    {
        //                        AdsManager.Instance.ShowInterstitial();
        //                    }

        //                    GameManager.Instance.OnNextLevel("");
        //                    break;
        //            };
        //        };
        //    }
        //}
        //else
        //{
        //    LevelManagerMainGame.Instance.EndGameEvent += uiLose.OnEndgame;

        //    LevelManagerMainGame.Instance.EndGameEvent += uiWin.OnEndgame;
        //}


        // LevelManagerMainGame.Instance.EndGameEvent += uiLose.OnEndgame;
        //
        // LevelManagerMainGame.Instance.EndGameEvent += uiWin.OnEndgame;
        //
        //
        // LevelManagerMainGame.Instance.EndGameEvent += uiChooseChar.OnEndgame;
        //
        // LevelManagerMainGame.Instance.EndGameEvent += uiEnergy.OnEndgame;
        //
        // LevelManagerMainGame.Instance.EndGameEvent += uiBtn.OnEndgame;

        //LevelManagerMainGame.Instance.EndGameEvent += LogEndGame;


        //if (currentLevel == 1 && !GameManager.Instance.NoTutorial)
        //{
        //    Time.timeScale = 0.1f;

        //    uiTutorial.StartTutorialChoose();
        //}

        //if (!GameManager.Instance.NoTutorial)
        //{
        //    for (int i = 0; i < dataNewEnermy.DescriptionEnermies.Count; i++)
        //    {
        //        if (currentLevel == dataNewEnermy.DescriptionEnermies[i].levelShow)
        //        {
        //            descriptionEnermy = dataNewEnermy.DescriptionEnermies[i];

        //            btnNewEnermy.Show(true);

        //            LoadUiNewEnermy();

        //            break;
        //        }
        //    }
        //}

        //if (currentLevel == 4 && GameManager.Instance.DataManager.GetTutorialLobby() && PlayerPrefs.GetInt("TutInGameLv4", 0) == 0)
        //{
        //    PlayerPrefs.SetInt("TutInGameLv4", 1);

        //    hasTutorialLv4 = true;

        //    uiTutorial.ActiveTutorialLevel4(true);
        //}
    }
}
