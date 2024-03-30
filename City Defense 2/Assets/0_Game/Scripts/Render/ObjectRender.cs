using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectRender : RenderControl
{
    protected Tween TweenGetHit;

    private Vector3 positionInitial;

    private void Start()
    {
        
    }

    public override void OnGetHit()
    {
        //base.OnGetHit();

        if(TweenGetHit == null)
        {
            TweenGetHit = transform.DOLocalMoveY(-0.1f, 0.1f).SetEase(DG.Tweening.Ease.Linear).OnComplete(() =>
            {
                transform.DOLocalMoveY(0, 0.1f).SetEase(DG.Tweening.Ease.Linear).OnComplete(() => { TweenGetHit = null; });
            });
        }
    }
}
