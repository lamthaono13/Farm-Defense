using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class UiTweenScale : UiTween
{
    // [SerializeField] private float timeDelayScale;
    //
    // [SerializeField] private bool isScale;
    //
    // [ShowIf("isScale")] [SerializeField] private float scaleOffset;
    //
    // [ShowIf("isScale")] [SerializeField] private float timeScale;
    //
    // public override void Start()
    // {
    //     base.Start();
    // }
    //
    // public override void StartTween(Action eventComplete)
    // {
    //     base.StartTween(eventComplete);
    //     
    //     if (isScale)
    //     {
    //         transform.DOScale(Vector3.one * 1.05f, timeScale / 2).SetDelay(timeDelayScale).SetEase(DG.Tweening.Ease.Linear).SetUpdate(true).OnComplete(() =>
    //         {
    //             transform.DOScale(Vector3.one, timeScale / 2).SetDelay(timeDelayScale).SetEase(DG.Tweening.Ease.Linear).SetUpdate(true).OnComplete(() => 
    //             {
    //                 eventComplete?.Invoke();
    //             });
    //         });
    //     }
    // }
    //
    // public override void OnReset()
    // {
    //     base.OnReset();
    // }
    //
    // public override void StopTween(Action eventComplete)
    // {
    //     base.StopTween(eventComplete);
    // }
}
