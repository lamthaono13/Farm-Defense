using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UiRevive : UiCanvas
{
    [SerializeField] private float timeCount;

    [SerializeField] private TextMeshProUGUI textCountTime;

    [SerializeField] private Button btnGem;

    [SerializeField] private Button btnReward;

    [SerializeField] private int gemRevive;

    [SerializeField] private TextMeshProUGUI textGem;

    [SerializeField] private GameObject objHideGem;

    private float currentTimeCount;

    private Tween tweenCount;

    protected override void Start()
    {
        base.Start();

        btnGem.onClick.AddListener(OnClickBtnGem);

        btnReward.onClick.AddListener(OnClickBtnReward);
    }


    public void OnEndgame(GameResult gameResult)
    {
        if (gameResult == GameResult.Lose)
        {
            Show(true);
            Init();
        }
    }

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);
    }

    public override void Init()
    {
        base.Init();

        textGem.text = gemRevive.ToString();

        long currentGem = GameManager.Instance.DataManager.GetGem();

        if(currentGem < gemRevive)
        {
            objHideGem.gameObject.SetActive(true);
        }
        else
        {
            objHideGem.gameObject.SetActive(false);
        }

        CountTime();
    }

    private void CountTime()
    {
        tweenCount = DOTween.To((x) => { textCountTime.text = ((int)x).ToString(); }, timeCount, 0, timeCount).SetUpdate(true).SetEase(DG.Tweening.Ease.Linear).OnComplete(() => 
        {
            LevelManagerMainGame.Instance.UiManagerMainGame.UiLose.OnEndgame(GameResult.Lose);
            Show(false);
        });
    }

    private void OnClickBtnGem()
    {
        tweenCount.Kill();

        GameManager.Instance.DataManager.AddGem(-gemRevive, "Use_Gem_To_Revive");

        LevelManagerMainGame.Instance.UiManagerMainGame.UiReviveResume.Show(true);

        HealthGamePlay.Instance.OnRevive();

        EnergyManager.Instance.OnRevive();

        int level = GameManager.Instance.DataManager.GetLevel();

        HandleFireBase.Instance.LogEventWithParameter("Revive", new FirebaseParam[] { new FirebaseParam("Level", level) });

        Show(false);
    }

    private void OnClickBtnReward()
    {
        tweenCount.Kill();

        AdsManager.Instance.ShowRewarded(() => 
        {
            LevelManagerMainGame.Instance.UiManagerMainGame.UiReviveResume.Show(true);

            HealthGamePlay.Instance.OnRevive();

            EnergyManager.Instance.OnRevive();

            int level = GameManager.Instance.DataManager.GetLevel();

            HandleFireBase.Instance.LogEventWithParameter("Revive", new FirebaseParam[] { new FirebaseParam("Level", level) });

            Show(false);
        }, "Revive");
    }
}
