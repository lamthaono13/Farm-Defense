using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UiHealthBoss : UiCanvas
{
    [SerializeField] private Image imgFill;

    private Tween tween;

    public void OnChangeHealth(float index , float current, float max)
    {
        //imgFill.fillAmount = current / max;

        current -= index;

        if(tween != null)
        {
            tween.Kill();
        }

        tween = imgFill.DOFillAmount(current / max, 0.05f).SetUpdate(true).OnComplete(() => { tween = null; });
    }
}
