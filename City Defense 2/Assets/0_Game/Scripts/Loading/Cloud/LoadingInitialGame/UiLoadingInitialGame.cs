using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UiLoadingInitialGame : UiCanvas
{
    [SerializeField] private float timeWaitToShowBarLoading;
    [SerializeField] private float timeLoading;
    [SerializeField] private Image imgFill;
    [SerializeField] private GameObject barLoading;

    [SerializeField] private RectTransform rectImgFollow;

    [SerializeField] private float min;

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);
    }

    private void Update()
    {

    }

    public void OnLoading(Action actionWhenLoadingDone)
    {
        StartCoroutine(WaitToShowBarLoading(actionWhenLoadingDone));
    }

    IEnumerator WaitToShowBarLoading(Action actionWhenLoadingDone)
    {
        yield return new WaitForSeconds(timeWaitToShowBarLoading);
        barLoading.gameObject.SetActive(true);

        DOTween.To((x) => 
        {
            imgFill.fillAmount = x;
            rectImgFollow.anchoredPosition = new Vector2(min + imgFill.fillAmount * 820, rectImgFollow.anchoredPosition.y);
        }, 0, 1, timeLoading).OnComplete(() => 
        {
//#if !UNITY_EDITOR
//            AdsManager.Instance.ShowAOA();
//#endif
            actionWhenLoadingDone?.Invoke(); 
        });

        //imgFill.DOFillAmount(1, timeLoading).OnComplete(() => { actionWhenLoadingDone?.Invoke(); });
    }
}
