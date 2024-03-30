using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiNotEnough : MonoBehaviour
{
    [SerializeField] private float offsetY;

    [SerializeField] private float timeMove;

    [SerializeField] private TextMeshProUGUI text;

    private Vector3 initialPosition;

    private bool isSetInitial;

    private Tween tweenMove;

    private Tween tweenFade;

    public void Play()
    {
        gameObject.SetActive(true);

        if (!isSetInitial)
        {
            isSetInitial = true;
            initialPosition = transform.localPosition;
        }

        transform.localPosition = initialPosition;

        if(tweenFade != null)
        {
            tweenFade.Kill();
        }

        if(tweenMove != null)
        {
            tweenMove.Kill();
        }

        tweenMove =  transform.DOLocalMoveY(initialPosition.y + offsetY, timeMove).SetUpdate(true).OnComplete(() => { gameObject.SetActive(false); tweenMove = null; });

        tweenFade = text.DOFade(1, 0).SetUpdate(true).OnComplete(() =>
        {
            tweenFade = text.DOFade(0, timeMove).SetUpdate(true).OnComplete(() => { tweenFade = null; });
        });
    }
}
