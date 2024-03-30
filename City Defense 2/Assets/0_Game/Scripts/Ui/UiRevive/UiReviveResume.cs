using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class UiReviveResume : UiCanvas
{
    [SerializeField] private TextMeshProUGUI textCount;

    [SerializeField] private int timeCount;

    private Tween tweenCount;

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            GameManager.Instance.SetTimeScale(0);

            StartCount(); 
        }
    }

    public void StartCount() 
    {
        tweenCount = DOTween.To((x) => { textCount.text = ((int)x).ToString(); }, timeCount, 0, timeCount).SetUpdate(true).SetEase(DG.Tweening.Ease.Linear).OnComplete(() =>
        {
            LevelManagerMainGame.Instance.OnRevive();
            Show(false);
        });
    }
}
