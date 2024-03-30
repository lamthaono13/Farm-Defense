using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UiHealthGame : UiCanvas
{
    [SerializeField] private TextMeshProUGUI textHealth;

    [SerializeField] private Image imgWarning;

    [SerializeField] private Color colorFade;

    [SerializeField] private Color colorOrigin;

    private Tween tweenWarning;

    public void Init(int max)
    {
        textHealth.text = max.ToString();

        imgWarning.color = new Color(255, 255, 255, 0);
    }

    public void SubHealth(int current)
    {
        textHealth.text = current.ToString();

        if(tweenWarning != null)
        {
            tweenWarning.Kill();
        }

        tweenWarning = imgWarning.DOColor(colorOrigin, 0.05f).SetUpdate(true).OnComplete(() =>
        {
            tweenWarning = imgWarning.DOColor(colorFade, 0.05f).SetUpdate(true).OnComplete(() =>
            {
                tweenWarning = null;
            });
        });
    }

    public void SetHealth(int index)
    {
        textHealth.text = index.ToString();
    }
}
