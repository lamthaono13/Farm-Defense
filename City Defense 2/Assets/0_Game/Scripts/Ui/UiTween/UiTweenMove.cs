using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using System;

public class UiTweenMove : UiTween
{
    // [SerializeField] private float timeDelayMove;
    //
    // [SerializeField] private bool isMove;
    //
    // [ShowIf("isMove")] [SerializeField] private TypeMoveUi typeMoveUi;
    //
    // [ShowIf("isMove")] [SerializeField] private Vector2 moveOffset;
    //
    // [ShowIf("isMove")] [SerializeField] private float timeMove;
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
    //     if (isMove)
    //     {
    //         Vector3 u = transform.localPosition;
    //     
    //         switch (typeMoveUi)
    //         {
    //             case TypeMoveUi.X:
    //     
    //                 transform.localPosition = new Vector3(u.x + moveOffset.x, u.y, u.z);
    //     
    //                 transform.DOLocalMove(u, timeMove).SetDelay(timeDelayMove).SetEase(DG.Tweening.Ease.Linear).SetUpdate(true).OnComplete(
    //                     () => { eventComplete?.Invoke();});
    //     
    //                 break;
    //     
    //             case TypeMoveUi.Y:
    //     
    //                 transform.localPosition = new Vector3(u.x, u.y + moveOffset.y, u.z);
    //     
    //                 transform.DOLocalMove(u, timeMove).SetDelay(timeDelayMove).SetEase(DG.Tweening.Ease.Linear).SetUpdate(true).OnComplete(
    //                     () => { eventComplete?.Invoke();});
    //     
    //                 break;
    //             
    //             case TypeMoveUi.XY:
    //                 break;
    //     
    //         }
    //     }
    // }
    //
    // public override void StopTween(Action eventComplete)
    // {
    //     base.StopTween(eventComplete);
    // }
    //
    // public override void OnReset()
    // {
    //     base.OnReset();
    // }
}
